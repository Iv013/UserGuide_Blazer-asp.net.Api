

using UserGuide.Core.Repository;
using UserGuide.Shared.Models;

namespace UUserGuide.Core.Data
{
    public static class InitializedBD
    {



        public static void MethodInit(IUserRepository db)
        {

            var user = new UserData()
            {
                FirstName = "Иван",
                LastName = "Иванов",
                Patronymic = "Иванович",
                UserLogin = "Google.com\\IvanovII",
                UserEnable = true
            };
            db.AddUser(user);
            user = new UserData()
            {
                FirstName = "Петр",
                LastName = "Петров",
                Patronymic = "Петрович",
                UserLogin = "google.Com\\petrovPP",
                UserEnable = true
            };
            db.AddUser(user);
            user = new UserData()
            {
                FirstName = "Сидоров",
                LastName = "Семен",
                Patronymic = "Семенович",
                UserLogin = "testdomen.ru\\IvanovII",
                UserEnable = true
            };
            db.AddUser(user);




        }
    }
}
