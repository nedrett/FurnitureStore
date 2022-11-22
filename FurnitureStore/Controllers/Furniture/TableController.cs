using FurnitureStore.Core.Models.Furniture.Table;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.Controllers.Furniture
{
    using Core.Contracts;
    using System.Security.Claims;

    public class TableController : FurnitureController
    {
        private readonly ITableService tableService;

        public TableController(ITableService _tableService)
        {
            tableService = _tableService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var tableItems = await tableService.GetAll();

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
                return RedirectToAction(nameof(All));
            }

            var tableModel = await tableService.TableDetailsById(id);

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
                return View(model);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await tableService.Add(model, userId);

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
                return View(model);
            }

            await tableService.Edit(model.Id, model);

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

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await tableService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
