using UserService.Database.Entities;

namespace UserService.IRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int Id);
        User AddUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(int Id);
    }
}
