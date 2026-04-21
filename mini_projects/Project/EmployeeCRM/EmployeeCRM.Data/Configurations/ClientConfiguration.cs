using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeeCRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            // Table name
            builder.ToTable("Clients");

            // Primary key
            builder.HasKey(c => c.Id);

            // Properties configuration
            builder
                .Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(c => c.ContactName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(c => c.Phone)
                .HasMaxLength(15);

            builder
                .Property(c => c.Address)
                .HasMaxLength(300);

            builder
                .Property(c => c.City)
                .HasMaxLength(100);

            builder
                .Property(c => c.State)
                .HasMaxLength(100);

            builder
                .Property(c => c.ZipCode)
                .HasMaxLength(20);

            builder
                .Property(c => c.Rating)
                .HasDefaultValue(0);

            builder
                .Property(c => c.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationship with Employee (Many-to-One)
            builder
                .HasOne(c => c.Employee)
                .WithMany(e => e.Clients)
                .HasForeignKey(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship with Tasks (One-to-Many)
            builder
                .HasMany(c => c.Tasks)
                .WithOne(t => t.Client)
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
