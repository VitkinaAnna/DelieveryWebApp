using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DelieveryWebApplication
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Ім`я")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Прізвище")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Назва вулиці")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Номер будинку")]
        public int? House { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Номер квартири")]
        public int? Flat { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Номер телефону")]
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
