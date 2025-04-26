using System.Collections.Generic;
using Noya.Models;

namespace Noya.BLL.Interfaces
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetAll();
        Payment GetById(int id);
        void Create(Payment payment);
        void Update(Payment payment);
        void Delete(int id);
    }
}
