

using Inventory.Entities;

namespace Inventory.Persistence.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, String password);
        Task<User> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}