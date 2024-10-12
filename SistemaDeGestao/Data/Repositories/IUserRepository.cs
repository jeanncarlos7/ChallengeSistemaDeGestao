using SistemaDeGestao.Models;

namespace SistemaDeGestao.Data.Repositories
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
