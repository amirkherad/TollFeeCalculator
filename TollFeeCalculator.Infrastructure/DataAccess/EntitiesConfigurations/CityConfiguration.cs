using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TollFeeCalculator.Domain.Entities.Cities;
using TollFeeCalculator.Domain.Shared.BaseClasses;
using TollFeeCalculator.Infrastructure.DataAccess.Enums;

namespace TollFeeCalculator.Infrastructure.DataAccess.EntitiesConfigurations;

/// <summary>
/// EntityTypeConfiguration for the City entity
/// </summary>
public class CityConfiguration
    : IEntityTypeConfiguration<City>
{
    /// <summary>
    /// Implementation of Configure method of IEntityTypeConfiguration interface 
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable(
            DatabaseTablesName.City,
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
            .Property(city => city.ProvinceId)
            .HasColumnName(nameof(City.ProvinceId));

        builder
            .HasIndex(city => city.ProvinceId);

        builder
            .Property(e => e.Name)
            .HasColumnName(nameof(City.Name))
            .HasMaxLength(Constants.MaxNameLength)
            .IsRequired();

        builder
            .HasOne(d => d.Province)
            .WithMany(p => p.Cities)
            .HasForeignKey(d => d.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasIndex(e => e.Name);

        builder
            .HasIndex(city => new { city.ProvinceId, city.Name })
            .IsUnique();
    }
}