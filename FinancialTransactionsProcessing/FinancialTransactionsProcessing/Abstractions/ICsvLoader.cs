namespace FinancialTransactionsProcessing.Abstractions
{
    public interface ICsvLoader
    {
        Task LoadCsvToDbAsync(string csvFilePath);
    }
}
