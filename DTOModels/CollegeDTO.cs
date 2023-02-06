using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using SelfieSmile.Interfaces;

namespace SelfieSmile.Models
{
    public class CollegeDTO : IBasicInfo
    {
        public int Id { get ; set; }
        [Display(Name = "College Name")]
        public string Name { get; set; }
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
    }
}
