using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfieSmile.Models
{
    public class AppUser : IdentityUser
    {
        public byte[]? ProfilePicture { get; set;}

        public string?FirstName { get; set;}

        public string? LastName { get; set; }

        //[InverseProperty("ReservedBy")]
        //public List<Reservation>? ReservationsByUser { get; set;}

        //[InverseProperty("ReservedTo")]
        //public List<Reservation>? ReservationsToDoctor { get; set; }
       
        //public List<Appointment>? Appointments { get; set; }

    }
}
