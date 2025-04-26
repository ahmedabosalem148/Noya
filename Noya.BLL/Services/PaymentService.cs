using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;

namespace Noya.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentService()
        {
            _paymentRepository = new PaymentRepository();
        }

        public IEnumerable<Payment> GetAll() => _paymentRepository.GetAll();

        public Payment GetById(int id) => _paymentRepository.GetById(id);

        public void Create(Payment payment) => _paymentRepository.Add(payment);

        public void Update(Payment payment) => _paymentRepository.Update(payment);

        public void Delete(int id) => _paymentRepository.Delete(id);
    }
}
