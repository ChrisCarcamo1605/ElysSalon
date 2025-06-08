using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Services;
using Moq;

namespace GeneralTests.CoreServices;

[TestClass]
public class TicketDetailsServiceTest
{
    private TicketDetailsService _service;
    private Mock<IRepository<TicketDetails>> _tickDetailsRepo;

    [TestInitialize]
    public void SetUp()
    {
        _tickDetailsRepo = new Mock<IRepository<TicketDetails>>();
        _service = new TicketDetailsService(_tickDetailsRepo.Object);
    }

    [TestMethod]
    public async Task TestGetTicketDetails()
    {
        var tickDetails = new List<TicketDetails>
        {
            new()
            {
                ArticleId = 1, ArticleName = "Test", Date = new DateTime(2025, 06, 25),
                Quantity = 2, Price = 10.0m, TicketId = "123", TicketDetailsId = 1
            },
            new()
            {
                ArticleId = 2, ArticleName = "Test2", Date = new DateTime(2025, 06, 26),
                Quantity = 3, Price = 15.0m, TicketId = "123", TicketDetailsId = 2
            }
        };

        var ticketId = 123;

        _tickDetailsRepo.Setup(x
                => x.GetAllWithIncludesAsync(x => x.Article))
            .ReturnsAsync(new ObservableCollection<TicketDetails>(tickDetails));

        var result = await _service.GetAllOfAsync();

        Assert.IsNotNull(result.Data);
        Assert.IsTrue(result.Success);

        _tickDetailsRepo.Verify(x => x.GetAllWithIncludesAsync(x => x.Article));
    }


    [TestMethod]
    public async Task AddTicketDetails()
    {
        var ticketDetails = new TicketDetails
        {
            TicketId = "123",
            ArticleName = "Test Article",
            ArticleId = 1,
            Quantity = 2,
            Price = 10.0m,
            Date = DateTime.Now
        };
        _tickDetailsRepo.Setup(x => x.SaveAsync(ticketDetails)).ReturnsAsync(ticketDetails);
        var result = await _service.AddAsync(ticketDetails);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);

        _tickDetailsRepo.Verify(x => x.SaveAsync(ticketDetails));
    }

    [TestMethod]
    public async Task DeleteTicketDetails()
    {
        var tickDetails = new TicketDetails
        {
            ArticleId = 1,
            Quantity = 2,
            Date = new DateTime(2025, 06, 25),
            TicketId = "1",
            TicketDetailsId = 1
        };

        _tickDetailsRepo.Setup(X => X.FindAsync(It.IsAny<Expression<Func<TicketDetails, bool>>>()))
            .ReturnsAsync(tickDetails);

        _tickDetailsRepo.Setup(x => x.DeleteAsync(It.IsAny<TicketDetails>()));

        var result = await _service.DeleteAsync(tickDetails.TicketDetailsId);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);

        _tickDetailsRepo.Verify(x => x.DeleteAsync(It.IsAny<TicketDetails>()));
    }
}