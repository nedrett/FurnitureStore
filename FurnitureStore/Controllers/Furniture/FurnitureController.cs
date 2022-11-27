using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.Controllers.Furniture
{
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class FurnitureController : Controller
    {
        /// <summary>
        /// Shows All Items in Catalog
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Catalog() => View();

        /// <summary>
        /// Logged in users can create item
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create() => View();

        /// <summary>
        /// Shows created offers by the User
        /// </summary>
        /// <returns></returns>
        public IActionResult MyFurnitures() => View();
    }
}
