using UserService.Database.Entities;

namespace UserService.IRepository
{
    public interface IAuthUserRepository
    {
        AuthUser AuthenticateAuthUser(AuthUser authUser);
        IEnumerable<AuthUser> GetAuthUsers();
        AuthUser GetAuthUserById(int Id);
        AuthUser AddAuthUser(AuthUser authUser);
        AuthUser UpdateAuthUser(AuthUser authAser);
        bool DeleteAuthUser(int Id);
    }
}
