using PracticeWebApi.CommonClasses.Orders;
using PracticeWebApi.Services.Orders;
using System.Threading.Tasks;

namespace PracticeWebApi.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(CreateOrderRequest request);
        Task<Order> FindOrderByUserId(string id);
    }
}
