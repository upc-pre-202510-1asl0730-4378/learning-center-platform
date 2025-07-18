using eb4341u202318323.API.Hr.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eb4341u202318323.API.Hr.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyHrConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Employee>().HasKey(e => e.Id);
        builder.Entity<Employee>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();

        builder.Entity<Employee>().Property(e => e.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Employee>().Property(e => e.MonthlySalary).IsRequired();
        builder.Entity<Employee>().Property(e => e.ContractDurationMonths).IsRequired();
        builder.Entity<Employee>().Property(e => e.StartDate).IsRequired();

        builder.Entity<Employee>().OwnsOne(p => p.CodeProject, cp =>
        {
            cp.WithOwner().HasForeignKey("Id");
            cp.Property(c => c.Code)
                .HasColumnName("CodeProject")
                .IsRequired();
        });

        builder.Entity<Employee>()
            .HasOne(e => e.ContractType) 
            .WithMany() 
            .HasForeignKey("ContractTypeId") 
            .IsRequired(); 
    }
}