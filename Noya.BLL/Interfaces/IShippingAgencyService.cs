using System.Collections.Generic;
using Noya.Models;

namespace Noya.BLL.Interfaces
{
    public interface IShippingAgencyService
    {
        IEnumerable<ShippingAgency> GetAll();
        ShippingAgency GetById(int id);
        void Create(ShippingAgency agency);
        void Update(ShippingAgency agency);
        void Delete(int id);
    }
}
