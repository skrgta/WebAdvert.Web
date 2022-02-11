using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAdvert.Web.Models;
using WebAdvert.Web.Models.Accounts;

namespace WebAdvert.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly CognitoSignInManager<CognitoUser> signInManager;
        private readonly CognitoUserManager<CognitoUser> userManager;
        private readonly CognitoUserPool pool;

        public AccountsController(SignInManager<CognitoUser> signInManager,
            UserManager<CognitoUser> userManager,
            CognitoUserPool pool)
        {
            this.signInManager = (CognitoSignInManager<CognitoUser>)signInManager;
            this.userManager = (CognitoUserManager<CognitoUser>)userManager;
            this.pool = pool;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            var model = new SignupModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                CognitoUser user = pool.GetUser(model.Email);
                user.Attributes.Add(CognitoAttribute.Name.ToString(), model.Email);

                if(user.Status != null)
                {
                    ModelState.AddModelError("UserExists", "User with this email already exists");
                    return View(model);
                }

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AdminConfirmSignUpAsync(user);
                    return RedirectToAction("Login");
                }
            }

            return View(model);
        }

        public IActionResult Confirm()
        {
            var model = new ConfirmModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("ConfirmPost")]
        public async Task<IActionResult> Confirm(ConfirmModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if(user == null)
                {
                    ModelState.AddModelError("NotFound", "A user with the given email address was not found");
                    return View(model);
                }

                var result = await userManager.ConfirmSignUpAsync(user, model.Code, true);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                {
                    foreach(var error in result.Errors)
                        ModelState.AddModelError(error.Code, error.Description);

                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                CognitoUser user = await userManager.FindByEmailAsync(model.Email);
                await userManager.ChangePasswordAsync(user, model.Password, model.Password);
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError("LoginError", "Email and Password do not match");
            }

            return View("Login", model);
        }
    }
}
