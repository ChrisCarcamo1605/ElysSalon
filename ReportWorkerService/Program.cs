using Application.Configurations;
using Application.DependencyInjection;
using Application.Services;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.DependencyInjection;
using Infrastructure.Persistence.DataBase;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ReportWorkerService;


var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
builder.Services.AddWindowsService();  

// Database
builder.Services.AddDbContext<ElyDbContext>(options =>
    options.UseSqlServer(
        "Server=localhost,1433;Database=elysalondb;User Id=sa;Password=Carcamito*-*2024$1605;TrustServerCertificate=True;"));

// Generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Core services
builder.Services.AddAplication();
builder.Services.AddInfrastructure(
    "Server=localhost,1433;Database=elysalondb;User Id=sa;Password=Carcamito*-*2024$1605;TrustServerCertificate=True;");

builder.Services.AddScoped<IFilePathProvider, FilePathDialog>();
builder.Services.AddScoped<IReportInfraService, ReportInfraService>();

// AutoMapper
// Hosted service
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();