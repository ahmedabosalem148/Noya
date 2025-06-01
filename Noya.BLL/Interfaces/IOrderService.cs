using System.Collections.Generic;
using Noya.Models;

namespace Noya.BLL.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        IEnumerable<Order> GetOrdersByCustomerId(int customerId);
        void Create(Order order);
        void Update(Order order);
        void Delete(int id);
        Order ProcessCheckout(CheckoutRequest checkout);
    }
}
