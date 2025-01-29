namespace FinancialTransactionsProcessing.Models
{
    public class JsonReport
    {
        public List<UserSummary> UsersSummary { get; set; }
        public List<TopCategory> TopCategories { get; set; }
        public HighestSpender? HighestSpender { get; set; }
    }
}
