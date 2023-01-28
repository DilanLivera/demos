using System.IO.Abstractions;
using ConsoleApp;
using ConsoleApp.VirusScan;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("---File Virus Scanner---");

ServiceProvider serviceProvider = new ServiceCollection()
    .AddScoped<IFileSystem, FileSystem>()
    .AddLogging(builder =>
    {
        builder.SetMinimumLevel(LogLevel.Debug);
        builder.AddConsole();
    })
    .AddScoped<CheckScanResultByPatternMatching>()
    .AddScoped<ScanProcess>()
    .BuildServiceProvider();

var checkScanResultByPatternMatching = serviceProvider
    .GetRequiredService<CheckScanResultByPatternMatching>();

var filePath = new FilePath(value: @"C:\Users\dilan\Downloads\git.png");

var hasThreats = await checkScanResultByPatternMatching.HasThreats(filePath);

var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

if (hasThreats)
{
    logger.LogInformation("File is not clean");
}
else
{
    logger.LogInformation("File is clean");
}

logger.LogInformation("---End---");
