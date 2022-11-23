using FurnitureStore.Core.Models.Furniture.Table;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.Controllers.Furniture
{
    using Core.Contracts;
    using System.Security.Claims;

    public class SofaController : FurnitureController
    {
        private readonly ISofaService sofaService;

        public SofaController(ISofaService _sofaService)
        {
            sofaService = _sofaService;
        }

        /// <summary>
        /// Shows all Sofa Items
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var sofaItems = await sofaService.GetAll();

            return View("SofaCatalog", sofaItems);
        }

        /// <summary>
        /// Show Detailed View of Sofa Item
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (await sofaService.Exists(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var sofaModel = await sofaService.SofaDetailsById(id);

            return View(sofaModel);
        }

        /// <summary>
        /// Get Add Sofa Item View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var model = new SofaModel();

            return View(model);
        }

        /// <summary>
        /// Adds Sofa item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(SofaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await sofaService.Add(model, userId);

            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Get Edit Sofa item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await sofaService.Exists(id) == false)
            {
                return RedirectToAction(nameof(All));

            }

            var sofa = await sofaService.SofaDetailsById(id);

            var model = new SofaModel
            {
                Id = sofa.Id,
                Name = sofa.Name,
                UpholsteryType = sofa.UpholsteryType,
                Width = sofa.Width,
                Length = sofa.Length,
                Height = sofa.Height,
                Price = sofa.Price,
                Quantity = sofa.Quantity,
                Description = sofa.Description,
                ImageUrl = sofa.ImageUrl
            };

            return View(model);
        }

        /// <summary>
        /// Edits Sofa item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SofaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await sofaService.Edit(model.Id, model);

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        /// <summary>
        /// User not owner buy Sofa item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Changes IsActive flag to false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await sofaService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
