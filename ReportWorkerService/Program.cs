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
using MailKit;
using Microsoft.EntityFrameworkCore;
using ReportWorkerService;
using MailService = Infrastructure.Service.MailService;

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


var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

builder.Services.AddInfrastructure(configuration,
    "Server=localhost,1433;Database=elysalondb;User Id=sa;Password=Carcamito*-*2024$1605;TrustServerCertificate=True;");

builder.Services.AddScoped<IFilePathProvider, FilePathDialogUtil>();
builder.Services.AddScoped<IReportInfraService, ReportInfraService>();
builder.Services.AddTransient<IEmailService, MailService>();
// En tu método AddInfrastructure

// AutoMapper
// Hosted service
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();