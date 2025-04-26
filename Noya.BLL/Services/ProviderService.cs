using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;

namespace Noya.BLL.Services
{
    public class ProviderService : IProviderService
    {
        private readonly ProviderRepository _providerRepository;

        public ProviderService()
        {
            _providerRepository = new ProviderRepository();
        }

        public IEnumerable<Provider> GetAll() => _providerRepository.GetAll();

        public Provider GetById(int id) => _providerRepository.GetById(id);

        public void Create(Provider provider) => _providerRepository.Add(provider);

        public void Update(Provider provider) => _providerRepository.Update(provider);

        public void Delete(int id) => _providerRepository.Delete(id);
    }
}
