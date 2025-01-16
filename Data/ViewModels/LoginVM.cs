using System.ComponentModel.DataAnnotations;

namespace crud2.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "This Field is Required!")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress {  get; set; }
        [Required(ErrorMessage = "This Field is Required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

