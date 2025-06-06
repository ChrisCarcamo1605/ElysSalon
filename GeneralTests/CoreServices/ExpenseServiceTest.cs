using System.Collections.ObjectModel;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Services;
using Moq;

namespace GeneralTests.CoreServices;

[TestClass]
public class ExpenseServiceTest
{
    private Mock<IRepository<Expense>> _expenseRepo;
    private ExpenseService _expenseService;

    [TestInitialize]
    public void SetUp()
    {
        _expenseRepo = new Mock<IRepository<Expense>>();
        _expenseService = new ExpenseService(_expenseRepo.Object);
    }


    [TestMethod]
    public async Task AddExpense()
    {
        var expense = new Expense
        {
            Id = 1,
            Amount = 12.32m,
            Date = new DateTime(25, 3, 20),
            Reason = "test"
        };
        _expenseRepo.Setup(x => x.SaveAsync(expense)).ReturnsAsync(It.IsAny<Expense>());

        var result = await _expenseService.AddAsync(expense);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        
        _expenseRepo.Verify(x=> x.SaveAsync(expense));
    }

    [TestMethod]
    public async Task DeleteExpense()
    {
        var expenseId = 1;
        _expenseRepo.Setup(x => x.GetByIdAsync(expenseId)).ReturnsAsync(new Expense { Id = expenseId });
        _expenseRepo.Setup(x => x.DeleteAsync(It.IsAny<Expense>()));
        var result = await _expenseService.DeleteAsync(expenseId);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);

        _expenseRepo.Verify(x => x.DeleteAsync(It.IsAny<Expense>()), Times.Once);
    }

    [TestMethod]
    public async Task GetAllExpenses()
    {
        var expenses = new ObservableCollection<Expense>
        {
            new Expense { Id = 1, Amount = 12.32m, Date = new DateTime(2023, 3, 20), Reason = "test" }
        };
        _expenseRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(expenses);
        var result = await _expenseService.GetAllOfAsync();
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        Assert.IsInstanceOfType(result.Data, typeof(ObservableCollection<Expense>));

        _expenseRepo.Verify(x => x.GetAllAsync(), Times.Once);
    }
}