using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeeCRM.Core.Entities;
using EmployeeCRM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Table name
            builder.ToTable("Employees");

            // Primary key
            builder.HasKey(e => e.Id);

            // Properties configuration
            builder
                .Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(e => e.Phone)
                .HasMaxLength(15);

            builder
                .Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(e => e.Salary)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            builder
                .Property(e => e.Status)
                .HasConversion<int>()
                .HasDefaultValue(EmployeeStatus.Active);

            builder
                .Property(e => e.JoinDate)
                .IsRequired();

            builder
                .Property(e => e.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            // Self-referencing relationship (Manager-Subordinates)
            builder
                .HasOne(e => e.Manager)
                .WithMany(e => e.Subordinates)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship with User (One-to-One, Optional)
            // Configured in UserConfiguration

            // Relationship with Clients (One-to-Many)
            builder
                .HasMany(e => e.Clients)
                .WithOne(c => c.Employee)
                .HasForeignKey(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship with Tasks (One-to-Many)
            builder
                .HasMany(e => e.Tasks)
                .WithOne(t => t.Employee)
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique email constraint
            builder.HasIndex(e => e.Email).IsUnique();
        }
    }
}
