using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pluralize.NET.Core;
using TollFeeCalculator.Domain.Entities.SingleChargeRules;
using TollFeeCalculator.Infrastructure.DataAccess.Enums;

namespace TollFeeCalculator.Infrastructure.DataAccess.EntitiesConfigurations;

/// <summary>
/// EntityTypeConfiguration for the SingleChargeRule entity
/// </summary>
public class SingleChargeRuleConfiguration 
    : IEntityTypeConfiguration<SingleChargeRule>
{
    /// <summary>
    /// Implementation of Configure method of IEntityTypeConfiguration interface 
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<SingleChargeRule> builder)
    {
        builder.ToTable(
            name: new Pluralizer().Pluralize(nameof(SingleChargeRule)),
            schema: DatabaseSchemaNames.Dbo.Name.ToLower());

        builder
            .Property(baseEntity => baseEntity.Id)
            .HasColumnName(nameof(SingleChargeRule.Id))
            .IsRequired();

        builder
            .Property(baseEntity => baseEntity.CreatedOn)
            .HasColumnName(nameof(SingleChargeRule.CreatedOn))
            .HasDefaultValueSql("getdate()");
        
        builder
            .HasIndex(baseEntity => baseEntity.CreatedOn);

        builder
            .Property(e => e.CityId)
            .HasColumnName(nameof(SingleChargeRule.CityId))
            .IsRequired();
    
        builder
            .Property(e => e.VehicleTypeId)
            .HasColumnName(nameof(SingleChargeRule.VehicleTypeId));
        
        builder
            .Property(e => e.PeriodOfTime)
            .HasColumnName(nameof(SingleChargeRule.PeriodOfTime))
            .IsRequired();
        
        builder
            .HasIndex(e => e.CityId);
        
        builder
            .HasIndex(e => e.VehicleTypeId);

        builder
            .HasOne(d => d.City)
            .WithMany(p => p.SingleChargeRules)
            .HasForeignKey(d => d.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(d => d.VehicleType)
            .WithMany(p => p.SingleChargeRules)
            .HasForeignKey(d => d.VehicleTypeId);
    }
}