using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FurnitureStore.Areas.Admin.AdminConstants;

namespace FurnitureStore.Controllers.Furniture
{
    using Core.Contracts;
    using Core.Models.Furniture.Chair;
    using HouseRentingSystem.Core.Constants;
    using System.Security.Claims;

    public class ChairController : FurnitureController
    {
        private readonly IChairService chairService;
        private readonly ILogger<ChairController> logger;

        public ChairController(
            IChairService _chairService, 
            ILogger<ChairController> _logger)
        {
            chairService = _chairService;
            logger = _logger;
        }

        /// <summary>
        /// Shows all Chair Items
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            IEnumerable<ChairCatalogModel> chairItems = null;

            try
            {
                chairItems = await chairService.GetAll();

            }
            catch (Exception e)
            {
                logger.LogError(nameof(All), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot get items";
            }

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

            try
            {
                await chairService.Add(model, userId);

            }
            catch (Exception e)
            {

                logger.LogError(nameof(Add), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot add item";
            }

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

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var model = new ChairModel
            {
                Id = chair.Id,
                Name = chair.Name,
                Price = chair.Price,
                Quantity = chair.Quantity,
                Description = chair.Description,
                ImageUrl = chair.ImageUrl,
                CreatorId = chair.CreatorId
            };

            if (chair.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
            {
                return View(model);
            }
            else
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
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

            try
            {
                await chairService.Edit(model.Id, model);
            }
            catch (Exception e)
            {
                logger.LogError(nameof(Edit), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot edit item";
            }

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
            var chair = await chairService.ChairDetailsById(id);
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (chair.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
            else
            {
                return RedirectToAction(nameof(All));
            }
        }

        /// <summary>
        /// Changes IsActive flag to false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var chair = await chairService.ChairDetailsById(id);
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (chair.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
            {
                try
                {
                    await chairService.Delete(id);
                }
                catch (Exception e)
                {
                    logger.LogError(nameof(Delete), e);
                    TempData[MessageConstant.ErrorMessage] = "Cannot delete item";
                }

                TempData[MessageConstant.SuccessMessage] = "Successfully deleted Chair";

                return RedirectToAction("All", "Chair");
            }
            else
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
        }
    }
}
