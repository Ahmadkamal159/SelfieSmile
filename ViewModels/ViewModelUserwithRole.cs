using System.ComponentModel.DataAnnotations;

namespace SelfieSmile.ViewModels
{
    public class ViewModelUserwithRole
    {
        [Display( Name ="User Name")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Role")]
        public string? Role { get; set; }

    }
}
