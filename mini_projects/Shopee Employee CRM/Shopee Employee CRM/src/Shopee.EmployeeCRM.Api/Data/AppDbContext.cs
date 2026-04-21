using Microsoft.EntityFrameworkCore;
using Shopee.EmployeeCRM.Api.Entities;

namespace Shopee.EmployeeCRM.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Client> Clients => Set<Client>();

    public DbSet<WorkTask> WorkTasks => Set<WorkTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasIndex(employee => employee.Email)
            .IsUnique();

        modelBuilder.Entity<Employee>()
            .HasMany(employee => employee.Clients)
            .WithOne(client => client.Employee)
            .HasForeignKey(client => client.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasMany(employee => employee.Tasks)
            .WithOne(task => task.Employee)
            .HasForeignKey(task => task.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
