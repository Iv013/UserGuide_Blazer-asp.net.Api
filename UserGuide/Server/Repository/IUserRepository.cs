using System.Linq.Expressions;


namespace UserGuide.Server.Repository
{
    public interface IUserRepository
    {

      

        Task<IEnumerable<UserData>> GetAll(
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


         Task<ServiceResponse> Add(UserData entity);
  
        Task<ServiceResponse> DisableUser(int id);
        Task<ServiceResponse> Update(UserData entity);

    }
}
