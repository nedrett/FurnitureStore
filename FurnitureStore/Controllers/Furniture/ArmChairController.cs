using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.Controllers.Furniture
{
    using Core.Contracts;
    using Core.Models.Furniture.ArmChair;
    using HouseRentingSystem.Core.Constants;
    using System.Security.Claims;

    public class ArmChairController : FurnitureController
    {
        private readonly IArmChairService armChairService;
        private readonly ILogger logger;

        public ArmChairController(
            IArmChairService _armChairService,
            ILogger _logger)
        {
            armChairService = _armChairService;
            logger = _logger;
        }

        /// <summary>
        /// Shows all Chair Items
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var armChairItems;

            try
            {
                armChairItems = await armChairService.GetAll();

            }
            catch (Exception e)
            {
                logger.LogError(nameof(All), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot get items";
            }

            if (armChairItems == null || armChairItems.Count() == 0)
            {
                TempData[MessageConstant.ErrorMessage] = "No Armchairs are Available";

                return RedirectToAction("Catalog", "Furniture");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            foreach (var armChair in armChairItems)
            {
                if (armChair.CreatorId == userId)
                {
                    armChair.IsCreator = true;
                }
            }

            TempData[MessageConstant.SuccessMessage] = "All Available Armchairs";

            return View("ArmChairCatalog", armChairItems);
        }

        /// <summary>
        /// Show Detailed View of Chair Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (await armChairService.Exists(id) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Armchair not Available";

                return RedirectToAction(nameof(All));

            }

            var armChairModel = await armChairService.ArmChairDetailsById(id);

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (armChairModel.CreatorId == userId)
            {
                armChairModel.IsCreator = true;
            }

            return View(armChairModel);
        }

        /// <summary>
        /// Get Add Chair Item View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var model = new ArmChairModel();

            return View(model);
        }

        /// <summary>
        /// Adds Chair item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(ArmChairModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[MessageConstant.ErrorMessage] = "Incorrect Input";

                return View(model);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            try
            {
                await armChairService.Add(model, userId);
            }
            catch (Exception e)
            {

                logger.LogError(nameof(Add), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot add item";
            }

            TempData[MessageConstant.SuccessMessage] = "Successfully added Armchair";

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
            if (await armChairService.Exists(id) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Armchair not Available";

                return RedirectToAction(nameof(All));
            }

            var armChair = await armChairService.ArmChairDetailsById(id);

            var model = new ArmChairModel
            {
                Id = armChair.Id,
                Name = armChair.Name,
                UpholsteryType = armChair.UpholsteryType,
                Length = armChair.Length,
                Width = armChair.Width,
                Height = armChair.Height,
                Price = armChair.Price,
                Quantity = armChair.Quantity,
                Description = armChair.Description,
                ImageUrl = armChair.ImageUrl
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
        public async Task<IActionResult> Edit(int id, ArmChairModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[MessageConstant.ErrorMessage] = "Incorrect Input";

                return View(model);
            }

            try
            {
                await armChairService.Edit(model.Id, model);
            }
            catch (Exception e)
            {
                logger.LogError(nameof(Edit), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot edit item";
            }

            TempData[MessageConstant.SuccessMessage] = "Successfully edited Armchair";

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
            try
            {
                await armChairService.Delete(id);
            }
            catch (Exception e)
            {
                logger.LogError(nameof(Delete), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot delete item";
            }

            TempData[MessageConstant.SuccessMessage] = "Successfully deleted Armchair";

            return RedirectToAction("All", "ArmChair");
        }
    }
}
