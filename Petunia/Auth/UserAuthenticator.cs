namespace Petunia.Auth
{
    abstract class UserAuthenticator : SqlBase
    {
        public abstract AuthUser GetAuthUser(string id, string password);
    }
}
