using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using SelfieSmile.Interfaces;

namespace SelfieSmile.Models
{
    public class UniversityDTO:IBasicInfo
    {
        public int Id { get; set; }

        [Remote(action: "CheckName", controller: "University", ErrorMessage = "Name Already Exist",AdditionalFields ="Id")]
        [StringLength(30,ErrorMessage = "Max Length of the name is 30)")]
        [Display(Name ="University Name")]
        [Required(ErrorMessage = "Please enter university name")]
        public string Name{ get; set; }
        
        
        [StringLength(40)]
        [Display(Name ="university Location")]
        public string Location { get; set; }
        //public List<College> colleges { get; set; }
    }
}
