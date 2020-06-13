using PracticeWebApi.CommonClasses.Orders;
using PracticeWebApi.CommonClasses.Products;
using PracticeWebApi.CommonClasses.Users;
using PracticeWebApi.Data;
using PracticeWebApi.Data.Orders;
using PracticeWebApi.Data.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PracticeWebApi.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper<Order, OrderDataEntity> _orderMapper;
        private readonly IMapper<Product, ProductDataEntity> _productMapper;

        public OrderService(
            IUserService userService, 
            IProductService productService, 
            IOrderRepository orderRepository, 
            IMapper<Order, OrderDataEntity> orderMapper, 
            IMapper<Product, ProductDataEntity> productMapper)
        {
            _userService = userService;
            _productService = productService;
            _orderRepository = orderRepository;
            _orderMapper = orderMapper;
            _productMapper = productMapper;
        }

        public async Task<Order> CreateOrder(CreateOrderRequest request)
        {
            var order = new Order();

            // generate guid id
            order.Id = Guid.NewGuid().ToString();
            order.Status = "Started";

            // because we're just mapping this to data entity, we don't need a full user yet
            order.User = new User { Id = request.UserId };

            // map to data entity for storage - will only map id, userid, and status
            var orderDataEntity = _orderMapper.MapToDataEntity(order);

            foreach(var productId in request.ProductIds)
            {
                // create 'ordered product' object which represents each product on an order by id and order id
                var orderedProduct = new OrderedProductDataEntity
                {
                    OrderId = order.Id,
                    ProductId = productId
                };

                // add each ordered product to the db 
                await _orderRepository.AddProductToOrder(orderedProduct);
            }

            // add order to db
            await _orderRepository.CreateOrder(orderDataEntity);

            // add products and user to order for returning 
            await ApplyProductsToOrder(order);
            await ApplyUserToOrder(order, request.UserId);

            return order;
        }

        public async Task<Order> FindOrderByUserId(string id)
        {
            // find order in db and map to base order
            var orderDataEntity = await _orderRepository.FindOrderByUserId(id);
            var order = _orderMapper.MapToBase(orderDataEntity);

            // find user by userid and add to order
            await ApplyUserToOrder(order, orderDataEntity.UserId);

            // find each product added by order id and add to order
            await ApplyProductsToOrder(order);

            return order;
        }

        private async Task ApplyUserToOrder(Order order, string userId)
        {
            // just grab user by id
            var user = await _userService.FindUserById(userId);

            // and add it to the user
            order.User = user;
        }

        private async Task ApplyProductsToOrder(Order order)
        {
            // get all of the ordered products by order id
            var orderedProducts = await _orderRepository.GetOrderedProductsByOrderId(order.Id);
            var products = new List<Product>();

            // find each product based on orderedProduct's product id
            foreach(var orderedProduct in orderedProducts)
            {
                products.Add(await _productService.FindProductById(orderedProduct.ProductId));
            }

            // add them to the order
            order.Products = products;
        }
    }
}
