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
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
        HttpRequest request,
        ILogger logger)
    {
        logger.LogInformation("Received a payment");

        var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);

        return new OkObjectResult("Thank you for your purchase");
    }
}
