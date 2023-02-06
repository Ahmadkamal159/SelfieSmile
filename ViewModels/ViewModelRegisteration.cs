using System.ComponentModel.DataAnnotations;

namespace SelfieSmile.ViewModels
{
    public class ViewModelRegisteration
    {
        [Required,Display(Name ="UserName")]
        [StringLength(100,ErrorMessage ="username must be between 3 and 100 characters",MinimumLength =3)]
        public string UserName { get; set; } = string.Empty;

        [ Display(Name = "FirstName")]
        [StringLength(100, ErrorMessage = "username must be between 3 and 100 characters", MinimumLength = 3)]
        public string FirstName { get; set; } = string.Empty;
        
        [Display(Name = "LastName")]
        [StringLength(100, ErrorMessage = "username must be between 3 and 100 characters", MinimumLength = 3)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.EmailAddress)] 
        [Compare("Email",ErrorMessage ="email is not matched")]
        public string ConfirmEmail { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="password is not matched")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required,Display(Name="phone Number")]
        [RegularExpression(@"^(00201|\+201|01)[0-2,5]{1}[0-9]{8}$",ErrorMessage ="invalid mobile number")]
        public string PhoneNumber {get; set;}
        [Display(Name ="Profile Pic")]
        public byte[]? ProfilePicture { get; set; }

    }
}
