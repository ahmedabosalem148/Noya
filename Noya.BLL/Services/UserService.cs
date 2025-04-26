using System.Collections.Generic;
using Noya.Models;
using Noya.DAL;
using Noya.BLL.Interfaces;

namespace Noya.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public IEnumerable<User> GetAll() => _userRepository.GetAll();

        public User GetById(int id) => _userRepository.GetById(id);

        public void Create(User user) => _userRepository.Add(user);

        public void Update(User user) => _userRepository.Update(user);

        public void Delete(int id) => _userRepository.Delete(id);
    }
}
