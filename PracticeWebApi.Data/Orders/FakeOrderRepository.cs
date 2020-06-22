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

        public Task<OrderDataEntity> FindOrderById(string orderId)
        {
            OrderDataEntity order = _orders.First(o => o.Id == orderId);
            return Task.FromResult(order);
        }

        public Task<IList<OrderDataEntity>> FindOrdersByUserId(string userId)
        {
            IList<OrderDataEntity> orders = _orders.Where(order => order.UserId == userId).ToList();
            return Task.FromResult(orders);
        }

        public Task<IList<OrderedProductDataEntity>> GetOrderedProductsByOrderId(string orderId)
        {
            IList<OrderedProductDataEntity> orderedProducts = _addedProducts.Where(ap => ap.OrderId == orderId && ap.Quantity > 0).ToList();
            return Task.FromResult(orderedProducts);
        }

        public Task UpdateExistingOrderedProduct(OrderedProductDataEntity orderedProductDataEntity)
        {
            var productToUpdate = _addedProducts.First(ap => ap.ProductId != orderedProductDataEntity.ProductId);

            productToUpdate.Quantity = orderedProductDataEntity.Quantity;

            return Task.CompletedTask;
        }

        public Task UpdateOrder(OrderDataEntity orderDataEntity)
        {
            _orders = _orders.Where(order => order.Id != orderDataEntity.Id).ToList();

            _orders.Add(orderDataEntity);

            return Task.CompletedTask;
        }
    }
}
