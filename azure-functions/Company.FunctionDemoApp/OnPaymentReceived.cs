using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.FunctionDemoApp;

public static class OnPaymentReceived
{
    [FunctionName("OnPaymentReceived")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
        HttpRequest request,
        ILogger logger)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");

        string name = request.Query["name"];

        var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        name = name ?? data?.name;

        return name != null
            ? new OkObjectResult($"Hello, {name}")
            : new BadRequestObjectResult(
                "Please pass a name on the query string or in the request body");
    }
}
