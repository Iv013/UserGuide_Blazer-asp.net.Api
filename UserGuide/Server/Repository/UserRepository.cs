using UserGuide.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserGuide.Server.Repository;

namespace WebAppUser.Repository
{
    public class UserRepository  : IUserRepository
    {
        private readonly ApplicationDBContext _db;
        internal DbSet<UserData> dbSet;
        public UserRepository(ApplicationDBContext db)
        {
            _db = db;
            this.dbSet = db.Set<UserData>();
        }

        public void Add(UserData entity)
        {
           dbSet.Add(entity);
        }

        public UserData Find(int id)
        {
            return dbSet.Find(id);
        }

        public async Task<UserData> FirstOfDefault(Expression<Func<UserData, bool>>
            filter = null, string includeProperty = null, bool isTracking = true)
        {
            IQueryable<UserData> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperty != null)
            {
                foreach (var includeProp in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }    
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return  query.FirstOrDefault();
        }

        public async Task<IEnumerable<UserData>> GetAll(Expression<Func<UserData, 
            bool>> filter = null,
            Func<IQueryable<UserData>, 
                IOrderedQueryable<UserData>> orderBY = null,
            string includeProperty = null, bool isTracking = true)
        {
            IQueryable<UserData> query = dbSet;
            if (filter!= null){
                query= query.Where(filter);
            }
            if(includeProperty!= null)
            { 
                foreach(var includeProp in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)){
                    query = query.Include(includeProp);
                }           
            }
           if (orderBY != null)
            {
                query = orderBY(query);
            }
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();

        }

     

        public void Remove(UserData entity)
        {
           dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<UserData> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public void Save()
        {
           _db.SaveChangesAsync();
        }

        public void Update(UserData entity)
        {
            _db.Update(entity);
        }
    }
}
