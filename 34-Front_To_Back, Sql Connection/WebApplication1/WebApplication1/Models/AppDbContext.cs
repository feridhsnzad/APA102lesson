using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Flowers> Flowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=localhost,1433;
                      Database=ProniaDb;
                      User Id=SA;
                      Password=Orxan_2005._;
                      TrustServerCertificate=True;");
            }
        }
    }
}