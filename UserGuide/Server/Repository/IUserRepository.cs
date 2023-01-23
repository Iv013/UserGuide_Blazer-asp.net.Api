using System.Linq.Expressions;


namespace UserGuide.Server.Repository
{
    public interface IUserRepository
    {

      

        Task<IEnumerable<UserData>> GetAllUsers(
            Expression<Func<UserData, bool>> filter = null,
            Func<IQueryable<UserData>,
            IOrderedQueryable<UserData>> orderBY = null,
            string includeProperty = null,
            bool isTracking = true
            );

        Task<UserData> FirstOfDefault(
            Expression<Func<UserData, bool>> filter = null,
            string includeProperty = null,
            bool isTracking = true
            );


         Task<ServiceResponse> AddUser(UserData entity);
        Task<ServiceResponse> DisableUser(int id);
        Task<ServiceResponse> UpdateUser(UserData entity);

    }
}
