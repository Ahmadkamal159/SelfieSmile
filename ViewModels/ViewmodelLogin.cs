using System.ComponentModel.DataAnnotations;

namespace SelfieSmile.ViewModels
{
    public class ViewmodelLogin
    {
        [Required,Display(Name ="UserName OR E-mail")]
        public string UserName { get; set; } = string.Empty;
        
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
