using Chubb.Prueba.BL.Interfaces;
using Chubb.Prueba.BL.Services;
using Chubb.Prueba.DAL.Repositorios;
using Chubb.Prueba.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(
         "MyAllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
        });
});

// Add services to the container.
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

builder.Services.AddControllers();
builder.Services.AddDbContext<ChubbDbContext>((options) => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAseguradoRepositorio, AseguradoRepositorio>();
builder.Services.AddScoped<IAseguradoSeguroRepositorio, AseguradoSeguroRepositorio>();
builder.Services.AddScoped<ISeguroRepositorio, SeguroRepositorio>();
builder.Services.AddScoped<IAseguradoServices, AseguradoServices>();
builder.Services.AddScoped<IAseguradoSeguroServices, AseguradoSeguroServices>();
builder.Services.AddScoped<ISeguroServices, SeguroServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseCors("MyAllowSpecificOrigins");

app.MapControllers();

app.Run();
