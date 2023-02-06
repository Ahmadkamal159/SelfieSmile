using System.ComponentModel.DataAnnotations.Schema;

namespace SelfieSmile.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public DateTime AddAppointmentDate { get; set; }

        public string ReservedToDoctorId { get; set; }
        [ForeignKey("ReservedToDoctorId")]
        public AppUser ReservedToDoctor { get; set; }

        public Reservation? Reservation { get; set; }

        public bool IsReserved { get; set; }
        //public enum Month {Jan,Feb,Mars,Apr,May,June,July,Aug,Sep,Oct,Nov,Dec}
        //public enum Day { Sat,Sun,Mon,Tue,Wed,Thu,Fri}
        //public enum Hour { Four,Five,Six,Seven,Eight,Nine,Ten,Eleven}
    }
}
