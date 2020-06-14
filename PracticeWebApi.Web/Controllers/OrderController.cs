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
            await _orderService.CancelOrder(orderId);

            return Ok();
        }

        [HttpPut("/orders/{orderId}/{productId}")]
        public async Task<IActionResult> AddProductToOrder([FromRoute]string orderId, [FromRoute] string productId) 
        {
            await _orderService.AddProductToOrder(orderId, productId);

            return Ok();
        }

        [HttpPost("/orders/{orderId}")]
        public async Task<IActionResult> CompleteOrder([FromRoute]string orderId)
        {
            await _orderService.CompleteOrder(orderId);

            return Ok();
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
        public async Task<IActionResult> FindOrdersByUserId([FromRoute] string userId) 
        {
            try
            {
                var orders = await _orderService.FindOrdersByUserId(userId);

                return Ok(orders);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
