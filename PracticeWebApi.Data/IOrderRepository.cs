using PracticeWebApi.Data.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticeWebApi.Data
{
    public interface IOrderRepository
    {
        Task CreateOrder(OrderDataEntity order);
        Task<OrderDataEntity> FindOrderById(string orderId);
        Task<IList<OrderDataEntity>> FindOrdersByUserId(string userId);
        Task AddProductToOrder(OrderedProductDataEntity orderedProductDataEntity);
        Task<IList<OrderedProductDataEntity>> GetOrderedProductsByOrderId(string orderId);
        Task UpdateOrder(OrderDataEntity orderDataEntity);
    }
}
