using System.Collections.Specialized;

namespace UserGuide.Server.Services
{
    public class ActiveDirectoryStub : IActiveDirectoryMethod
    {
        private NameValueCollection UserLogin = new NameValueCollection()
        {
            {"qwerty.com", "IvanovII"},
            {"qwerty.com","PetrovPP"},
             {"123.com","PetrovPP"},
             {"321.com","PetrovPP"},
             {"qwerty.com","SidorovSS"}
        };

        public bool ContainsUser(string userLogin)
        {
           var DomenLogin  = userLogin.Split('\\').Select(x=>x.ToLower()).ToArray() ;
          return  UserLogin.GetValues(DomenLogin[0]).Select(x=>x.ToLower()).Contains(DomenLogin[1]);
        }


    }
}
