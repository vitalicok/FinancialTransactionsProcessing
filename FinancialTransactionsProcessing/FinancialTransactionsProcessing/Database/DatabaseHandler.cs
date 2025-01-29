using Dapper;
using FinancialTransactionsProcessing.Abstractions;
using FinancialTransactionsProcessing.Exceptions;
using FinancialTransactionsProcessing.Models;
using Microsoft.Data.SqlClient;
using Z.Dapper.Plus;

namespace FinancialTransactionsProcessing.Database
{
    public class DatabaseHandler : IDatabaseHandler
    {
        private readonly string _connectionString;

        public DatabaseHandler(string connectionString)
            => _connectionString = connectionString;

        public async Task AddTransactionsAsync(IReadOnlyCollection<Transaction> transactions, CancellationToken ct = new())
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync();
            var dapperPlusContext = new DapperPlusContext { Connection = connection, Transaction = transaction };

            try
            {
                await transaction.BulkInsertAsync(transactions);

                await transaction.CommitAsync(ct);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<IEnumerable<UserSummary>> GetIncomeExpense()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var userSummaries = await connection.QueryAsync<UserSummary>(Constants.DB.Queries.IncomeExpenseQuery);

            return userSummaries;
        }

        public async Task<IEnumerable<TopCategory>> GetTopCategories()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var topCategories = await connection.QueryAsync<TopCategory>(Constants.DB.Queries.TopCategoriesQuery);

            return topCategories;
        }

        public async Task<HighestSpender?> GetHighestSpender()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var highestSpender = await connection.QuerySingleOrDefaultAsync<HighestSpender>(Constants.DB.Queries.HighestSpenderQuery);

            return highestSpender;
        }
    }
}
