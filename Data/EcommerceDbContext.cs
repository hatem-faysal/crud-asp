using crud2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace crud2.Data
{
    //public class EcommerceDbContext: DbContext
    public class EcommerceDbContext : IdentityDbContext<ApplicationUser>
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products{ get; set; }

 
    }
}
