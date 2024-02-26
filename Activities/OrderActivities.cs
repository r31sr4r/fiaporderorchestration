using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using FIAP.Function.Models;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Threading.Tasks;

namespace FIAP.Function.Activities
{
    public static class OrderActivities
    {

        [FunctionName("ProcessOrder")]
        public static async Task ProcessOrder([ActivityTrigger] Order order, ILogger log)
        {
            log.LogInformation($"Order received: {order.CustomerName}, Total: {order.TotalValue}");
            // Simula um delay de processamento
            await Task.Delay(5000); // Delay de 5 segundos
        }

        [FunctionName("ProcessPayment")]
        public static async Task ProcessPayment([ActivityTrigger] Order order, ILogger log)
        {
            log.LogInformation($"Processing payment for Order: {order.CustomerName}, Total: {order.TotalValue}");
            // Simula um delay de processamento
            await Task.Delay(5000); // Delay de 5 segundos
        }

        [FunctionName("OpenShippingTicket")]
        public static async Task<string> OpenShippingTicket([ActivityTrigger] Order order, ILogger log)
        {
            var ticketNumber = Guid.NewGuid().ToString(); // Gera um GUID único
            log.LogInformation($"Opening shipping ticket for: {order.DeliveryAddress} with Ticket Number: {ticketNumber}");
            // Simula um delay de processamento
            await Task.Delay(5000); // Delay de 5 segundos
            return ticketNumber;
        }

        [FunctionName("FinalizeOrder")]
        public static async Task FinalizeOrder([ActivityTrigger] IDurableActivityContext context, ILogger log)
        {
            // Obtenha o input como um objeto
            var input = context.GetInput<object>();

            // Converta o objeto para uma string JSON
            string jsonString = JsonConvert.SerializeObject(input);

            // Deserializa a string JSON para um objeto dinâmico
            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonString);

            // Deserializa partes específicas para os tipos apropriados
            Order order = JsonConvert.DeserializeObject<Order>(data.Order.ToString());
            string shippingTicket = data.ShippingTicket;

            if (order == null || shippingTicket == null)
            {
                log.LogWarning("Order or shipping ticket is null.");
                return; // Early return to avoid null reference exception
            }

            log.LogInformation($"Order finalized: {order.CustomerName}, Shipping Ticket: {shippingTicket}");
            // Finalize order processing
            await Task.Delay(5000); // Delay de 5 segundos
        }
    }
}