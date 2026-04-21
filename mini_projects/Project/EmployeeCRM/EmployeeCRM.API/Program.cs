using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EmployeeCRM.Core.Interfaces;
using EmployeeCRM.Data;
using EmployeeCRM.Repository;
using EmployeeCRM.Repository.Implementation;
using EmployeeCRM.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);

// ============ Configuration ============

// 1. Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// 2. Register Repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// 3. Register Services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// 4. Register AuthService (with JWT configuration)
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var jwtSecret = jwtSettings["Secret"];
var jwtIssuer = jwtSettings["Issuer"];
var jwtAudience = jwtSettings["Audience"];
var jwtExpirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"] ?? "60");

builder.Services.AddScoped<IAuthService>(provider =>
    new AuthService(
        provider.GetRequiredService<IUserRepository>(),
        jwtSecret,
        jwtIssuer,
        jwtAudience,
        jwtExpirationMinutes
    )
);

// 5. Add Authentication (JWT Bearer)
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// 6. Add Authorization
builder.Services.AddAuthorization();

// 7. Add Controllers
builder.Services.AddControllers();

// 8. Add Swagger (API Documentation)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Employee CRM API",
        Version = "v1",
        Description = "Multi-Page Employee CRM System API"
    });

    // Add JWT to Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// 9. Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMVC", builder =>
    {
        builder
            .WithOrigins("https://localhost:7001", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// ============ Build App ============
var app = builder.Build();

// Configure the HTTP request pipeline
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee CRM API v1");
        c.RoutePrefix = string.Empty; // Swagger at root
    });
//}

// Apply migrations automatically (optional)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

// CORS must be before Authentication
app.UseCors("AllowMVC");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Run the application
app.Run();






//-------------------------------------------------------------------------------------------------------
////var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.

////builder.Services.AddControllers();
////// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

////var app = builder.Build();

////// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}

////app.UseHttpsRedirection();

////app.UseAuthorization();

////app.MapControllers();

////app.Run();

//using EmployeeCRM.Core.Interfaces;
//using EmployeeCRM.Data;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Add DbContext
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection")
//    )
//);

////// Register repositories
////builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
////builder.Services.AddScoped<IClientRepository, ClientRepository>();
////builder.Services.AddScoped<ITaskRepository, TaskRepository>();
////builder.Services.AddScoped<IUserRepository, UserRepository>();

////// Generic repository (optional)
////builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//var app = builder.Build();

//// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
//    app.UseSwagger();
//    app.UseSwaggerUI();
////}

//// Optional: Apply migrations automatically
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    dbContext.Database.Migrate();
//}

//app.Run();
