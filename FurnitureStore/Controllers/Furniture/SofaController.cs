using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FurnitureStore.Areas.Admin.AdminConstants;

namespace FurnitureStore.Controllers.Furniture
{
    using Core.Contracts;
    using Extensions;
    using Core.Models.Furniture.Sofa;
    using Core.Constants;
    using Microsoft.Extensions.Logging;
    using System.Security.Claims;
    using FurnitureStore.Core.Models.Furniture;
    using FurnitureStore.Infrastructure.Data.Models;

    public class SofaController : FurnitureController
    {
        private readonly ISofaService sofaService;
        private readonly ILogger<SofaController> logger;

        public SofaController(
            ISofaService _sofaService,
            ILogger<SofaController> _logger)
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
            IEnumerable<SofaCatalogModel> sofaItems = null;

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

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

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

            if (sofa.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
            {
                return View(model);
            }
            else
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
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
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var sofa = await sofaService.SofaDetailsById(id);

            if (sofa.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
            else
            {
                ProductViewModel product = new ProductViewModel();
                product.Id = sofa.Id;
                product.Name = sofa.Name;
                product.ImageUrl = sofa.ImageUrl;
                product.Price = sofa.Price;

                if (SessionExtensions.Get<List<Product>>(HttpContext.Session, "cart") == null)
                {
                    List<Product> cart = new List<Product>();
                    cart.Add(new Product
                    {
                        Id = product.Id,
                        Name = product.Name,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        Quantity = 1
                    });
                    SessionExtensions.Set(HttpContext.Session, "cart", cart);
                }
                else
                {
                    List<Product> cart = SessionExtensions.Get<List<Product>>(HttpContext.Session, "cart");
                    bool productExist = isExist(product.Name);
                    if (productExist)
                    {
                        cart.First(p => p.Name == product.Name).Quantity++;
                        foreach (var item in cart)
                        {
                            if (item.Name == product.Name)
                            {
                                item.Quantity++;
                            }
                        }
                    }
                    else
                    {
                        cart.Add(new Product
                        {
                            Id = product.Id,
                            Name = product.Name,
                            ImageUrl = product.ImageUrl,
                            Price = product.Price,
                            Quantity = 1
                        });
                        SessionExtensions.Set(HttpContext.Session, "cart", cart);
                    }
                }
                return RedirectToAction(nameof(All));
            }
        }

        /// <summary>
        /// Changes IsActive flag to false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var sofa = await sofaService.SofaDetailsById(id);

            if (sofa.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
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
            else
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

        }

        private bool isExist(string name)
        {
            List<Product> cart = SessionExtensions.Get<List<Product>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
