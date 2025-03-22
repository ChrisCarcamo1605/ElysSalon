using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using ElysSalon2._0.Core.aplication.Management;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.domain.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace ElysSalon2._0.Core.domain.Services;

public class SalesService
{
    private ISalesRepository _salesRepo;
    private ITicketRepository _ticketRepo;

    public SalesService(ISalesRepository salesRepo, ITicketRepository ticketRepo)
    {
        _salesRepo = salesRepo;
        _ticketRepo = ticketRepo;
        
    }


    public async Task GenerateReport(ObservableCollection<object> collection)
    {
    
       
    }
    
}