using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore.Controllers
{
    using Core.Models.Furniture;
    using Extensions;
    using Infrastructure.Data.Models;

    public class CartController : Controller
    {

        public IActionResult Index()
        {
            var cart = SessionExtensions.Get<List<Product>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            return View();

        }

        //public IActionResult Buy(ProductViewModel model)
        //{


        //    if (SessionExtensions.Get<List<Product>>(HttpContext.Session, "cart") == null)
        //    {
        //        List<Product> cart = new List<Product>();
        //        cart.Add(new Product { Id = model.Id, Name = model.Name, ImageUrl = model.ImageUrl, Price = model.Price, Quantity = 1 });
        //        SessionExtensions.Set(HttpContext.Session, "cart", cart);
        //    }
        //    else
        //    {
        //        List<Product> cart = SessionExtensions.Get<List<Product>>(HttpContext.Session, "cart");
        //        int index = isExist(model.Id);
        //        if (index != -1)
        //        {
        //            cart[index].Quantity++;
        //        }
        //        else
        //        {
        //            cart.Add(new Product { Id = model.Id, Name = model.Name, ImageUrl = model.ImageUrl, Price = model.Price, Quantity = 1 });
        //            SessionExtensions.Set(HttpContext.Session, "cart", cart);
        //        }
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

        //public IActionResult Remove(ProductViewModel model)
        //{
        //    List<Product> cart = SessionExtensions.Get<List<Product>>(HttpContext.Session, "cart");
        //    int index = isExist(model.Id);
        //    cart.RemoveAt(index);
        //    SessionExtensions.Set(HttpContext.Session, "cart", cart);

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
