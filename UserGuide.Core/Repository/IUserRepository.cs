using System.Linq.Expressions;
using UserGuide.Shared.Models;

namespace UserGuide.Core.Repository
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
