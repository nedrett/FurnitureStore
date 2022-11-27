using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.Controllers.Furniture
{
    using Core.Contracts;
    using Core.Models.Furniture.Chair;
    using HouseRentingSystem.Core.Constants;
    using System.Security.Claims;

    public class ChairController : FurnitureController
    {
        private readonly IChairService chairService;

        public ChairController(IChairService _chairService)
        {
            chairService = _chairService;
        }

        /// <summary>
        /// Shows all Chair Items
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var chairItems = await chairService.GetAll();

            if (chairItems == null || chairItems.Count() == 0)
            {
                TempData[MessageConstant.ErrorMessage] = "No Chairs are Available";

                return RedirectToAction("Catalog", "Furniture");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            foreach (var chair in chairItems)
            {
                if (chair.CreatorId == userId)
                {
                    chair.IsCreator = true;
                }
            }

            TempData[MessageConstant.SuccessMessage] = "All Available Chairs";

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
            if (await chairService.Exists(id) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Chair not Available";

                return RedirectToAction(nameof(All));
            }

            var chairModel = await chairService.ChairDetailsById(id);
            
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (chairModel.CreatorId == userId)
            {
                chairModel.IsCreator = true;
            }

            return View(chairModel);
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
                TempData[MessageConstant.ErrorMessage] = "Incorrect Input";

                return View(model);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await chairService.Add(model, userId);

            TempData[MessageConstant.SuccessMessage] = "Successfully added Chair";
            
            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Get Edit Chair item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await chairService.Exists(id) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Chair not Available";

                return RedirectToAction(nameof(All));

            }

            var chair = await chairService.ChairDetailsById(id);
            
            var model = new ChairModel
            {
                Id = chair.Id,
                Name = chair.Name,
                Price = chair.Price,
                Quantity = chair.Quantity,
                Description = chair.Description,
                ImageUrl = chair.ImageUrl
            };

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
            if (!ModelState.IsValid)
            {
                TempData[MessageConstant.ErrorMessage] = "Incorrect Input";

                return View(model);
            }

            await chairService.Edit(model.Id, model);

            TempData[MessageConstant.SuccessMessage] = "Successfully edited Chair";

            return RedirectToAction(nameof(Details), new { id = model.Id });
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

        /// <summary>
        /// Changes IsActive flag to false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await chairService.Delete(id);

            TempData[MessageConstant.SuccessMessage] = "Successfully deleted Chair";

            return RedirectToAction("All", "Chair");
        }
    }
}
