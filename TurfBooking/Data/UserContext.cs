using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TurfBooking.Models;

namespace TurfBooking.Data
{
    public class UserContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=PAVILION;Initial Catalog=Turf;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
