using Microsoft.EntityFrameworkCore;
using TollFeeCalculator.Infrastructure.DataAccess;

namespace TollFeeCalculator.Presentation.Extensions;

/// <summary>
/// Uses the middlewares
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Uses the middlewares
    /// </summary>
    /// <param name="webApplication"></param>
    public static void UseServices(this WebApplication webApplication)
    {
        if (webApplication.Environment.IsDevelopment())
            webApplication.UseSwaggerDocumentation();

        webApplication.UseHttpsRedirection();

        webApplication.UseAuthorization();

        webApplication.MapControllers();
        
        webApplication.MigrateDatabase();
    }
    
    /// <summary>
    /// Checks database for existing. If there is not, creates new one.
    /// </summary>
    /// <param name="webApplication"></param>
    /// <returns></returns>
    private static WebApplication MigrateDatabase(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        using var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        applicationDbContext.Database.Migrate();
        return webApplication;
    }
}