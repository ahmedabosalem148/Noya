using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;

namespace Noya.BLL.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryRepository _inventoryRepository;

        public InventoryService()
        {
            _inventoryRepository = new InventoryRepository();
        }

        public IEnumerable<Inventory> GetAll() => _inventoryRepository.GetAll();

        public Inventory GetById(int id) => _inventoryRepository.GetById(id);

        public void Create(Inventory inventory) => _inventoryRepository.Add(inventory);

        public void Update(Inventory inventory) => _inventoryRepository.Update(inventory);

        public void Delete(int id) => _inventoryRepository.Delete(id);
    }
}
