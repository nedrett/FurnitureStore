using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.Controllers.Furniture
{
    using Core.Contracts;
    using Core.Models.Furniture.Sofa;
    using System.Security.Claims;
    using HouseRentingSystem.Core.Constants;
    using FurnitureStore.Core.Services;
    using Microsoft.Extensions.Logging;

    public class SofaController : FurnitureController
    {
        private readonly ISofaService sofaService;
        private readonly ILogger logger;

        public SofaController(
            ISofaService _sofaService, 
            ILogger _logger)
        {
            sofaService = _sofaService;
            logger = _logger;
        }

        /// <summary>
        /// Shows all Sofa Items
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var sofaItems;
            try
            {
                sofaItems = await sofaService.GetAll();

            }
            catch (Exception e)
            {
                logger.LogError(nameof(All), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot get items";
            }

            if (sofaItems == null || sofaItems.Count() == 0)
            {
                TempData[MessageConstant.ErrorMessage] = "No Sofas are Available";

                return RedirectToAction("Catalog", "Furniture");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            foreach (var sofa in sofaItems)
            {
                if (sofa.CreatorId == userId)
                {
                    sofa.IsCreator = true;
                }
            }

            TempData[MessageConstant.SuccessMessage] = "All Available Sofas";


            return View("SofaCatalog", sofaItems);
        }

        /// <summary>
        /// Show Detailed View of Sofa Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (await sofaService.Exists(id) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Sofa not Available";


                return RedirectToAction(nameof(All));
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var sofaModel = await sofaService.SofaDetailsById(id);

            if (sofaModel.CreatorId == userId)
            {
                sofaModel.IsCreator = true;
            }

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
                TempData[MessageConstant.ErrorMessage] = "Incorrect Input";
                
                return View(model);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            try
            {
                await sofaService.Add(model, userId);
            }
            catch (Exception e)
            {

                logger.LogError(nameof(Add), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot add item";
            }

            TempData[MessageConstant.SuccessMessage] = "Successfully added Sofa";

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
                TempData[MessageConstant.ErrorMessage] = "Sofa not Available";
                
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
                TempData[MessageConstant.ErrorMessage] = "Incorrect Input";

                return View(model);
            }

            try
            {
                await sofaService.Edit(model.Id, model);
            }
            catch (Exception e)
            {
                logger.LogError(nameof(Edit), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot edit item";
            }

            TempData[MessageConstant.SuccessMessage] = "Successfully edited Sofa";
            
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
            try
            {
                await sofaService.Delete(id);
            }
            catch (Exception e)
            {
                logger.LogError(nameof(Delete), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot delete item";
            }

            TempData[MessageConstant.SuccessMessage] = "Successfully deleted Sofa";
            
            return RedirectToAction(nameof(All));
        }
    }
}
