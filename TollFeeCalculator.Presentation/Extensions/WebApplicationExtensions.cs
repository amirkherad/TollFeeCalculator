namespace TollFeeCalculator.Presentation.Extensions;

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
    }
}