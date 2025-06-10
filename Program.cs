using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ZooManagement;
using ZooManagementDB;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ZooManagementDBContext>(
(options =>
   options.UseSqlite("Filename=MyDatabase.db")
   .UseSeeding((context, _) =>
   {
   int animalCount = 50;
       int zooKeeperCount = 20;
   List<string> enclosureNames = new List<string> { "Lion Enclosure", "Aviary", "Reptile House",  "Giraffe Enclosure", "Hippo Enclosure" };

       var ZooKeeperExists = context.Set<ZooKeeper>().Any();

       if (!ZooKeeperExists)
       {
         for (int i = 1; i <= zooKeeperCount; i++)
           {
               {
                   ZooKeeper newZooKeeper = FakeData.createZooKeeper();
                   context.Set<ZooKeeper>().Add(newZooKeeper);
               }
           }
           context.SaveChanges();
       }

   var enclosureExists = context.Set<Enclosure>().Any();
   if (!enclosureExists)
   {
           foreach (var name in enclosureNames)
           {
                // var zookeepers =  context.Set<ZooKeeper>().ToList();
               var zookeeperids = context.Set<ZooKeeper>().Select(r => r.ZooKeeperID).ToList();

               Enclosure newEnclosure = FakeData.createEnclosure(name, zookeeperids);
               context.Set<Enclosure>().Add(newEnclosure);
               context.SaveChanges();
               EnclosureZooKeeper newEnclosureZooKeeper = new EnclosureZooKeeper();
               newEnclosureZooKeeper.EnclosureID = newEnclosure.EnclosureID;
               newEnclosureZooKeeper.ZooKeeperID = newEnclosure.ZooKeeperID;
               context.Set<EnclosureZooKeeper>().Add(newEnclosureZooKeeper);

                        

       }
       context.SaveChanges();
   }
   var animalExists = context.Set<Animal>().Any();
       if (!animalExists)
       {
           for (int i = 1; i <= animalCount; i++)
           {
               {
                   Animal newAnimal = FakeData.generateFakeAnimalData(context);
                   context.Set<Animal>().Add(newAnimal);
               }
           }
           context.SaveChanges();
       }

   })
   //    .UseAsyncSeeding(async (context, _, CancellationToken) =>
   //    {
   //        int count = 100;
   //        for (int i = 1; i <= count; i++)
   //        {
   //            var animalExists = await context.Set<Animal>().AnyAsync();

   //            if (!animalExists)
   //            {
   //                Animal newAnimal = FakeData.generateFakeAnimalData();
   //                await context.Set<Animal>().AddAsync(newAnimal);
   //                await context.SaveChangesAsync();
   //            }
   //        }
   //    })



   )

   );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
