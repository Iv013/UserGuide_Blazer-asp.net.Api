namespace UserGuide.Server.Services
{
    public interface IActiveDirectoryService
    {
       bool ContainsUser(string domen, string login);
    }
}
