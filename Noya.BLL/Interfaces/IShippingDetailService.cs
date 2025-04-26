using System.Collections.Generic;
using Noya.Models;

namespace Noya.BLL.Interfaces
{
    public interface IShippingDetailService
    {
        IEnumerable<ShippingDetail> GetAll();
        ShippingDetail GetById(int id);
        void Create(ShippingDetail detail);
        void Update(ShippingDetail detail);
        void Delete(int id);
    }
}
