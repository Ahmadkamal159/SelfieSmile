using System.ComponentModel.DataAnnotations;

namespace SelfieSmile.ViewModels
{
    public class ViewModelRole
    {
        [Required,Display(Name ="Role Name")]
        public string RoleName { get; set; }

        public string Id { get; set; }

    }
}
