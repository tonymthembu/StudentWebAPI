using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StudentWebAPI.Data;
using Swashbuckle.AspNetCore.Filters;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentWebAPI", Version = "v1" });

    // Add a security scheme for JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    // Add a global filter to include the JWT token in Swagger requests  // 081 782 3013 Lona Ndamase - KZN Tourism
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
