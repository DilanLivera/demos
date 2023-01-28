using Microsoft.Extensions.Logging;

namespace ConsoleApp.VirusScan;

public class CheckScanResultByPatternMatching
{
    private const string Pattern = "found no threats";
    private readonly ILogger<CheckScanResultByPatternMatching> _logger;
    private readonly ScanProcess _scanProcess;

    public CheckScanResultByPatternMatching(
        ILogger<CheckScanResultByPatternMatching> logger,
        ScanProcess scanProcess)
    {
        _logger = logger;
        _scanProcess = scanProcess;
    }

    public async Task<bool> HasThreats(FilePath filePath)
    {
        var output = await _scanProcess.Run(filePath);

        if (output.Contains(Pattern))
        {
            return false;
        }

        _logger.LogInformation(
            "Threats found in {File} using pattern matching",
            filePath);

        return true;
    }
}
