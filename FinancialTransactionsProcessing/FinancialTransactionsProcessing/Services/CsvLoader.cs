using CsvHelper;
using FinancialTransactionsProcessing.Abstractions;
using FinancialTransactionsProcessing.Configurations;
using FinancialTransactionsProcessing.Models;
using System.Globalization;

namespace FinancialTransactionsProcessing.Services
{
    public class CsvLoader : ICsvLoader
    {
        private readonly CsvManagerConfiguration _csvConfiguration;
        private readonly IDatabaseHandler _databaseHandler;

        public CsvLoader(IDatabaseHandler databaseHandler, CsvManagerConfiguration csvConfiguration)
        {
            _csvConfiguration = csvConfiguration;
            _databaseHandler = databaseHandler;
        }

        public async Task LoadCsvToDbAsync(string csvFilePath)
        {
            if (string.IsNullOrEmpty(csvFilePath))
                throw new ArgumentException("CSV file path cannot be null or empty", nameof(csvFilePath));

            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var transactions = new List<Transaction>();

            while (await csv.ReadAsync())
            {
                var record = csv.GetRecord<Transaction>();
                transactions.Add(record);

                if (transactions.Count == _csvConfiguration.BatchSize)
                {
                    await _databaseHandler.AddTransactionsAsync(transactions);
                    transactions.Clear();
                }
            }

            // Insert if any transactions are left
            if (transactions.Any())
                await _databaseHandler.AddTransactionsAsync(transactions);
        }
    }
}
