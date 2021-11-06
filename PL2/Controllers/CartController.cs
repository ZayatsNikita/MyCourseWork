using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Extensions;
using PL.Infrastructure.Services.Abstract;
using PL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;

namespace PL.Controllers
{
    public class CartController : Controller
    {
        private IBuildStandartService _service;
        public CartController(IBuildStandartService service)
        {
            _service = service;
        }
        public ActionResult SS()
        {
            Dictionary<string, int> resutl = new Dictionary<string, int>();
            
            resutl.Add("kiril", 10);
            resutl.Add("natasha", 15);
            resutl.Add("dima", 12);
            resutl.Add("liza", 12);
            resutl.Add("masha", 12);

            return View(resutl);
        }
        public ViewResult ShowCart()
        {
            Cart cart = new Cart();
            cart.GetFromCoockie(HttpContext);
            return View("Cart", cart);
        }
        public ActionResult AddProduct(int count, int builderStandartId)
        {
            if (count < 1)
            {
                return RedirectToAction("List","Product");
            }
            BuildStandart standart = _service.ReadById(builderStandartId);
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
    }
}
