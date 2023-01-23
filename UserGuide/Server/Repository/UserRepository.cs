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

        public async Task<ServiceResponse> AddUser(UserData entity)
        {
            var obj = dbSet.FirstOrDefault(x => x.UserLogin == entity.UserLogin);
            if (obj != null ) //Проверяем есть такой  пользователь в базе или нет
            { 
                if (obj.UserEnable) //если он активный тогда не добаляем его
                {
                    return new ServiceResponse()
                    {
                        Success = 409, //ошибка
                        Message = "Пользователь не добавлен. Пользователь с данным логином уже есть в базе"
                    };
                } else      //если не активный то обновляем данные и активируем
                {
                    obj.FirstName = entity.FirstName;
                    obj.LastName = entity.LastName;
                    obj.Patronymic = entity.Patronymic;
                    obj.UserLogin = entity.UserLogin;
                    obj.UserEnable = true;
                    _db.Update(obj);
                    await _db.SaveChangesAsync();
                    return new ServiceResponse() { Message = "Новый пользователь успешно добавлен в справочник " };
                }
            }

           dbSet.Add(entity);
           await _db.SaveChangesAsync();
           return new ServiceResponse() { Message = "Новый пользователь успешно добавлен в справочник" };
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

        public async Task<IEnumerable<UserData>> GetAllUsers(Expression<Func<UserData, 
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



        public async Task<ServiceResponse> DisableUser(int id)
        {
            var obj = dbSet.FirstOrDefault(x => x.Userid == id);
            if (obj == null)
            {
                return new ServiceResponse()
                {
                    Success = 409, //ошибка
                    Message = "Пользователь с такими данными отсутвутет"
                };
            }

            obj.UserEnable = false;
            _db.Update(obj);
            await _db.SaveChangesAsync();
            return new ServiceResponse()  { Message = "Пользователь успешно удален" };   
        }


        public async Task<ServiceResponse> UpdateUser(UserData entity)
        {
            var obj = dbSet.FirstOrDefault(x => x.Userid == entity.Userid);
            if (obj == null)
            {
                return new ServiceResponse()
                {
                    Success = 409, //ошибка
                    Message = "Пользователь с такими данными отсутвутет в базе"
                };
            }

            obj.FirstName = entity.FirstName;
            obj.LastName = entity.LastName;
            obj.Patronymic = entity.Patronymic;
            obj.UserLogin = entity.UserLogin;
            _db.Update(obj);
            await _db.SaveChangesAsync();
            return new ServiceResponse()
            { Message = "Данные пользователя успешно обновлены в справочнике" };
        }
    }
}
