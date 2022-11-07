using UserService.Database;
using UserService.Database.Entities;
using UserService.IRepository;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext databaseContext;
        public UserRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public User AddUser(User user)
        {
            var response = databaseContext.Users.Add(user);
            databaseContext.SaveChanges();
            return response.Entity;
        }

        public bool DeleteUser(int Id)
        {
            var user = databaseContext.Users.Find(Id);
            var isDeleted = databaseContext.Users.Remove(user);
            databaseContext.SaveChanges(true);
            return isDeleted != null ? true : false;
        }

        public User GetUserById(int Id)
        {
            return databaseContext.Users.Find(Id);
        }

        public IEnumerable<User> GetUsers()
        {
            return databaseContext.Users.ToList();
        }

        public User UpdateUser(User user)
        {
           var response = databaseContext.Users.Update(user);
            databaseContext.SaveChanges();
            return response.Entity;
        }
    }
}
