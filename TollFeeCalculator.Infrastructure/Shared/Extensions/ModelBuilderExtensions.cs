using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace TollFeeCalculator.Infrastructure.Shared.Extensions;

/// <summary>
/// Entity framework modelBuilder extensions
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Finds entities that have implemented IEntityTypeConfiguration and applies configurations
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="assemblies"></param>
    public static void ApplyEntityTypeConfigurations(
        this ModelBuilder modelBuilder, 
        params Assembly[] assemblies)
    {
        var methodInfo = typeof(ModelBuilder)
            .GetMethods()
            .First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

        var types = assemblies
            .SelectMany(assembly => assembly.GetExportedTypes())
            .Where(type => type is { IsClass: true, IsAbstract: false, IsPublic: true })
            .ToList();

        foreach (var type in types)
        {
            var interfaces = type.GetInterfaces();
            
            foreach (var currentInterface in interfaces)
            {
                if (currentInterface.IsConstructedGenericType is false 
                    || currentInterface.GetGenericTypeDefinition() != typeof(IEntityTypeConfiguration<>))
                {
                    continue;
                }

                var method = methodInfo.MakeGenericMethod(currentInterface.GenericTypeArguments[0]);
                method.Invoke(modelBuilder, new[] { Activator.CreateInstance(type) });
            }
        }
    }
    
    /// <summary>
    /// Dynamically register all Entities that inherit from specific BaseType
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="assemblies">Assemblies contains Entities</param>
    /// <typeparam name="TBaseType">Base type that Entities inherit from this</typeparam>
    public static void RegisterAllEntities<TBaseType>(
        this ModelBuilder modelBuilder, 
        params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c is { IsClass: true, IsAbstract: false, IsPublic: true } && typeof(TBaseType).IsAssignableFrom(c));

        foreach (var type in types)
            modelBuilder.Entity(type);
    }
}