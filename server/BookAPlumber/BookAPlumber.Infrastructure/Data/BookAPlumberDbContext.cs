using BookAPlumber.Core.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookAPlumber.Infrastructure.Data
{
    public class BookAPlumberDbContext : DbContext
    {
        public BookAPlumberDbContext(DbContextOptions<BookAPlumberDbContext> dbContextOptions) : base(dbContextOptions)
        {   
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Part> Parts { get; set; }
    }
}
