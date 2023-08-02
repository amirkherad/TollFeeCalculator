using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TollFeeCalculator.Presentation.Filters.Swagger;

public class RemoveVersionParameterFilter 
    : IOperationFilter
{
    public void Apply(
        OpenApiOperation operation, 
        OperationFilterContext context)
    {
        var versionParameter = operation.Parameters.FirstOrDefault(p => p.Name == "version");
        if (versionParameter != null)
            operation.Parameters.Remove(versionParameter);
    }
}