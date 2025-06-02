using Infrastructure.workers.ReportWorkService;
using Microsoft.EntityFrameworkCore;
using System;
using Core.Interfaces.Services;
using Infrastructure.Persistence.DataBase;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ElyDbContext>(options =>
    options.UseSqlServer(
        "Server=localhost,1433;Database=elysalondb;User Id=sa;Password=Carcamito*-*2024$1605;TrustServerCertificate=True;"));

builder.Services.AddHostedService<Worker>();
var host = builder.Build();
host.Run();