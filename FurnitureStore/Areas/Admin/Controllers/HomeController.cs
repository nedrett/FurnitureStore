using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
