using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;

namespace Noya.BLL.Services
{
    public class ShippingAgencyService : IShippingAgencyService
    {
        private readonly ShippingAgencyRepository _shippingAgencyRepository;

        public ShippingAgencyService()
        {
            _shippingAgencyRepository = new ShippingAgencyRepository();
        }

        public IEnumerable<ShippingAgency> GetAll() => _shippingAgencyRepository.GetAll();

        public ShippingAgency GetById(int id) => _shippingAgencyRepository.GetById(id);

        public void Create(ShippingAgency agency) => _shippingAgencyRepository.Add(agency);

        public void Update(ShippingAgency agency) => _shippingAgencyRepository.Update(agency);

        public void Delete(int id) => _shippingAgencyRepository.Delete(id);
    }
}
