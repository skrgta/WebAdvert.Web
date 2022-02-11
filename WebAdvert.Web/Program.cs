using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//var cognitoIdentityProvider = new AmazonCognitoIdentityProviderClient(RegionEndpoint.USEast1);
//var cognitoUserPool = new CognitoUserPool(
//    builder.Configuration["AWS:UserPoolId"],
//    builder.Configuration["AWS:UserPoolClientId"],
//    cognitoIdentityProvider,
//    builder.Configuration["AWS:UserPoolClientSecret"]);

//builder.Services.AddSingleton<IAmazonCognitoIdentityProvider>(x => cognitoIdentityProvider);
//builder.Services.AddSingleton<CognitoUserPool>(x => cognitoUserPool);

builder.Services.AddCognitoIdentity(config =>
{
    config.Password = new Microsoft.AspNetCore.Identity.PasswordOptions()
    {
        RequireDigit = false,
        RequiredLength = 6,
        RequiredUniqueChars = 0,
        RequireLowercase = false,
        RequireNonAlphanumeric = false,
        RequireUppercase = false
    };
});
builder.Services.ConfigureApplicationCookie(configure =>
{
    configure.LoginPath = "/Accounts/Login";
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
