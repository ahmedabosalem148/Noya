using Noya.DAL;
using Noya.Models;
using Noya.Models.Noya.Models;

namespace Noya.BLL.Services
{
    public class AuthService
    {
        private readonly AuthRepository _authRepo;

        public AuthService()
        {
            _authRepo = new AuthRepository();
        }

        public LoginResponse Login(string email, string password)
        {
            return _authRepo.Login(email, password);
        }
    }
}
