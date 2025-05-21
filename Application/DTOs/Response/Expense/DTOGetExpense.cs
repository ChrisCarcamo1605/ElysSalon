using System.Diagnostics.CodeAnalysis;

namespace Application.DTOs.Response.Expense;

public record DTOGetExpense(
    int ExpenseId,
    decimal Amount,
    string Reason,
    DateTime Date
)
{
}