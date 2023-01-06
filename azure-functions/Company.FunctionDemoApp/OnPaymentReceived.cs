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
        var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        var order = JsonConvert.DeserializeObject<Order>(requestBody);

        logger.LogInformation(
            "Received a payment for {OrderId} order id with {ProductId} product id.",
            order.Id, order.ProductId);

        return new OkObjectResult("Thank you for your purchase");
    }
}

public class Order
{
    public string Id { get; init; } = "";
    public string ProductId { get; init; } = "";
    public string Email { get; init; } = "";
    public decimal Price { get; init; }
}
