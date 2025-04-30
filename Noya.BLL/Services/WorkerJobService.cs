using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;

namespace Noya.BLL
{
    public class WorkerJobService
    {
        private readonly WorkerJobRepository _repo;

        public WorkerJobService()
        {
            _repo = new WorkerJobRepository();
        }

        public IEnumerable<WorkerJob> GetByWorkerId(int workerId)
        {
            return _repo.GetByWorkerId(workerId);
        }

        public void Create(WorkerJob job)
        {
            _repo.Add(job);
        }

        public void Update(WorkerJob job)
        {
            _repo.Update(job);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
