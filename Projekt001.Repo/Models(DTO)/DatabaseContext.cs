using Microsoft.EntityFrameworkCore;
using Projekt001.Repo.Models_DTO_;

namespace Projekt001.Repo
{
    public class DatabaseContext : DbContext
    {

        //public DatabaseContext(DbContextOptions<DatabaseContext> option) : base(option) { }
        public DatabaseContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                base.OnConfiguring(optionBuilder);
                optionBuilder.UseSqlServer("Data Source =.; Initial Catalog = Projekt001; Integrated Security = True");
            }
        }
        public DbSet<Person> Person { get; set; }
        public DbSet<Car> Car { get; set; }
    }
}
