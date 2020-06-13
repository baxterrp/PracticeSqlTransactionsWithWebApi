using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeWebApi.Data.Orders
{
    public class FakeOrderRepository : IOrderRepository
    {
        private IList<OrderDataEntity> _orders = new List<OrderDataEntity>();
        private IList<OrderedProductDataEntity> _addedProducts = new List<OrderedProductDataEntity>();

        public Task AddProductToOrder(OrderedProductDataEntity orderedProductDataEntity)
        {
            _addedProducts.Add(orderedProductDataEntity);

            return Task.CompletedTask;
        }

        public Task CreateOrder(OrderDataEntity order)
        {
            _orders.Add(order);

            return Task.CompletedTask;
        }

        public Task<OrderDataEntity> FindOrderByUserId(string userId)
        {
            return Task.FromResult(_orders.Where(order => order.UserId == userId).FirstOrDefault());
        }

        public Task<IList<OrderedProductDataEntity>> GetOrderedProductsByOrderId(string orderId)
        {
            IList<OrderedProductDataEntity> orderedProducts = _addedProducts.Where(ap => ap.OrderId == orderId).ToList();
            return Task.FromResult(orderedProducts);
        }
    }
}
