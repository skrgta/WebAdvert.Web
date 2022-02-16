using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Web.Models.AdvertManagement
{
    public class CreateAdvertViewModel
    {
        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}
