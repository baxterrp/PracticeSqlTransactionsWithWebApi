using Microsoft.AspNetCore.Mvc;
using PracticeWebApi.Services;
using PracticeWebApi.Services.Orders;
using System;
using System.Threading.Tasks;

namespace PracticeWebApi.Web.Controllers
{
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpDelete("/orders/{orderId}")]
        public async Task<IActionResult> CancelOrder([FromRoute]string orderId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("/orders/{productId}")]
        public async Task<IActionResult> AddProductToOrder([FromRoute] string productId) 
        {
            throw new NotImplementedException();
        }

        [HttpPost("/orders/{orderId}")]
        public async Task<IActionResult> CompleteOrder([FromRoute]string orderId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("/orders")]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderRequest request)
        {
            try
            {
                var addedOrder = await _orderService.CreateOrder(request);

                return Ok(addedOrder);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("/orders/{userId}")]
        public async Task<IActionResult> FindOrderByUserId([FromRoute] string userId) 
        {
            try
            {
                var order = await _orderService.FindOrderByUserId(userId);

                return Ok(order);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
