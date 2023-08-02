using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pluralize.NET.Core;
using TollFeeCalculator.Domain.Entities.Cities;
using TollFeeCalculator.Domain.Entities.Provinces;
using TollFeeCalculator.Domain.Shared.BaseClasses;
using TollFeeCalculator.Infrastructure.DataAccess.Enums;

namespace TollFeeCalculator.Infrastructure.DataAccess.EntitiesConfigurations;

/// <summary>
/// EntityTypeConfiguration for the Province entity
/// </summary>
public class ProvinceConfiguration 
    : IEntityTypeConfiguration<Province>
{
    /// <summary>
    /// Implementation of Configure method of IEntityTypeConfiguration interface 
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable(
            name: new Pluralizer().Pluralize(nameof(Province)),
            schema: DatabaseSchemaNames.Dbo.Name.ToLower());
     
        builder
            .Property(baseEntity => baseEntity.Id)
            .HasColumnName(nameof(City.Id))
            .IsRequired();

        builder
            .Property(baseEntity => baseEntity.CreatedOn)
            .HasColumnName(nameof(City.CreatedOn))
            .HasDefaultValueSql("getdate()");
        
        builder
            .HasIndex(baseEntity => baseEntity.CreatedOn);

        builder
            .Property(e => e.Name)
            .HasColumnName(nameof(Province.Name))
            .HasMaxLength(Constants.MaxNameLength)
            .IsRequired();
        
        builder
            .HasIndex(e => e.Name);
    }
}