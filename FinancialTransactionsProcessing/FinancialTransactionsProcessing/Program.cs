using FinancialTransactionsProcessing.Abstractions;
using FinancialTransactionsProcessing.Configurations;
using FinancialTransactionsProcessing.Database;
using FinancialTransactionsProcessing.Services;
using FinancialTransactionsProcessing.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{ 
    static async Task Main(string[] args)
    {
        // Configure DI
        var serviceProvider = ConfigureServices();

        // Retrieve configuration from serviceProvider
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        // Resolve dependencies
        var csvLoader = serviceProvider.GetRequiredService<ICsvLoader>();
        var databaseHandler = serviceProvider.GetRequiredService<IDatabaseHandler>();
        var databaseInitializer = serviceProvider.GetRequiredService<IDatabaseInitializer>();
        var reportService = serviceProvider.GetRequiredService<IReportService>();

        string dataPath = Path.Combine(AppContext.BaseDirectory, configuration.GetValue<string>("FilePaths:DataPath") ?? Environment.GetEnvironmentVariable("DATA_PATH") ?? "data");
        string projectPath = Path.Combine(AppContext.BaseDirectory, configuration.GetValue<string>("FilePaths:ProjectPath") ?? Environment.GetEnvironmentVariable("PROJECT_PATH") ?? ".");

        string csvFilePath = Path.Combine(dataPath, configuration.GetValue<string>("CsvManager:CsvDataPath") ?? "transactions.csv"); // Default if not in config
        string outputPath = Path.Combine(projectPath, configuration.GetValue<string>("CsvManager:ReportOutputPath") ?? "report.json");

        // Step 0: Initialize database

        await databaseInitializer.InitializeDatabaseAsync();

        // Step 1: Load CSV and insert into DB

        await csvLoader.LoadCsvToDbAsync(csvFilePath);

        // Step 2: Perform report 

        var report = await reportService.GenerateReportAsync();

        // Step 3: Save report to JSON

        await JsonUtils.SaveReportToJsonAsync(report, outputPath);

        Console.WriteLine("Analysis complete. Report saved at: " + outputPath);
        //Console.ReadKey();
    }

    private static ServiceProvider ConfigureServices()
    {
        // Load configuration from appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) // Set the base path to the application's directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Load appsettings.json
            .Build();

        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                                ?? configuration.GetSection("SqlServer:DefaultConnection").Value;

        // Configure DI container
        var services = new ServiceCollection();

        // Bind configuration
        var csvManagerConfiguration = new CsvManagerConfiguration();
        configuration.GetSection(CsvManagerConfiguration.SectionName).Bind(csvManagerConfiguration);
        services.AddSingleton(csvManagerConfiguration);

        // Register services
        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<IDatabaseInitializer>(provider => new DatabaseInitializer(connectionString!));
        services.AddScoped<IDatabaseHandler>(provider => new DatabaseHandler(connectionString!));
        services.AddSingleton<ICsvLoader, CsvLoader>();
        services.AddScoped<IReportService, ReportService>();

        // Build and return the service provider
        return services.BuildServiceProvider();
    }
}