using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app ,bool isProd)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                SeedData(context,isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
{
    if(isProd)
    {
        Console.WriteLine("--> Atempting to apply mugrations...");
        try 
        {
        context.Database.Migrate();
        }
        catch(Exception ex)
        {
        Console.WriteLine($"--> Could not run migration:{ex.Message}");

        }
    }
    if (!context.Platforms.Any())
    {
        Console.WriteLine("--> Seeding Data...");

        context.Platforms.AddRange(
            new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
            new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
            new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
        );

        context.SaveChanges();
        Console.WriteLine("--> Data seeded successfully.");
    }
    else
    {
        Console.WriteLine("--> We already have data.");
    }
}
    }
}