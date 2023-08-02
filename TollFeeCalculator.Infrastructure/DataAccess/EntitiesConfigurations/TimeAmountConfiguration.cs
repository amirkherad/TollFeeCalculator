using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pluralize.NET.Core;
using TollFeeCalculator.Domain.Entities.TimeAmounts;
using TollFeeCalculator.Infrastructure.DataAccess.Enums;

namespace TollFeeCalculator.Infrastructure.DataAccess.EntitiesConfigurations;

/// <summary>
/// EntityTypeConfiguration for the TimeAmount entity
/// </summary>
public class TimeAmountConfiguration 
    : IEntityTypeConfiguration<TimeAmount>
{
    /// <summary>
    /// Implementation of Configure method of IEntityTypeConfiguration interface 
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<TimeAmount> builder)
    {
        builder.ToTable(
            name: new Pluralizer().Pluralize(nameof(TimeAmount)),
            schema: DatabaseSchemaNames.Dbo.Name.ToLower());
        
        builder
            .Property(baseEntity => baseEntity.Id)
            .HasColumnName(nameof(TimeAmount.Id))
            .IsRequired();

        builder
            .Property(baseEntity => baseEntity.CreatedOn)
            .HasColumnName(nameof(TimeAmount.CreatedOn))
            .HasDefaultValueSql("getdate()");
        
        builder
            .HasIndex(baseEntity => baseEntity.CreatedOn);

        builder
            .Property(e => e.CityId)
            .HasColumnName(nameof(TimeAmount.CityId))
            .IsRequired();

        builder
            .Property(e => e.VehicleTypeId)
            .HasColumnName(nameof(TimeAmount.VehicleTypeId));

        builder
            .Property(e => e.From)
            .HasColumnName(nameof(TimeAmount.From))
            .IsRequired();

        builder
            .Property(e => e.To)
            .HasColumnName(nameof(TimeAmount.To))
            .IsRequired();

        builder
            .HasIndex(e => e.CityId);

        builder
            .HasIndex(e => e.From);

        builder
            .HasIndex(e => e.To);

        builder
            .HasIndex(e => e.VehicleTypeId);

        builder
            .HasOne(d => d.City)
            .WithMany(p => p.TimeAmounts)
            .HasForeignKey(d => d.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(d => d.VehicleType)
            .WithMany(p => p.TimeAmounts)
            .HasForeignKey(d => d.VehicleTypeId);
    }
}