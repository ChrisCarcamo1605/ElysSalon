using System.Reflection;
using Application.DependencyInjection;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.DependencyInjection;
using Infrastructure.Persistence.DataBase;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Service;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using ReportWorkerService;

var builder = Host.CreateApplicationBuilder(args);


var exePath = Assembly.GetExecutingAssembly().Location;
var rootDirectory = Path.GetDirectoryName(exePath);


builder.Configuration.AddJsonFile("appsettings.json", false);
builder.Services.AddWindowsService();

// Database
builder.Services.AddDbContext<ElyDbContext>(options =>
    options.UseSqlServer(
        "Server=localhost,1433;Database=elysalondb;User Id=sa;Password=Carcamito*-*2024$1605;TrustServerCertificate=True;"));

// Generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // para debugging local
builder.Services.AddAplication();
builder.Logging.AddEventLog(eventLogSettings => { eventLogSettings.SourceName = "ReportWorkService3"; });

builder.Services.AddInfrastructure(
    "Server=localhost,1433;Database=elysalondb;User Id=sa;Password=Carcamito*-*2024$1605;TrustServerCertificate=True;");

builder.Services.AddScoped<IFilePathProvider, FilePathDialogUtil>();
builder.Services.AddScoped<IReportInfraService, ReportInfraService>();

// AutoMapper
// Hosted service
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();