using eb4341u202318323.API.Maintenance.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace eb4341u202318323.API.Maintenance.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyMaintenanceConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Project>().HasKey(p => p.Id);
        builder.Entity<Project>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        
        builder.Entity<Project>().OwnsOne(p => p.CodeProject, cp =>
        {
            cp.WithOwner().HasForeignKey("Id"); 
            
            cp.Property(c => c.Code)
                .HasColumnName("CodeProject") 
                .IsRequired();
        });

        builder.Entity<Project>().Property(p => p.ProjectName).IsRequired().HasMaxLength(50);
        builder.Entity<Project>().Property(p => p.Type).IsRequired();
        builder.Entity<Project>().Property(p => p.MaterialsBudgetUsd).IsRequired();
        builder.Entity<Project>().Property(p => p.PersonnelBudgetUsd).IsRequired();
        builder.Entity<Project>().Property(p => p.DurationMonths).IsRequired();
    }
}