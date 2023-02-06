using SelfieSmile.Interfaces;

namespace SelfieSmile.Models
{
    public class DepartmentDTO : IBasicInfo
    {
        public int Id { get; set; }
        public string Name { get; set;}
        //public int CollegeId;
    }
}
