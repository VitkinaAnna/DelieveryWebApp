using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DelieveryWebApplication
{
    public partial class Menu
    {
        public Menu()
        {
            Delieveries = new HashSet<Delievery>();
        }

        public int DishId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Назва страви")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Ціна, грн")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public int Price { get; set; }
        [Display(Name = "Тип страви")]
        public int TypeId { get; set; }

        [Display(Name = "Тип страви")]
        public virtual DishType Type { get; set; } = null!;
        public virtual ICollection<Delievery> Delieveries { get; set; }
    }
}
