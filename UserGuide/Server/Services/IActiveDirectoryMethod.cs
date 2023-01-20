namespace UserGuide.Server.Services
{
    public interface IActiveDirectoryMethod
    {
        bool ContainsUser(string userLogin);
    }
}
