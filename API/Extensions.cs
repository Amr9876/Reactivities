
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API;

public static class AppExtensions
{
    
    public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        try
        {

            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                
            context.Database.Migrate();
            Seed.SeedData(context, userManager).Wait();

        }
        catch (Exception e)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogError(e, "an error occured during migration");
        }

        return app;
    }
}