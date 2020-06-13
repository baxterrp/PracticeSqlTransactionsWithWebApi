using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticeWebApi.Data.Orders
{
    public class OrderRepository : IOrderRepository
    {
        public Task AddProductToOrder(OrderedProductDataEntity orderedProductDataEntity)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateOrder(OrderDataEntity order)
        {
            throw new System.NotImplementedException();
        }

        public Task<OrderDataEntity> FindOrderByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<OrderedProductDataEntity>> GetOrderedProductsByOrderId(string orderId)
        {
            throw new System.NotImplementedException();
        }
    }
}
