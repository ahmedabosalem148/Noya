using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;

namespace Noya.BLL.Services
{
    public class BundleService : IBundleService
    {
        private readonly BundleRepository _bundleRepository;

        public BundleService()
        {
            _bundleRepository = new BundleRepository();
        }

        public IEnumerable<Bundle> GetAll() => _bundleRepository.GetAll();

        public Bundle GetById(int id) => _bundleRepository.GetById(id);

        public void Create(Bundle bundle) => _bundleRepository.Add(bundle);

        public void Update(Bundle bundle) => _bundleRepository.Update(bundle);

        public void Delete(int id) => _bundleRepository.Delete(id);
    }
}
