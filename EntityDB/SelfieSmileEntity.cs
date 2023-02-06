using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SelfieSmile.Models;

namespace SelfieSmile.EntityDB
{
    public class SelfieSmileEntity : IdentityDbContext<AppUser>
    {
        public SelfieSmileEntity(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        //public DbSet<Reservation> Reservations { get; set; }
        //public DbSet<Appointment> Appointments { get; set; }

    }
}
