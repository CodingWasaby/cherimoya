namespace Petunia.Auth
{
    class MockUserAuthenticator : UserAuthenticator
    {
        public override AuthUser GetAuthUser(string id, string password)
        {
            return new AuthUser() { ID = id, Name = id };
        }
    }
}
