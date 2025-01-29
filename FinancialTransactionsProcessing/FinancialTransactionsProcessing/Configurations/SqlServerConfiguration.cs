using Newtonsoft.Json;

namespace FinancialTransactionsProcessing.Configurations
{
    public class SqlServerConfiguration
    {
        public const string SectionName = "SqlServer";

        [JsonProperty("DefaultConnection")]
        public string ConnectionString { get; set; }
    }
}
