using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DelieveryWebApplication
{
    public partial class Delievery
    {
        [Display(Name = "Страва")]
        public int DishId { get; set; }
        [Display(Name = "Замовлення")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Кількість")]
        public int Amount { get; set; }
        public int DelieveryId { get; set; }

        [Display(Name = "Назва страви")]
        public virtual Menu Dish { get; set; } = null!;

        [Display(Name = "Замовлення")]
        public virtual Order Order { get; set; } = null!;
    }
}
