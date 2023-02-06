using System.ComponentModel.DataAnnotations.Schema;

namespace SelfieSmile.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime ReportDate { get; set; } = DateTime.Now;
        
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }

        public DateTime ReservedDate { get; set; }

        public enum Type {General,
            CosmeticDentistry,
            DentalImplants,
            Endodontics,
            Orthodontics,
            PediatricDentistry
        }
        public string ReservedById { get; set; }
        [ForeignKey("ReservedById")]
        public AppUser ReservedBy { get; set; }

        public string ReservedToId { get; set; }
        [ForeignKey("ReservedToId")]
        public AppUser ReservedTo { get; set; }

        public bool IsConfirmed { get; set; }

    }
}
