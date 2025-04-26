using System.Collections.Generic;
using Noya.Models;

namespace Noya.BLL.Interfaces
{
    public interface IInventoryService
    {
        IEnumerable<Inventory> GetAll();
        Inventory GetById(int id);
        void Create(Inventory inventory);
        void Update(Inventory inventory);
        void Delete(int id);
    }
}
