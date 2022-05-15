using DelieveryWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DelieveryWebApplication.ViewModel
{
    public class EditableDelivery
    {
        public Delievery Delivery { get; set; }
        public IEnumerable<Order> OrderList { get; set; }
        public IEnumerable<SelectListItem> DishList { get; set; }
    }
}
