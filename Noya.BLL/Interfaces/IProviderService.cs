using System.Collections.Generic;
using Noya.Models;

namespace Noya.BLL.Interfaces
{
    public interface IProviderService
    {
        IEnumerable<Provider> GetAll();
        Provider GetById(int id);
        void Create(Provider provider);
        void Update(Provider provider);
        void Delete(int id);
    }
}
