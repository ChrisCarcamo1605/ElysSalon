using Application.DTOs.Request.SalesData;
using Application.DTOs.Request.Tickets;
using Application.DTOs.Request.TicketsDetails;
using Application.DTOs.Response.Expenses;
using Application.DTOs.Response.SalesData;
using Application.Services;
using AutoMapper;
using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Services;
using Moq;
using System.Collections.ObjectModel;

namespace GeneralTests.AppServices
{
    [TestClass]
    public class SaleDataAppServiceTests
    {
        private Mock<ISalesService> _salesServiceMock;
        private Mock<IExpensesService> _expServiceMock;
        private Mock<ITicketService> _ticketServiceMock;
        private Mock<ITicketDetailsService> _tDetailsServiceMock;
        private Mock<IMapper> _mapperMock;
        private SaleDataAppService _saleDataAppService;

        [TestInitialize]
        public void Initialize()
        {
            _salesServiceMock = new Mock<ISalesService>();
            _expServiceMock = new Mock<IExpensesService>();
            _ticketServiceMock = new Mock<ITicketService>();
            _tDetailsServiceMock = new Mock<ITicketDetailsService>();
            _mapperMock = new Mock<IMapper>();

            _saleDataAppService = new SaleDataAppService(
                _salesServiceMock.Object,
                _expServiceMock.Object,
                _ticketServiceMock.Object,
                _tDetailsServiceMock.Object,
                _mapperMock.Object);
        }

        #region Add Tests

        [TestMethod]
        public async Task Add_WithSales_ShouldCallSalesService()
        {
            // Arrange
            var sales = new Sales();
            var expectedResult = ResultFromService.SuccessResult(sales);
            _salesServiceMock.Setup(x => x.AddAsync(sales)).ReturnsAsync(expectedResult);

            // Act
            var result = await _saleDataAppService.Add<Sales>(sales);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(expectedResult, result);
            _salesServiceMock.Verify(x => x.AddAsync(sales), Times.Once());
        }

        [TestMethod]
        public async Task Add_WithDtoCreateTicket_ShouldMapAndCallTicketService()
        {
            // Arrange
            var dtoTicket = new DtoCreateTicket("1", new DateTime(2025, 06, 07), "messi", 12.25m);
            var ticket = new Ticket();
            var savedTicketResult = ResultFromService.SuccessResult(ticket);

            _mapperMock.Setup(x => x.Map<Ticket>(dtoTicket)).Returns(ticket);
            _ticketServiceMock.Setup(x => x.AddAsync(ticket)).ReturnsAsync(savedTicketResult);

            // Act
            var result = await _saleDataAppService.Add<DtoCreateTicket>(dtoTicket);

            // Assert
            Assert.IsTrue(result.Success);
            _mapperMock.Verify(x => x.Map<Ticket>(dtoTicket), Times.Once());
            _ticketServiceMock.Verify(x => x.AddAsync(ticket), Times.Once());
        }

        [TestMethod]
        public async Task Add_WithUnsupportedType_ShouldReturnFailedResult()
        {
            // Arrange
            var unsupportedObj = new object();

            // Act
            var result = await _saleDataAppService.Add<object>(unsupportedObj);

            // Assert
            Assert.IsFalse(result.Success);
            StringAssert.Contains(result.Message, "Tipo no soportado");
        }

        #endregion

        #region AddTicketDetailsRange Tests

        [TestMethod]
        public async Task AddTicketDetailsRange_WithValidDetails_ShouldCallService()
        {
            // Arrange
            var ticketDetails = new List<DtoCreateTicketDetails>
            {
                new DtoCreateTicketDetails
                {
                    Ticket = new Ticket { TicketId = "1" },
                    Article = new Core.Domain.Entities.Article { ArticleId = 1, Name = "Test" },
                    Date = System.DateTime.Now,
                    Quantity = 1,
                    Price = 10.0m
                }
            };

            var lastTicketResult = ResultFromService.SuccessResult(new Ticket { TicketId = "1" });
            var addRangeResult = ResultFromService.SuccessResult();

            _ticketServiceMock.Setup(x => x.GetLastIdAsync()).ReturnsAsync(lastTicketResult);
            _tDetailsServiceMock.Setup(x => x.AddRange(It.IsAny<List<TicketDetails>>())).ReturnsAsync(addRangeResult);

            // Act
            var result = await _saleDataAppService.AddTicketDetailsRange(ticketDetails);

            // Assert
            Assert.IsTrue(result.Success);
            _tDetailsServiceMock.Verify(x => x.AddRange(It.Is<List<TicketDetails>>(l =>
                l.All(td => td.TicketId == "1"))), Times.Once());
        }

        #endregion

        #region Delete Tests

        [TestMethod]
        public async Task Delete_WithSalesType_ShouldCallSalesService()
        {
            // Arrange
            var id = "123";
            _salesServiceMock.Setup(x => x.DeleteAsync(id));

            // Act
            var result = await _saleDataAppService.Delete<DTOGetSales>(id);

            // Assert
            Assert.IsTrue(result.Success);
            _salesServiceMock.Verify(x => x.DeleteAsync(id), Times.Once());
        }

        [TestMethod]
        public async Task Delete_WhenServiceThrowsException_ShouldReturnFailedResult()
        {
            // Arrange
            var id = "123";
            _salesServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new Exception("Error de prueba"));

            // Act
            var result = await _saleDataAppService.Delete<DTOGetSales>(id);

            // Assert
            Assert.IsFalse(result.Success);
            StringAssert.Contains(result.Message, "Error al eliminar");
        }

        #endregion

        #region GetAllOf Tests

        [TestMethod]
        public async Task GetAllOf_WithSalesType_ShouldReturnSalesData()
        {
            // Arrange
            var sales = new ObservableCollection<Sales> { new Sales() };
            var serviceResult = ResultFromService.SuccessResult(sales);

            _salesServiceMock.Setup(x => x.GetAllOfAsync()).ReturnsAsync(serviceResult);

            // Act
            var result = await _saleDataAppService.GetAllOf<DTOGetSales>();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsInstanceOfType(result.Data, typeof(ObservableCollection<DTOSalesData>));
        }

        [TestMethod]
        public async Task GetAllOf_WithUnsupportedType_ShouldReturnFailedResult()
        {
            // Act
            var result = await _saleDataAppService.GetAllOf<object>();

            // Assert
            Assert.IsFalse(result.Success);
            StringAssert.Contains(result.Message, "Tipo no soportado");
        }

        #endregion

        #region GetLastIdTicket Tests

        [TestMethod]
        public async Task GetLastIdTicket_WhenTicketExists_ShouldReturnData()
        {
            // Arrange
            var ticket = new Ticket { TicketId = "1" };
            var serviceResult = ResultFromService.SuccessResult(ticket);

            _ticketServiceMock.Setup(x => x.GetLastIdAsync()).ReturnsAsync(serviceResult);

            // Act
            var result = await _saleDataAppService.GetLastIdTicket();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsInstanceOfType(result.Data, typeof(DTOSalesData));
            Assert.AreEqual(ticket.TicketId, ((DTOSalesData)result.Data).Id);
        }

        #endregion
    }
}