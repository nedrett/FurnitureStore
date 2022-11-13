using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.Controllers.Furniture
{
    using Core.Contracts;
    using Core.Models.Furniture.Chair;
    using System.Security.Claims;

    public class ChairController : FurnitureController
    {
        private readonly IChairService chairService;

        public ChairController(IChairService _chairService)
        {
            chairService = _chairService;
        }

        public async Task<IActionResult> All()
        {
            var chairItems = await chairService.GetAll();

            return View("ChairCatalog", chairItems);
        }

        /// <summary>
        /// Show Detailed View of Chair Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = new ChairDetailsModel();

            return View(model);
        }

        /// <summary>
        /// Get Add Chair Item View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var model = new ChairModel();

            return View(model);
        }

        /// <summary>
        /// Adds Chair item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(ChairModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await chairService.Add(model, userId);

            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Get Edit Chair item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new ChairModel();

            return View(model);
        }

        /// <summary>
        /// Edits Chair item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ChairModel model)
        {
            return RedirectToAction(nameof(Details), new { id });
        }

        /// <summary>
        /// User not owner buy Chair item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await chairService.Delete(id);

            return RedirectToAction("All", "Chair");
        }
    }
}
