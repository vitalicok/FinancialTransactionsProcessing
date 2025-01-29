namespace FinancialTransactionsProcessing.Abstractions
{
    public interface IDatabaseInitializer
    {
        Task InitializeDatabaseAsync(CancellationToken ct = new());
    }
}
