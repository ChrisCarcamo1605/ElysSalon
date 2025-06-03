using Core.Domain.Entities;

namespace Application.DTOs.Response.Expenses;

public record DTOGetExpense(
    int ExpenseId,
    decimal Amount,
    string Reason,
    DateTime Date
)
{
    public DTOGetExpense() : this(0, 0, string.Empty, DateTime.MinValue)
    {
    }
    public DTOGetExpense(Expense expense) : this(
        expense.Id,
        expense.Amount,
        expense.Reason,
        expense.Date
    )
    {
    }
}