using UserService.Database;
using UserService.Database.Entities;
using UserService.IRepository;

namespace UserService.Repository
{
    public class AuthUserRepository : IAuthUserRepository
    {
        private readonly DatabaseContext databaseContext;
        public AuthUserRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }


        public AuthUser AddAuthUser(AuthUser authUser)
        {
            var response = databaseContext.AuthUsers.Add(authUser);
            databaseContext.SaveChanges();
            return response.Entity;
        }

        public AuthUser AuthenticateAuthUser(AuthUser authUser)
        {
            authUser.CreateDate = DateTime.Now;
            try
            {
                var user = databaseContext
                       .AuthUsers
                       .Where(x => x.Username == authUser.Username && x.Password == authUser.Password)
                       .FirstOrDefault();
                return user;
            }
            catch (Exception)
            {
                return null;
            }                     
        }

        public bool DeleteAuthUser(int Id)
        {
            var user = databaseContext.AuthUsers.Find(Id);
            var isDeleted = databaseContext.AuthUsers.Remove(user);
            databaseContext.SaveChanges(true);
            return isDeleted != null ? true : false;
        }       

        public AuthUser GetAuthUserById(int Id)
        {
            return databaseContext.AuthUsers.Find(Id);
        }

        public IEnumerable<AuthUser> GetAuthUsers()
        {
            return databaseContext.AuthUsers.ToList();
        }               

        public AuthUser UpdateAuthUser(AuthUser authAser)
        {
            var response = databaseContext.AuthUsers.Update(authAser);
            databaseContext.SaveChanges();
            return response.Entity;
        }

        
    }
}
