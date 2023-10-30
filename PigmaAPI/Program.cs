using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PigmaAPI.Helpers;
using PigmaAPI.Infrastructure.ApplicationDbContext;
using PigmaAPI.Services.ServiceInjections;
using WebApi.Helpers;

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
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    );
ServiceInjections.ConfigureServices(builder.Services);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization(); 
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
