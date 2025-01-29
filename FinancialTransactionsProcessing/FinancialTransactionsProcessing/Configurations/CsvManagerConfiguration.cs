using Newtonsoft.Json;

namespace FinancialTransactionsProcessing.Configurations
{
    public class CsvManagerConfiguration
    {
        public const string SectionName = "CsvManager";

        [JsonProperty("CsvDataPath")]
        public string CsvDataPath { get; set; }

        [JsonProperty("ReportOutputPath")]
        public string ReportOutputPath { get; set; }

        [JsonProperty("BatchSize")]
        public int BatchSize { get; set; }
    }
}
