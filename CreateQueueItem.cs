using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class CreateQueueItem
    {
        [FunctionName("CreateQueueItem")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Queue("CreateItem", Connection = "AzureWebJobsStorage")]IAsyncCollector<string> queueItem,
            ILogger log)
        {
            log.LogInformation("Function started a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            await queueItem.AddAsync(requestBody);

            log.LogInformation("Function finished a request.");

            return new OkObjectResult("Thanks!");
        }
    }
}
