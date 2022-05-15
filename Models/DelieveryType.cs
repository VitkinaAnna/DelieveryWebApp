using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DelieveryWebApplication
{
    public partial class DelieveryType
    {
        public DelieveryType()
        {
            Couriers = new HashSet<Courier>();
        }

        public int TypeId { get; set; }


        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Тип доставки")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Courier> Couriers { get; set; }
    }
}
