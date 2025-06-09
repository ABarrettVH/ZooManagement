//using Bookish.Models;
using Microsoft.EntityFrameworkCore;
using ZooManagement;

namespace ZooManagementDB
{
    public class ZooManagementDBContext : DbContext
    {
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Animal>()
        //         .Property(e => e.DOB)
        //         .HasConversion(
        //             v => v.ToString(), // Convert DateOnly to string for storage
        //             v => DateOnly.Parse(v)        // Convert string back to DateOnly
        //         );
        // }

        public ZooManagementDBContext(DbContextOptions<ZooManagementDBContext> options)
                 : base(options)
                { }

        // Put all the tables you want in your database here
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Enclosure> Enclosure { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // This is the configuration used for connecting to the database


            optionsBuilder.UseSqlite("Filename=MyDatabase.db");


            //         protected override void OnModelCreating(ModelBuilder modelBuilder)
            // {
            //     // Example seed data for Animals table
            //     modelBuilder.Entity<Animal>().HasData(
            //         new Animal { AnimalID=1, Name = "Ed", Species = "Lion", DOB = "09/10/2021", Sex= "Male", Classification= "Mammal", ArrivedAtZoo= "14/06/2022" },
            //         new Animal { AnimalID=2, Name = "Mary", Species = "Elephant", DOB = "08/12/2015", Sex= "Female", Classification="Mammals", ArrivedAtZoo= "10/10/2010" },
            //         new Animal { AnimalID=3, Name = "Harry", Species = "Panda", DOB = "12/05/2014", Sex= "Male", Classification= "Mammal", ArrivedAtZoo= "24/06/2022" },
            //         new Animal { AnimalID=4, Name = "Kelly", Species = "Parrot", DOB = "18/11/2017", Sex= "Female", Classification="Bird", ArrivedAtZoo= "14/08/2019" },
            //         new Animal { AnimalID=5, Name = "Bob", Species = "Frog", DOB = "23/10/2002", Sex= "Male", Classification= "Amphibian", ArrivedAtZoo= "17/10/2022" },
            //         new Animal { AnimalID=6, Name = "Lisa", Species = "Python", DOB = "01/01/2019", Sex= "Female", Classification="Reptile", ArrivedAtZoo= "19/12/2020" },
            //         new Animal { AnimalID=7, Name = "Valerie", Species = "Zebra", DOB = "30/08/2021", Sex= "Female", Classification= "Mammal", ArrivedAtZoo= "21/07/2022" },
            //         new Animal { AnimalID=8, Name = "Michael", Species = "Shark", DOB = "07/12/2019", Sex= "Male", Classification="Fish", ArrivedAtZoo= "06/11/2021" },
            //         new Animal { AnimalID=9, Name = "Sarah", Species = "Elephant", DOB = "09/04/2011", Sex= "Female", Classification= "Mammal", ArrivedAtZoo= "20/02/2020" },
            //         new Animal { AnimalID=10, Name = "Sophie", Species = "Lion", DOB = "05/04/2019", Sex= "Female", Classification="Mammal", ArrivedAtZoo= "02/10/2021" }
            //     );
            // }
        }
    }
}