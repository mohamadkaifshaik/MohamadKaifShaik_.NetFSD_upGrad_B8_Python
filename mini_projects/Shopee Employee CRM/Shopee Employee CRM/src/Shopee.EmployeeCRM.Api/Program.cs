using Microsoft.EntityFrameworkCore;
using Shopee.EmployeeCRM.Api.Data;
using Shopee.EmployeeCRM.Api.Repositories;
using Shopee.EmployeeCRM.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IWorkTaskRepository, WorkTaskRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IWorkTaskService, WorkTaskService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

var app = builder.Build();

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    await DbSeeder.SeedAsync(context);
}

app.Run();
