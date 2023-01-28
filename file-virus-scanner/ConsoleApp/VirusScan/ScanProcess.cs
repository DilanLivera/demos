using System.Diagnostics;
using System.IO.Abstractions;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.VirusScan;

public class ScanProcess
{
    private const string ProcessArguments = "-Scan -ScanType 3 -File \"{FilePath}\"";
    private const string ProcessPath = @"C:\Program Files\Windows Defender\MpCmdRun.exe";
    private readonly IFileSystem _fileSystem;
    private readonly ILogger<ScanProcess> _logger;

    public ScanProcess(
        IFileSystem fileSystem,
        ILogger<ScanProcess> logger)
    {
        _fileSystem = fileSystem;
        _logger = logger;
    }

    public async Task<string> Run(FilePath filePath)
    {
        if (!_fileSystem.File.Exists(filePath.Value))
        {
            throw new IOException($"{filePath} not found.");
        }

        /*
         * Resources
         * https://knowledge.broadcom.com/external/article/151455/scan-endpoint-protection-clients-from-a.html
         * https://community.broadcom.com/symantecenterprise/communities/community-home/librarydocuments/viewdocument?DocumentKey=291116c2-dc4a-4321-b92f-63711fba404e
         */
        var arguments = ProcessArguments.Replace(
            oldValue: "{FilePath}",
            newValue: filePath.Value);

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = ProcessPath,
                Arguments = arguments,
                RedirectStandardOutput = true
            }
        };

        try
        {
            _logger.LogInformation(
                "Executing Scan for file {File}",
                filePath);

            process.Start();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new Exception(
                    $"Virus scan process exited with unknown(code: {process.ExitCode}) exit code.");
            }

            var output = await process.StandardOutput.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(output))
            {
                throw new Exception(
                    $"Scan of {filePath} file did not return any output");
            }

            _logger.LogInformation(
                "Virus scan process output: {Output}",
                output);

            return output;
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Virus scan did not complete successfully due to an exception");

            throw;
        }
    }
}
