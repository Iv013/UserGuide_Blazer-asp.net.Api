using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGuide.Shared.Models
{
   public static class WebConstant
    {

       public const string AddUserSuccess = "Новый пользователь успешно добавлен в справочник";
        public const string AddErrorUserContainedInBase = "Пользователь не добавлен. Пользователь с данным логином уже есть в базе";
       public const string FindUserNotSuccess = "Пользователь с такими данными отсутвутет в базе данных";
       public const string UserDeleteSuccess = "Пользователь успешно удален";
       public const string UserUpdateSuccess = "Данные пользователя успешно обновлены в справочнике";
       public const string FindUserInADNotSuccess = ", данный пользователь отсутcтвует в Active Directory";
       public const string FindUserInADSuccess = ", данный пользователь присутствует в Active Directory";

    } 
}
