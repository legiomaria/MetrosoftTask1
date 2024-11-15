using MetrosoftSearch.Api.EF;
using Microsoft.EntityFrameworkCore;

namespace MetrosoftSearch.Api.Helpers;

public class DatabaseHelper
{
    public static void UpdateDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        context.Database.Migrate();
        return;
    }
}
