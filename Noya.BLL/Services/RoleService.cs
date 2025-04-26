using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;

namespace Noya.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService()
        {
            _roleRepository = new RoleRepository();
        }

        public IEnumerable<Role> GetAll() => _roleRepository.GetAll();

        public Role GetById(int id) => _roleRepository.GetById(id);

        public void Create(Role role) => _roleRepository.Add(role);

        public void Update(Role role) => _roleRepository.Update(role);

        public void Delete(int id) => _roleRepository.Delete(id);
    }
}
