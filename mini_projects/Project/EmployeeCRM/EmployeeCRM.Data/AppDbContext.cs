using EmployeeCRM.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using EmployeeCRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Data
{
    /// <summary>
    /// Main DbContext for the Employee CRM System
    /// All entities are configured here
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets - Collections of entities
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        /// <summary>
        /// Configure entity relationships and constraints
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all entity configurations
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());

            // Seed initial data (optional - for testing)
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Seed initial data for testing
        /// </summary>
        private void SeedData(ModelBuilder modelBuilder)
        {
            // We can add seed data here later if needed
            // For now, leave empty
        }
    }
}
