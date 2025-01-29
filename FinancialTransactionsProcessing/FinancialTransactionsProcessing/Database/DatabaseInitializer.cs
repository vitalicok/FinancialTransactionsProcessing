using Dapper;
using FinancialTransactionsProcessing.Abstractions;
using Microsoft.Data.SqlClient;

namespace FinancialTransactionsProcessing.Database
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InitializeDatabaseAsync(CancellationToken ct = new())
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Get all .sql files in the folder
            var sqlFiles = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "Database/scripts"), "*.sql")
                                    .OrderBy(f => f) // Sort to ensure execution order (alphabetically)
                                    .ToList();

            foreach (var sqlScript in sqlFiles)
            {
                var script = await File.ReadAllTextAsync(sqlScript, ct);
                await connection.ExecuteAsync(script, ct);
            }
        }
    }
}
