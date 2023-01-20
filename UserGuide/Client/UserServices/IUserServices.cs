using UserGuide.Shared.Models;

namespace UserGuide.Client.UserServices
{
    public interface IUserServices
    {
        List<UserData> ListUser { get; set; }

        Task GetUsers();

        Task<UserData> GetSingleUser(int id);
        Task UpdateUser(UserData user);
        Task DeleteActiveUser(int id);
        Task CreateUser(UserData user);
    }
}
