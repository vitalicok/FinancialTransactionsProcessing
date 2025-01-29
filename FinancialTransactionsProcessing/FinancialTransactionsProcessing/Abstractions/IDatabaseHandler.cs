using FinancialTransactionsProcessing.Models;

namespace FinancialTransactionsProcessing.Abstractions
{
    public interface IDatabaseHandler
    {
        Task AddTransactionsAsync(IReadOnlyCollection<Transaction> transactions, CancellationToken ct = new());
        Task<IEnumerable<UserSummary>> GetIncomeExpense();
        Task<IEnumerable<TopCategory>> GetTopCategories();
        Task<HighestSpender?> GetHighestSpender();
    }
}
