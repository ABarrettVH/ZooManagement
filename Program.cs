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
          int count = 30;
    
              for (int i = 1; i <= count; i++)
              {
           var animalExists = context.Set<Animal>().Any();

           if (!animalExists)
           {
               Animal newAnimal = FakeData.generateFakeAnimalData();
               Console.WriteLine(newAnimal.Id);
               context.Set<Animal>().Add(newAnimal);
               //    context.Set<Animal>().AddRange(newAnimal);

           }
       }
       context.SaveChanges();
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
