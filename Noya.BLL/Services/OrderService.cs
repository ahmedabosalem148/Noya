using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;

namespace Noya.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
        }

        public IEnumerable<Order> GetAll() => _orderRepository.GetAll();

        public Order GetById(int id) => _orderRepository.GetById(id);

        public void Create(Order order) => _orderRepository.Add(order);

        public void Update(Order order) => _orderRepository.Update(order);

        public void Delete(int id) => _orderRepository.Delete(id);
    }
}
