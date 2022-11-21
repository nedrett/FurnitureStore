using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.Controllers.Furniture
{
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class FurnitureController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Catalog()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() => View();

        /// <summary>
        /// Shows created offers by the User
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> MyFurnitures()
        {
            //var model = new AllFurnituresModel();

            return View();
        }
    }
}
