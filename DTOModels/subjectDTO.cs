using SelfieSmile.Interfaces;

namespace SelfieSmile.Models
{
    public class subjectDTO : IBasicInfo
    {
        public int Id { get; set;}
        public string Name { get; set;}
        //public double? Grade { get; set; }
        //readonly double SubjectGrade;
        //readonly double Percentage;

    }
}
