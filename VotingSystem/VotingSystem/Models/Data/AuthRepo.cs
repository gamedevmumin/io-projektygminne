using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Models.Data
{
    internal class AuthRepo : IAuthRepo
    {
        private readonly VotingSystemDbContext _dbContext;

        public AuthRepo(VotingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users
                .Include(u => u.Role)
                .ToList();
        }
        
        public User FindUserByUsername(string username)
        {
            return _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username);
        }

        public IEnumerable<UserRole> GetAllUserRoles()
        {
            return _dbContext.UserRoles
                .ToList();
        }

        public User FindUserById(long id)
        {
            return _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User newUser)
        {
            _dbContext.Users.Add(newUser);
            _dbContext.Entry(newUser).Reference(u => u.Role).Load();
        }
    }
}