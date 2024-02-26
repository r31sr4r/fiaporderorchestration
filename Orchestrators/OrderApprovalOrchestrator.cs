using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using FIAP.Function.Models;
using System.Threading.Tasks;

namespace FIAP.Function.Orchestrators
{
    public static class OrderApprovalOrchestrator
    {
        [FunctionName("FiapOrderApprovalOrchestrator")]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var order = context.GetInput<Order>();

            await context.CallActivityAsync("ProcessOrder", order);
            await context.CallActivityAsync("ProcessPayment", order);
            var shippingTicket = await context.CallActivityAsync<string>("OpenShippingTicket", order);
            await context.CallActivityAsync("FinalizeOrder", new { Order = order, ShippingTicket = shippingTicket });
        }
    }
}