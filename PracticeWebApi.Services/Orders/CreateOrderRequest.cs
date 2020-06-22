using System.Collections;
using System.Collections.Generic;

namespace PracticeWebApi.Services.Orders
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; }
        // productId, count
        public Dictionary<string, int> ProductIds { get; set; }
    }
}
