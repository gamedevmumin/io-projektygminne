using System.Collections.Generic;

namespace VotingSystem.Models.Data
{
    public interface IAuthRepo
    {
        IEnumerable<User> GetAllUsers();
        User FindUserById(long id);
        User FindUserByUsername(string username);



        IEnumerable<UserRole> GetAllUserRoles();
        void AddUser(User newUser);
    }
}
