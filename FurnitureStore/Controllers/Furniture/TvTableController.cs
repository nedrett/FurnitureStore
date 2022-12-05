using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FurnitureStore.Areas.Admin.AdminConstants;

namespace FurnitureStore.Controllers.Furniture
{
    using Core.Contracts;
    using HouseRentingSystem.Core.Constants;
    using System.Security.Claims;
    using Core.Models.Furniture.TvTable;

    public class TvTableController : FurnitureController
    {
        private readonly ITvTableService tvTableService;
        private readonly ILogger<TvTableController> logger;

        public TvTableController(
            ITvTableService _tvTableService,
            ILogger<TvTableController> _logger)
        {
            tvTableService = _tvTableService;
            logger = _logger;
        }

        /// <summary>
        /// Shows all TvTable Items
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            IEnumerable<TvTableCatalogModel> tvTableItems = null;

            try
            {
                tvTableItems = await tvTableService.GetAll();

            }
            catch (Exception e)
            {
                logger.LogError(nameof(All), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot get items";
            }

            if (tvTableItems == null || tvTableItems.Count() == 0)
            {
                TempData[MessageConstant.ErrorMessage] = "No Tv Tables are Available";

                return RedirectToAction("Catalog", "Furniture");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            foreach (var tvTable in tvTableItems)
            {
                if (tvTable.CreatorId == userId)
                {
                    tvTable.IsCreator = true;
                }
            }

            TempData[MessageConstant.SuccessMessage] = "All Available Tables";

            return View("TvTableCatalog", tvTableItems);
        }

        /// <summary>
        /// Show Detailed View of TvTable Item
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (await tvTableService.Exists(id) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Tv Table not Available";

                return RedirectToAction(nameof(All));
            }

            var tvTableModel = await tvTableService.TvTableDetailsById(id);

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (tvTableModel.CreatorId == userId)
            {
                tvTableModel.IsCreator = true;
            }

            return View(tvTableModel);
        }

        /// <summary>
        /// Get Add TvTable Item View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var model = new TvTableModel();

            return View(model);
        }

        /// <summary>
        /// Adds TvTable item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(TvTableModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[MessageConstant.ErrorMessage] = "Incorrect Input";

                return View(model);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            try
            {
                await tvTableService.Add(model, userId);
            }
            catch (Exception e)
            {

                logger.LogError(nameof(Add), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot add item";
            }

            TempData[MessageConstant.SuccessMessage] = "Successfully added Tv Table";

            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Get Edit TvTable item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await tvTableService.Exists(id) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Tv Table not Available";

                return RedirectToAction(nameof(All));

            }

            var tvTable = await tvTableService.TvTableDetailsById(id);

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var model = new TvTableModel
            {
                Id = tvTable.Id,
                Name = tvTable.Name,
                Width = tvTable.Width,
                Length = tvTable.Length,
                Height = tvTable.Height,
                Price = tvTable.Price,
                Quantity = tvTable.Quantity,
                Description = tvTable.Description,
                ImageUrl = tvTable.ImageUrl,
                CreatorId = tvTable.CreatorId
            };

            if (tvTable.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
            {
                return View(model);
            }
            else
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
        }

        /// <summary>
        /// Edits TvTable item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TvTableModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[MessageConstant.ErrorMessage] = "Incorrect Input";

                return View(model);
            }

            try
            {
                await tvTableService.Edit(model.Id, model);
            }
            catch (Exception e)
            {
                logger.LogError(nameof(Edit), e);
                TempData[MessageConstant.ErrorMessage] = "Cannot edit item";
            }

            TempData[MessageConstant.SuccessMessage] = "Successfully edited Tv Table";

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        /// <summary>
        /// User not owner buy TvTable item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            var tvTable = await tvTableService.TvTableDetailsById(id);
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (tvTable.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
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
            var tvTable = await tvTableService.TvTableDetailsById(id);
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (tvTable.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
            {
                try
                {
                    await tvTableService.Delete(id);
                }
                catch (Exception e)
                {
                    logger.LogError(nameof(Delete), e);
                    TempData[MessageConstant.ErrorMessage] = "Cannot delete item";
                }

                TempData[MessageConstant.SuccessMessage] = "Successfully deleted Tv Table";

                return RedirectToAction(nameof(All));
            }
            else
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
        }
    }
}
