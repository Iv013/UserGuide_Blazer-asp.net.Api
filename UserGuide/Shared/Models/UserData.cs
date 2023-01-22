
using System.ComponentModel.DataAnnotations;

namespace UserGuide.Shared.Models
{
    public class UserData
    {
        [Key]
        public int Userid { get; set; }
        [StringLength(50, ErrorMessage = "Имя должно быть не длинее 50 символов")]
        [Required (ErrorMessage = "Введите имя")]
        public string FirstName     { get; set; }

        [StringLength(50, ErrorMessage = "Фамилия должна быть не длинее 50 символов")]
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }
       
        [StringLength(50, ErrorMessage = "Отчество должно быть не длинее 50 символов")]
        public string Patronymic { get; set; } = string.Empty;

        [RegularExpression (@"^([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}\\[a-zA-Z0-9_.-]+"
        , ErrorMessage = "Не верный формат (Домен1.домен2\\Логин)")]
        [Required(ErrorMessage = "Введите логин в формате 'Домен\\Логин'")]
        public string UserLogin { get; set; }

        public bool UserEnable { get; set; }
        
    }
}
