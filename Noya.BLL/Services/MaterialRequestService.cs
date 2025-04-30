using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;

namespace Noya.BLL
{
    public class MaterialRequestService
    {
        private readonly MaterialRequestRepository _repo;

        public MaterialRequestService()
        {
            _repo = new MaterialRequestRepository();
        }

        public IEnumerable<MaterialRequest> GetAll()
        {
            return _repo.GetAll();
        }

        public MaterialRequest GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Create(MaterialRequest request)
        {
            _repo.Add(request);
        }

        public void Update(MaterialRequest request)
        {
            _repo.Update(request);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
