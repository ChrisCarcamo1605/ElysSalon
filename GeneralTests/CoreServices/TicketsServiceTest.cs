using System.Collections.ObjectModel;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Services;
using Moq;

namespace GeneralTests.CoreServices;

[TestClass]
public class TicketsServiceTest
{
    private Mock<ITicketRepository> _ticketRepo;
    private TicketService _ticketService;

    [TestInitialize]
    public void SetUp()
    {
        _ticketRepo = new Mock<ITicketRepository>();
        _ticketService = new TicketService(_ticketRepo.Object);
    }

    [TestMethod]
    public async Task AddTicket()
    {
        var ticket = new Ticket
        {
            EmissionDateTime = new DateTime(25, 06, 06),
            Issuer = "Messi",
            TicketId = "1",
            TotalAmount = 100.0m,
            TotalOutTaxes = 125.25m
        };

        _ticketRepo.Setup(x => x.SaveAsync(ticket)).ReturnsAsync(It.IsAny<Ticket>());

        var result = await _ticketService.AddAsync(ticket);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);

        _ticketRepo.Verify(x => x.SaveAsync(ticket), Times.Once);
    }

    [TestMethod]
    public async Task DeleteTicket()
    {
        var ticketId = "1";
        _ticketRepo.Setup(x => x.GetByIdAsync(ticketId)).ReturnsAsync(new Ticket { TicketId = ticketId });
        _ticketRepo.Setup(x => x.DeleteAsync(It.IsAny<Ticket>()));
        var result = await _ticketService.DeleteAsync(ticketId);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        _ticketRepo.Verify(x => x.DeleteAsync(It.IsAny<Ticket>()), Times.Once);
    }

    [TestMethod]
    public async Task GetAllTickets()
    {
        var tickets = new ObservableCollection<Ticket>
        {
            new()
            {
                TicketId = "1", Issuer = "Messi", TotalAmount = 100.0m, TotalOutTaxes = 125.25m,
                EmissionDateTime = new DateTime(25, 06, 06)
            },
            new()
            {
                TicketId = "2", Issuer = "Ronaldo", TotalAmount = 200.0m, TotalOutTaxes = 250.50m,
                EmissionDateTime = new DateTime(26, 07, 07)
            }
        };
        _ticketRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(It.IsAny<ObservableCollection<Ticket>>());
        var result = await _ticketService.GetAllOfAsync();

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);

        _ticketRepo.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [TestMethod]
    public async Task GetTicketById()
    {
        var ticketId = "1";
        var ticket = new Ticket
        {
            TicketId = ticketId, Issuer = "Messi", TotalAmount = 100.0m, TotalOutTaxes = 125.25m,
            EmissionDateTime = new DateTime(25, 06, 06)
        };
        _ticketRepo.Setup(x => x.FindAsync(x => x.TicketId == ticketId)).ReturnsAsync(ticket);

        var result = await _ticketService.GetByIdAsync(ticketId);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);

        _ticketRepo.Verify(x => x.FindAsync(x => x.TicketId == ticketId), Times.Once);
    }


    [TestMethod]
    public async Task GetLastIdAsync()
    {
        var lastTicket = new Ticket
        {
            TicketId = "1",
            EmissionDateTime = new DateTime(2025, 03, 06),
            Issuer = "test",
            TotalAmount = 125.25m
        };

        _ticketRepo.Setup(x => x.GetLastId()).ReturnsAsync(lastTicket);

        var result = _ticketService.GetLastIdAsync();
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Result.Success);

        _ticketRepo.Verify(x => x.GetLastId(), Times.Once);
    }
}