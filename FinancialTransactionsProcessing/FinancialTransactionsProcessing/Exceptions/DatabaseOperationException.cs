namespace FinancialTransactionsProcessing.Exceptions
{
    public class DatabaseOperationException : Exception
    {
        public DatabaseOperationException(Exception innerException) : base("Database operation has failed.", innerException)
        {
        }
    }
}
