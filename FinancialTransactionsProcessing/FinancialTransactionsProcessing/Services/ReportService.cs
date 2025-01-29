using FinancialTransactionsProcessing.Abstractions;
using FinancialTransactionsProcessing.Exceptions;
using FinancialTransactionsProcessing.Models;

namespace FinancialTransactionsProcessing.Services
{
    public class ReportService : IReportService
    {
        private readonly IDatabaseHandler _databaseHandler;

        public ReportService(IDatabaseHandler databaseHandler)
            => _databaseHandler = databaseHandler;

        public async Task<JsonReport> GenerateReportAsync()
        {
            if (_databaseHandler is null)
                throw new DatabaseConfigurationException();

            var userSummary = await _databaseHandler.GetIncomeExpense();
            var topCategories = await _databaseHandler.GetTopCategories();
            var highestSpender = await _databaseHandler.GetHighestSpender();

            return new JsonReport
            {
                UsersSummary = userSummary.ToList(),
                TopCategories = topCategories.ToList(),
                HighestSpender = highestSpender
            };
        }
    }
}
