using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DelieveryWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecChartController : ControllerBase
    {
        private readonly delieveryContext _context;
        public SecChartController(delieveryContext context)
        {
            _context = context;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var types = _context.DishTypes.ToList();
            List<object> catTypes = new List<object>();
            catTypes.Add(new[] { "Тип страви", "Кількість страв кожного типу" });
            foreach (var item in types)
            {
                var curMenus = (from o in _context.Menus
                                 where o.TypeId == item.TypeId
                                 select o).ToList();

                if (curMenus != null)
                {
                    if (curMenus.Count() != 0)
                    {
                        catTypes.Add(new object[] { item.Name, curMenus.Count() });
                    }
                    else
                    {
                        catTypes.Add(new object[] { item.Name, 0 });
                    }
                }
            }
            return new JsonResult(catTypes);
        }
    }
}
