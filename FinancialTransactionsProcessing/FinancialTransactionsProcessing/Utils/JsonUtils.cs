using System.Text.Json;

namespace FinancialTransactionsProcessing.Utils
{
    public static class JsonUtils
    {
        public static async Task SaveReportToJsonAsync(object report, string filePath, CancellationToken ct = new())
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(filePath) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(report, options);

            await File.WriteAllTextAsync(filePath, json, ct);
        }
    }
}
