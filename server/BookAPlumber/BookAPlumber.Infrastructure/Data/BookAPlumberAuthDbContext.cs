using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookAPlumber.Infrastructure.Data
{
    public class BookAPlumberAuthDbContext : IdentityDbContext
    {
        public BookAPlumberAuthDbContext(DbContextOptions<BookAPlumberAuthDbContext> dbContextOptions) : base(dbContextOptions)
        {   
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "901320ac-e2ad-4abb-a0d9-d213dc83dc9e";
            var plumberRoleId = "1d279d26-f55c-4d76-9592-8d9cba905939";
            var userRoleId = "4290920a-cbe8-4466-a5c0-92aa11578408";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                 new IdentityRole()
                {
                    Id = plumberRoleId,
                    ConcurrencyStamp = plumberRoleId,
                    Name = "Plumber",
                    NormalizedName = "Plumber".ToUpper()
                },
                  new IdentityRole()
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
