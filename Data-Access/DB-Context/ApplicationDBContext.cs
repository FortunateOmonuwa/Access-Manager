using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data_Access.DB_Context
{
    public class ApplicationDBContext : IdentityDbContext<UserEntity>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {
            

        }


        public DbSet<UserEntity> Users { get; set; } 
    }
}
