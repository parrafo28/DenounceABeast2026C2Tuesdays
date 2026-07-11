using DenounceBeasts.Application.Models;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infraestructure;
using DenounceBeasts.Infraestructure.Repository;
using DenunciaUnaBestia.Api.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddAutoMapper(cfg =>
{
    // Registrar el perfil manualmente (opcional):
    cfg.AddProfile<MappingProfile>();
}, typeof(Program).Assembly /* escanear automát. perfiles en el assembly */);

//var automapperLicence = builder.Configuration.GetSection("KeysConfigurations:AutomapperLicenceKey").Value;
//var automapperLicence2 = builder.Configuration.GetSection("AutomapperLicenceKey").Value;
///var settingValue = builder.Configuration.GetSection("Logging:LogLevel:Microsoft.AspNetCore").Value;
//
//builder.Services.AddAutoMapper(cfg => cfg.LicenseKey = automapperLicence, typeof(MappingProfile));

builder.Services.AddScoped<ComplaintTypeRepository>();
builder.Services.AddScoped<SectorRepository>();
builder.Services.AddScoped<GenericRespository<Status>>();
builder.Services.AddScoped<GenericRespository<Municipality>>();
builder.Services.AddScoped<UnitOfWork>();

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
