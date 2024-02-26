using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FIAP.Function.Models;
using Newtonsoft.Json;
using System.Net;

namespace FIAP.Function;

public static class HttpStart
{
        [FunctionName("FiapOrderApprovalOrchestrator_HttpStart")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Deserialize the request content to Order object
            var content = await req.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Order>(content);

            if (order == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a valid order in the request body");
            }

            string instanceId = await starter.StartNewAsync("FiapOrderApprovalOrchestrator", order);

            log.LogInformation($"Started orchestration with ID = '{instanceId}' for order.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }

}
