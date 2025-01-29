using FinancialTransactionsProcessing.Models;

namespace FinancialTransactionsProcessing.Abstractions
{
    public interface IReportService
    {
        Task<JsonReport> GenerateReportAsync();
    }
}
