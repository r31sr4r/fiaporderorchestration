using System.Collections.Generic;

namespace FIAP.Function.Models
{
    public class Order
    {
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalValue { get; set; }
    }
}
