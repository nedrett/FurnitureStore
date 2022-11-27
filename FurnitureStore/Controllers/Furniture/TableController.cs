using FurnitureStore.Core.Models.Furniture.Table;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.Controllers.Furniture
{
    using Core.Contracts;
    using HouseRentingSystem.Core.Constants;
    using System.Security.Claims;

    public class TableController : FurnitureController
    {
        private readonly ITableService tableService;

        public TableController(ITableService _tableService)
        {
            tableService = _tableService;
        }

        /// <summary>
        /// Shows all Table Items
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var tableItems = await tableService.GetAll();

            if (tableItems == null || tableItems.Count() == 0)
            {
                TempData[MessageConstant.ErrorMessage] = "No Tables are Available";

                return RedirectToAction("Catalog", "Furniture");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            foreach (var table in tableItems)
            {
                if (table.CreatorId == userId)
                {
                    table.IsCreator = true;
                }
            }

            TempData[MessageConstant.SuccessMessage] = "All Available Tables";

            return View("TableCatalog", tableItems);
        }

        /// <summary>
        /// Show Detailed View of Table Item
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (await tableService.Exists(id) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Table not Available";

                return RedirectToAction(nameof(All));
            }

            var tableModel = await tableService.TableDetailsById(id);

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (tableModel.CreatorId == userId)
            {
                tableModel.IsCreator = true;
            }

            return View(tableModel);
        }

        /// <summary>
        /// Get Add Table Item View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var model = new TableModel();

            return View(model);
        }

        /// <summary>
        /// Adds Table item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(TableModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[MessageConstant.ErrorMessage] = "Incorrect Input";

                return View(model);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await tableService.Add(model, userId);

            TempData[MessageConstant.SuccessMessage] = "Successfully added Table";

            return RedirectToAction(nameof(All));
        }

        /// <summary>
        /// Get Edit Table item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await tableService.Exists(id) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Table not Available";

                return RedirectToAction(nameof(All));

            }

            var table = await tableService.TableDetailsById(id);

            var model = new TableModel
            {
                Id = table.Id,
                Type = table.Type,
                Material = table.Material,
                Width = table.Width,
                Length = table.Length,
                Price = table.Price,
                Quantity = table.Quantity,
                Description = table.Description,
                ImageUrl = table.ImageUrl
            };

            return View(model);
        }

        /// <summary>
        /// Edits Table item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TableModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[MessageConstant.ErrorMessage] = "Incorrect Input";

                return View(model);
            }

            await tableService.Edit(model.Id, model);

            TempData[MessageConstant.SuccessMessage] = "Successfully edited Table";
            
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        /// <summary>
        /// User not owner buy Table item
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
            await tableService.Delete(id);

            TempData[MessageConstant.SuccessMessage] = "Successfully deleted Table";
            
            return RedirectToAction(nameof(All));
        }
    }
}
