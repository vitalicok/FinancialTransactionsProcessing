namespace FinancialTransactionsProcessing.Exceptions
{
    public class DatabaseConfigurationException : Exception
    {
        public DatabaseConfigurationException() : base("Database is misconfigured, please check your connection string")
        {
        }
    }
}
