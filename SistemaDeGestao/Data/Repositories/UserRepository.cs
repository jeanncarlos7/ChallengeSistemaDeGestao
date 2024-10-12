using SistemaDeGestao.Models;
using System.Collections.Generic;

namespace SistemaDeGestao.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public User GetUserByEmail(string email) => _users.FirstOrDefault(u => u.Email == email);

        public void AddUser(User user) => _users.Add(user);

        public void UpdateUser(User user)
        {
            var existingUser = GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                existingUser.Password = user.Password;
            }
        }

        public void DeleteUser(int id) => _users.RemoveAll(u => u.Id == id);
    }
}
