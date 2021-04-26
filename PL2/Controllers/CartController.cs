using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Extensions;
using PL.Infrastructure.Services.Abstract;
using PL.Models;
using System.Linq;

namespace PL.Controllers
{
    public class CartController : Controller
    {
        private IBuildStandartService _service;
        public CartController(IBuildStandartService service)
        {
            _service = service;
        }
        public ViewResult ShowCart()
        {
            Cart cart = new Cart();
            cart.GetFromCoockie(HttpContext);
            return View("Cart", cart);
        }
        public ActionResult AddProduct(int count, int builderStandartId)
        {
            BuildStandart standart = _service.Read(minId: builderStandartId, maxId: builderStandartId).FirstOrDefault();
            Cart cart = new Cart();
            cart.GetFromCoockie(HttpContext);
            if (cart == null)
            {
                cart = new Cart();
            }
            cart.Add(count, standart);
            cart.SaveToCoockie(HttpContext);
            return RedirectToAction(nameof(ShowCart));
        }
        public ActionResult Clear()
        {
            Cart cart = new Cart();
            cart.Clear();
            cart.SaveToCoockie(HttpContext);
            return RedirectToAction(nameof(ShowCart));
        }

        public ActionResult DeleteItem(int builderStandartId)
        {
            Cart cart = new Cart();
            cart.GetFromCoockie(HttpContext);
            cart.OrderLine.Remove(builderStandartId);
            cart.SaveToCoockie(HttpContext);
            return RedirectToAction(nameof(ShowCart));
        }

        //private void SaveCart(Cart cart)
        //{
        //    HttpContext.Session.SetJson("cart", cart);
        //}
        //private Cart GetCart()
        //{
        //    Cart cart = HttpContext.Session.GetJson<Cart>("cart");
        //    return cart;
        //}

    }
}
