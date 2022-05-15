using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DelieveryWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly delieveryContext _context;
        public ChartController(delieveryContext context)
        {
            _context = context;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var couriers = _context.Couriers.ToList();
            List<object> catCourier = new List<object>();
            catCourier.Add(new[] { "І`мя кур`єра", "Кількість замовллень кур`ера" });
            foreach (var item in couriers)
            {
                var curOrders = (from o in _context.Orders
                                 where o.CourierId == item.CourierId
                                 select o).ToList();

                if (curOrders != null)
                {
                    if (curOrders.Count() != 0)
                    {
                        catCourier.Add(new object[] { item.Name, curOrders.Count() });
                    }
                    else
                    {
                        catCourier.Add(new object[] { item.Name, 0 });
                    }
                }
            }
            return new JsonResult(catCourier);
        }
    }
}
