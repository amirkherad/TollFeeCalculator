using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using TollFeeCalculator.Presentation.Filters.Swagger;

namespace TollFeeCalculator.Presentation.Extensions;

/// <summary>
/// Setting up swagger
/// </summary>
public static class SwaggerConfiguration
{
    /// <summary>
    /// Config swagger service
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.IgnoreObsoleteActions();
            c.IgnoreObsoleteProperties();
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.OrderActionsBy(apiDescription => $"{apiDescription.ActionDescriptor.RouteValues["controller"]}_{apiDescription.HttpMethod}");
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var xmlPath = Path.Combine(basePath, $"{nameof(Presentation)}.xml");
            c.IncludeXmlComments(xmlPath);

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Toll Fee Calculator",
                Version = "v1.0",
                Contact = new OpenApiContact
                {
                    Name = "Support",
                    Email = "kheradmandrad.amir@gmail.com",
                    Url = new Uri("http://amirkheradmandrad.ir")
                },
                Description = "",
            });

            c.CustomSchemaIds(s => s.FullName);

            c.OperationFilter<RemoveVersionParameterFilter>();
            c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
        });

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.RegisterMiddleware = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        })
            .AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            });

        return services;
    }
    
    /// <summary>
    /// Use swagger service
    /// </summary>
    /// <param name="app"></param>
    /// <param name="routePrefix"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwaggerDocumentation(
        this IApplicationBuilder app,
        string routePrefix = "api/v1")
    {
        app.UseSwagger(swaggerOptions =>
        {
            swaggerOptions.RouteTemplate = $"/{routePrefix}/swagger/{{documentName}}/swagger.json";
        });

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.DocumentTitle = "Toll Fee Calculator";
            c.InjectStylesheet(@"/swagger-ui/site.css");
            c.EnableDeepLinking();
            c.SwaggerEndpoint($"/{routePrefix}/swagger/v1/swagger.json", "API V1");
            c.RoutePrefix = $"{routePrefix}/swagger"; ;
        });

        return app;
    }
}