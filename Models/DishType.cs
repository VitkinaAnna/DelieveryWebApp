using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DelieveryWebApplication
{
    public partial class DishType
    {
        public DishType()
        {
            Menus = new HashSet<Menu>();
        }

        public int TypeId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Тип страви")]
        public string? Name { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
