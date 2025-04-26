using System.Collections.Generic;
using Noya.Models;

namespace Noya.BLL.Interfaces
{
    public interface IBundleService
    {
        IEnumerable<Bundle> GetAll();
        Bundle GetById(int id);
        void Create(Bundle bundle);
        void Update(Bundle bundle);
        void Delete(int id);
    }
}
