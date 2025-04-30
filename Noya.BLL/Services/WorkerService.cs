using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;

namespace Noya.BLL
{
    public class WorkerService
    {
        private readonly WorkerRepository _repo;

        public WorkerService()
        {
            _repo = new WorkerRepository();
        }

        public IEnumerable<Worker> GetAll()
        {
            return _repo.GetAll();
        }

        public Worker GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Create(Worker worker)
        {
            _repo.Add(worker);
        }

        public void Update(Worker worker)
        {
            _repo.Update(worker);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
