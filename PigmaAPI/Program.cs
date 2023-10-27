using Microsoft.EntityFrameworkCore;
using PigmaAPI.Infrastructure.ApplicationDbContext;
using PigmaAPI.Services.ServiceInjections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var allowedOrigins = builder.Configuration["AllowedOrigins"] ?? throw new ArgumentNullException("AllowedOrigins was null");

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AllowOrigin",
             b =>
             {
                 b.WithOrigins(allowedOrigins.Split(",", StringSplitOptions.RemoveEmptyEntries)
                     .ToArray()
                     )
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials();
             });
    });

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    );
ServiceInjections.ConfigureServices(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
