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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table name
            builder.ToTable("Users");

            // Primary key
            builder.HasKey(u => u.Id);

            // Properties configuration
            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(u => u.PasswordHash)
                .IsRequired();

            builder
                .Property(u => u.Role)
                .HasConversion<int>();

            builder
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder
                .Property(u => u.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            // Unique constraint on Email
            builder.HasIndex(u => u.Email).IsUnique();

            // Relationship with Employee (One-to-One, Optional)
            builder
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
