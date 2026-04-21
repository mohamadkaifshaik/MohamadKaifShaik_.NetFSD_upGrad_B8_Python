using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeeCRM.Core.Entities;
using EmployeeCRM.Core.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatusEnum = EmployeeCRM.Core.Enums.TaskStatus;

namespace EmployeeCRM.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            // Table name
            builder.ToTable("Tasks");

            // Primary key
            builder.HasKey(t => t.Id);

            // Properties configuration
            builder
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(t => t.Description)
                .HasMaxLength(1000);

            builder
                .Property(t => t.Status)
                .HasConversion<int>()
                .HasDefaultValue(TaskStatusEnum.Pending);

            builder
                .Property(t => t.Priority)
                .HasDefaultValue(1);

            builder
                .Property(t => t.DueDate)
                .IsRequired();

            builder
                .Property(t => t.ProgressPercentage)
                .HasDefaultValue(0);

            builder
                .Property(t => t.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationship with Employee (Many-to-One, Required)
            builder
                .HasOne(t => t.Employee)
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship with Client (Many-to-One, Optional)
            builder
                .HasOne(t => t.Client)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
