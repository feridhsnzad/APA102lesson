using EateryCafe.Models;
using Microsoft.EntityFrameworkCore;

namespace EateryCafe.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options){ }
        public DbSet<Chef> Chefs { get;set; }
        public DbSet<Role> Roles { get; set; }
    }
}
