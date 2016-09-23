using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TheTruck.Web.DataContexts;
using TheTruck.Web.Models;
using TheTruck.Web.Services;

namespace TheTruck.Web.Controllers
{
    public class CartController : Controller
    {
        private ProductDb db = new ProductDb();
        private CartService cartService = new CartService(System.Web.HttpContext.Current.Session);

        // GET: Cart
        public ActionResult ShowCart()
        {
            var productIds = cartService.GetProducts();
            List<CartViewModel> cart = new List<CartViewModel>();

            Dictionary<int, int> quantities = new Dictionary<int, int>();

            foreach (var id in productIds)
            {
                if (quantities.ContainsKey(id))
                {
                    quantities[id]++;
                }
                else
                {
                    quantities.Add(id, 1);
                }
            }

            var products = db.Products.Where(p => productIds.Contains(p.Id));

            // populate cart view model
            foreach (var p in products)
            {
                var qty = quantities[p.Id];
                var subTotal = qty * p.Price;

                cart.Add(new CartViewModel
                {
                    Category = p.Category,
                    Image = p.Image,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = qty,
                    SubTotal = subTotal
                });
            }

            return View(cart);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}