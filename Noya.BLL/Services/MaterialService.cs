using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;

namespace Noya.BLL
{
    public class MaterialService
    {
        private readonly MaterialRepository _repo;

        public MaterialService()
        {
            _repo = new MaterialRepository();
        }

        public IEnumerable<Material> GetAll()
        {
            return _repo.GetAll();
        }

        public Material GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Create(Material material)
        {
            _repo.Add(material);
        }

        public void Update(Material material)
        {
            _repo.Update(material);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
