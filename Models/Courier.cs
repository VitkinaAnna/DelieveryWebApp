using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DelieveryWebApplication
{
    public partial class Courier
    {
        public Courier()
        {
            Orders = new HashSet<Order>();
        }

        public int CourierId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Ім`я")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Прізвище")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Номер телефону")]
        public string? PhoneNumber { get; set; }
        public int? DelieveryType { get; set; }

        [Display(Name = "Тип доставки")]
        public virtual DelieveryType? DelieveryTypeNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
