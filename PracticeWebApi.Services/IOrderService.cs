using PracticeWebApi.CommonClasses.Orders;
using PracticeWebApi.Services.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticeWebApi.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(CreateOrderRequest request);
        Task<IList<Order>> FindOrdersByUserId(string id);
        Task<Order> FindOrderById(string id);
        Task AddProductToOrder(string orderId, string productId);
        Task CancelOrder(string userId);
        Task CompleteOrder(string userId);
    }
}
