using SelfieSmile.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfieSmile.ViewModels
{
    public class ViewModelAppointment
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AddAppointmentDate { get; set; }
        [Required]
        public string ReservedToDoctorName { get; set; }

        public bool IsReserved { get; set; }
    }
}
