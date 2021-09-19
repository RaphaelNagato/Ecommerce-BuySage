using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly IBasketRepository _basketRepo;
        private readonly IGenericRepository<Product> _productRepo;

        public OrderService(IGenericRepository<Order> orderRepo,
                            IGenericRepository<DeliveryMethod> deliveryMethodRepo,
                            IBasketRepository basketRepo,
                            IGenericRepository<Product> productRepo)
        {
            _orderRepo = orderRepo;
            _deliveryMethodRepo = deliveryMethodRepo;
            _basketRepo = basketRepo;
            _productRepo = productRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _productRepo.GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, item.Quantity, productItem.Price);
                items.Add(orderItem);
            }

            var deliveryMethod = await _deliveryMethodRepo.GetByIdAsync(deliveryMethodId);
            var subTotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(items, deliveryMethod, buyerEmail, shippingAddress, subTotal);

            //TODO: Save order to database

            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUser(string buyerEmail)
        {
            throw new System.NotImplementedException();
        }
    }
}