//using Bookish.Models;
using Microsoft.EntityFrameworkCore;
using ZooManagement;

namespace ZooManagementDB
{
    public class ZooManagementDBContext : DbContext
    {
        // Put all the tables you want in your database here
        public DbSet<Animal> Animals { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // This is the configuration used for connecting to the database
            optionsBuilder.UseSqlite("Filename=MyDatabase.db"); ;
        }
    }
}