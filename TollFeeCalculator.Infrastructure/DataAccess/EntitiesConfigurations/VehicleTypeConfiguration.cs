using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pluralize.NET.Core;
using TollFeeCalculator.Domain.Entities.VehicleTypes;
using TollFeeCalculator.Domain.Shared.BaseClasses;
using TollFeeCalculator.Infrastructure.DataAccess.Enums;

namespace TollFeeCalculator.Infrastructure.DataAccess.EntitiesConfigurations;

/// <summary>
/// EntityTypeConfiguration for the TimeAmount entity
/// </summary>
public class VehicleTypeConfiguration 
    : IEntityTypeConfiguration<VehicleType>
{
    /// <summary>
    /// Implementation of Configure method of IEntityTypeConfiguration interface 
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<VehicleType> builder)
    {
        builder.ToTable(
            name: new Pluralizer().Pluralize(nameof(VehicleType)),
            schema: DatabaseSchemaNames.Dbo.Name.ToLower());

        builder
            .Property(baseEntity => baseEntity.Id)
            .HasColumnName(nameof(VehicleType.Id))
            .IsRequired();

        builder
            .Property(baseEntity => baseEntity.CreatedOn)
            .HasColumnName(nameof(VehicleType.CreatedOn))
            .HasDefaultValueSql("getdate()");
        
        builder
            .HasIndex(baseEntity => baseEntity.CreatedOn);

        builder
            .Property(e => e.Name)
            .HasColumnName(nameof(VehicleType.Name))
            .HasMaxLength(Constants.MaxNameLength)
            .IsRequired();

        builder
            .HasIndex(e => e.Name)
            .IsUnique();
    }
}