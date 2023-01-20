using System.Linq.Expressions;


namespace UserGuide.Server.Repository
{
    public interface IUserRepository
    {

        public UserData Find(int id);

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


        void Add(UserData entity);
        void RemoveRange(IEnumerable<UserData> entities);
        void Remove(UserData entity);
        void Save();
        void Update(UserData entity);

    }
}
