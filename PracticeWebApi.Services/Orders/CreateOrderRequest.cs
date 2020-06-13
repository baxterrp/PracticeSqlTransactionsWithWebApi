using System.Collections;
using System.Collections.Generic;

namespace PracticeWebApi.Services.Orders
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; }
        public IEnumerable<string> ProductIds { get; set; }
    }
}
