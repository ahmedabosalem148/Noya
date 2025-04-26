using System.Collections.Generic;
using Noya.Models;

namespace Noya.BLL.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        void Create(Role role);
        void Update(Role role);
        void Delete(int id);
    }
}
