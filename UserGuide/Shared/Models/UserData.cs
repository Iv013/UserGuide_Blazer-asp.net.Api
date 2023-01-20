
using System.ComponentModel.DataAnnotations;

namespace UserGuide.Shared.Models
{
    public class UserData
    {
        [Key]
        public int Userid { get; set; }

        [Required (ErrorMessage = "Данный параметр не может быть пустым")]
        public string FirstName     { get; set; }
        [Required(ErrorMessage = "Данный параметр не может быть пустым")]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        //[RegularExpression(@"\w+\.\w+\\\w+") ]
        [RegularExpression(@"[0-9a-zA-Z-]+\.[a-zA-Z]{2,4}\\[0-9A-Za-z_.-]+", ErrorMessage = "Не верный формат (Домен\\логин)")]
     
        [Required(ErrorMessage = "Данный параметр не может быть пустым")]
        public string UserLogin { get; set; }

        public bool UserEnable { get; set; }

    }
}
