using Microsoft.EntityFrameworkCore;
using STGeneticsA.Models;

namespace STGeneticsA.NewFolder
{
    public class AnimalDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=STGenetics;Trusted_Connection=True;");
        }


    }

}
