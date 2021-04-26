using Microsoft.AspNetCore.Http;
using PL.Models;

namespace PL.Infrastructure.Extensions
{
    public static class StoringOrderCards
    {
        public static void SaveToCoockie(this Cart cart, HttpContext context)
        {
            SaveCart(cart, context);
        }
        public static void GetFromCoockie(this Cart cart, HttpContext context)
        {
            Cart newCart = GetCart(context);
            if (newCart != null)
            {
                cart.OrderLine = newCart.OrderLine;
            }
        }


        private static void SaveCart(Cart cart, HttpContext context)
        {
            context.Session.SetJson("cart", cart);
        }
        private static Cart GetCart(HttpContext context)
        {
            Cart cart = context.Session.GetJson<Cart>("cart");
            return cart;
        }
    }
}
