using PracticeWebApi.CommonClasses.Orders;
using PracticeWebApi.Data.Orders;

namespace PracticeWebApi.Services.Orders
{
    public class OrderMapper : IMapper<Order, OrderDataEntity>
    {
        // not mapping all properties due to complications with storage and not bloating this class with repository logic
        public Order MapToBase(OrderDataEntity dataEntity) => new Order
        {
            Id = dataEntity.Id,
            Status = dataEntity.Status
        };

        public OrderDataEntity MapToDataEntity(Order baseType) => new OrderDataEntity
        {
            Id = baseType.Id,
            Status = baseType.Status,
            UserId = baseType.User.Id
        };
    }
}
