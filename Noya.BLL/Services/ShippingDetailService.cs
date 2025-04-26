using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;

namespace Noya.BLL.Services
{
    public class ShippingDetailService : IShippingDetailService
    {
        private readonly ShippingDetailRepository _shippingDetailRepository;

        public ShippingDetailService()
        {
            _shippingDetailRepository = new ShippingDetailRepository();
        }

        public IEnumerable<ShippingDetail> GetAll() => _shippingDetailRepository.GetAll();

        public ShippingDetail GetById(int id) => _shippingDetailRepository.GetById(id);

        public void Create(ShippingDetail detail) => _shippingDetailRepository.Add(detail);

        public void Update(ShippingDetail detail) => _shippingDetailRepository.Update(detail);

        public void Delete(int id) => _shippingDetailRepository.Delete(id);
    }
}
