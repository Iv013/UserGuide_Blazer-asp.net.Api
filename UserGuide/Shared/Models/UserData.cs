
using System.ComponentModel.DataAnnotations;

namespace UserGuide.Shared.Models
{
    public class UserData
    {
        [Key]
        public int Userid { get; set; }
        [StringLength(50, ErrorMessage = "Имя  не должно быть длинее 50 символов")]
        [Required (ErrorMessage = "Введите имя")]
        public string FirstName     { get; set; }

        [StringLength(50, ErrorMessage = "Фамилия не должна быть  длинее 50 символов")]
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }
       
        [StringLength(50, ErrorMessage = "Отчество не должно быть  длинее 50 символов")]
        public string Patronymic { get; set; } = string.Empty;

        [RegularExpression (@"^([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}\\[a-zA-Z0-9_.-]+"
        , ErrorMessage = "Не верный формат (Доменное имя\\Логин)")]
        [Required(ErrorMessage = "Введите логин в формате 'Домен\\Логин'")]
        public string UserLogin { get; set; }

        public bool UserEnable { get; set; }
        
    }
}
