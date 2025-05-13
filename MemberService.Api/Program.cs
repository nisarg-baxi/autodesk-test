using AutoDeskTest.MemberService.Api.Data;
using AutoDeskTest.MemberService.Api.Repositories;
using AutoDeskTest.MemberService.Api.Services;
using AutoDeskTest.MemberService.Api.Middleware;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MemberDbContext>(options =>
    options.UseSqlite(connectionString));

// DI
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberServices, MemberServices>();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MemberService API",
        Version = "v1",
        Description = "An API to manage member records. Supports full and partial CRUD operations with a clean RESTful design.",
        Contact = new OpenApiContact
        {
            Name = "Nisarg Baxi",
            Email = "nisargbaxi@gmail.com",
            Url = new Uri("https://github.com/nisarg-baxi")
        }
    });
});
var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MemberService API v1");
});
app.UseAuthorization();
app.MapControllers();
app.Run();