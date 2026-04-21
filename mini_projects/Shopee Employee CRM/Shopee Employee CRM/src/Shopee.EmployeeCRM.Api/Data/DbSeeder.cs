using Shopee.EmployeeCRM.Api.Entities;
using Shopee.EmployeeCRM.Shared.Enums;

namespace Shopee.EmployeeCRM.Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Employees.Any())
        {
            return;
        }

        var employees = new List<Employee>
        {
            new()
            {
                FullName = "Aarav Sharma",
                Email = "aarav@company.com",
                Department = "Sales",
                Designation = "Sales Executive",
                Role = EmployeeRole.Manager,
                JoinedOn = DateTime.Today.AddMonths(-10)
            },
            new()
            {
                FullName = "Priya Patel",
                Email = "priya@company.com",
                Department = "Support",
                Designation = "Support Specialist",
                Role = EmployeeRole.Employee,
                JoinedOn = DateTime.Today.AddMonths(-6)
            },
            new()
            {
                FullName = "Rahul Verma",
                Email = "rahul@company.com",
                Department = "Operations",
                Designation = "Operations Admin",
                Role = EmployeeRole.Admin,
                JoinedOn = DateTime.Today.AddMonths(-14)
            }
        };

        context.Employees.AddRange(employees);
        await context.SaveChangesAsync();

        context.Clients.AddRange(
            new Client
            {
                CompanyName = "Bright Solutions",
                ContactPerson = "Neha Kapoor",
                Email = "contact@brightsolutions.com",
                PhoneNumber = "9876543210",
                EmployeeId = employees[0].Id,
                Notes = "Interested in yearly support package."
            },
            new Client
            {
                CompanyName = "Blue Tech",
                ContactPerson = "Amit Roy",
                Email = "amit@bluetech.com",
                PhoneNumber = "9123456780",
                EmployeeId = employees[1].Id,
                Notes = "Needs weekly follow-up."
            });

        context.WorkTasks.AddRange(
            new WorkTask
            {
                Title = "Prepare monthly sales report",
                Description = "Create a simple summary for all active clients.",
                EmployeeId = employees[0].Id,
                DueDate = DateTime.Today.AddDays(3),
                Status = WorkStatus.InProgress
            },
            new WorkTask
            {
                Title = "Call support client",
                Description = "Discuss onboarding and support plan.",
                EmployeeId = employees[1].Id,
                DueDate = DateTime.Today.AddDays(1),
                Status = WorkStatus.Pending
            },
            new WorkTask
            {
                Title = "Review CRM data entries",
                Description = "Check employee and client records for duplicates.",
                EmployeeId = employees[2].Id,
                DueDate = DateTime.Today.AddDays(-1),
                Status = WorkStatus.Completed
            });

        await context.SaveChangesAsync();
    }
}
