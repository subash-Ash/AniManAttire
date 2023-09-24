using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
       
        builder.Services.AddScoped<IProductRepository, ProductRepository>(); /* So, when you register IProductRepository as scoped, it means that a new instance of ProductRepository will be created for each incoming HTTP request, and it will be available to all components (controllers, middleware, etc.) that need it within the scope of that request. This helps ensure that different requests do not interfere with each other and can have their own isolated instances of the repository.
        
        This is a common pattern in ASP.NET Core for setting up dependency injection for your application's services, making your code more maintainable, testable, and loosely coupled.*/
        
        builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
        builder.Services.AddDbContext<StoreContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); //default
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
        using(var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(context,loggerFactory);
            }
            catch(Exception ex) 
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error during migration");
            }

            app.Run();
        }
        
    }
}