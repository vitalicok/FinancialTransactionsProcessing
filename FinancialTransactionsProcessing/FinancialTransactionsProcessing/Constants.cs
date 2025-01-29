namespace FinancialTransactionsProcessing
{
    public static class Constants
    {
        public static class DB 
        {
            public static class Queries
            {
                public const string IncomeExpenseQuery = @"
                                                      SELECT t.UserId, 
                                                             SUM(CASE WHEN t.Amount > 0 THEN t.Amount ELSE 0 END) AS TotalIncome,
                                                             SUM(CASE WHEN t.Amount < 0 THEN t.Amount ELSE 0 END) AS TotalExpense
                                                      FROM Transactions t
                                                      GROUP BY t.UserId";
                public const string TopCategoriesQuery = @"
                                                      SELECT TOP 3 t.Category, COUNT(*) AS TransactionsCount
                                                      FROM Transactions t
                                                      GROUP BY t.Category
                                                      ORDER BY COUNT(*) DESC";
                public const string HighestSpenderQuery = @"
                                                      SELECT TOP 1 t.UserId, SUM(Amount) AS TotalExpense
                                                      FROM Transactions t
                                                      WHERE Amount < 0
                                                      GROUP BY t.UserId
                                                      ORDER BY SUM(Amount) ASC";
                public const string InsertTransaction = @"
                                                      INSERT INTO Transactions (TransactionId, UserId, Date, Amount, Category, Description, Merchant)
                                                      VALUES (@TransactionId, @UserId, @Date, @Amount, @Category, @Description, @Merchant)";
            }
        }
    }
}
