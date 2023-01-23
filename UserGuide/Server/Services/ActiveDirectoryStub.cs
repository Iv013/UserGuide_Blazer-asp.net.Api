using System.Collections.Specialized;

namespace UserGuide.Server.Services
{
    public class ActiveDirectoryStub : IActiveDirectoryService
    {
        private NameValueCollection UserLogin = new NameValueCollection()
        {
             {"google.com", "IvanovII"},
             {"google.Com","PetrovPP"},
             {"VK.com","Petrov"},
             {"testdomen.ru","SidorovSS"},
             {"testdomen.ru","Ivanovi"},
             {"vk.com","SidorovSS"},
             {"vk.com","SidorovSI"}
        };

        public bool ContainsUser(string domen, string login)
        {
            try
            {
                var result = UserLogin.GetValues(domen).ToArray();   //поместил в try так как если в коллекции нет нужного домена ToArray выкинет исклюение
                foreach (var item in result)
                {
                    if (item.Equals(login, StringComparison.OrdinalIgnoreCase)) return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
           
            return  false;
        }


    }
}
