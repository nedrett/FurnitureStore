using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FurnitureStore.Areas.Admin.AdminConstants;


namespace FurnitureStore.Controllers.Furniture
{
    using Core.Contracts;
    using Core.Models.Furniture.ArmChair;
    using Core.Constants;
    using Extensions;
    using System.Security.Claims;
    using Core.Models.Furniture;
    using FurnitureStore.Infrastructure.Data.Models;

    public class ArmChairController : FurnitureController
    {
        private readonly IArmChairService armChairService;
        private readonly ILogger<ArmChairController> logger;

        public ArmChairController(
            IArmChairService _armChairService,
            ILogger<ArmChairController> _logger)
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
            IEnumerable<ArmChairCatalogModel> armChairItems = null!;

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

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

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
                ImageUrl = armChair.ImageUrl,
                CreatorId = armChair.CreatorId
            };

            if (armChair.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
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
        //[HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            var armChair = await armChairService.ArmChairDetailsById(id);
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (armChair.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
            else
            {
                ProductViewModel product = new ProductViewModel();
                product.Id = armChair.Id;
                product.Name = armChair.Name;
                product.ImageUrl = armChair.ImageUrl;
                product.Price = armChair.Price;

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
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var armChair = await armChairService.ArmChairDetailsById(id);
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (armChair.CreatorId == userId || User.IsInRole($"{AdminRoleName}"))
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
