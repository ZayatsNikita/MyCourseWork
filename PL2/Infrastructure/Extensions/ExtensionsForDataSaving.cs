using Microsoft.AspNetCore.Http;
using PL.Models;
using PL.Models.ModelsForView;

namespace PL.Infrastructure.Extensions
{
    public static class ExtensionsForDataSaving
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
        public static void SaveToCookie(this FullUser fullUser, HttpContext context)
        {
            SaveUser(fullUser, context);
        }
        public static void GetUserFromCookie(this FullUser user, HttpContext context)
        {
            FullUser fullUser = GetFullUser(context);
            if(fullUser != null)
            {
                user.Worker = fullUser.Worker;
                user.Roles = fullUser.Roles;
                user.User = fullUser.User;
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

        private static void SaveUser(FullUser user, HttpContext context)
        {
            context.Session.SetJson("user", user);
        }
        private static FullUser GetFullUser(HttpContext context)
        {
            FullUser fullUser = context.Session.GetJson<FullUser>("user");
            return fullUser;
        }
    }
}
