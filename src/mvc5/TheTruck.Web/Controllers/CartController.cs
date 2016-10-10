using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TheTruck.Entities;
using TheTruck.Web.DataContexts;
using TheTruck.Web.Models;
using TheTruck.Web.Services;

namespace TheTruck.Web.Controllers
{
    public class CartController : Controller
    {
        private ProductDb db = new ProductDb();
        private CartService cartService = new CartService(System.Web.HttpContext.Current.Session);

        private Dictionary<int, int> GetQuantities(List<int> ids)
        {
            Dictionary<int, int> quantities = new Dictionary<int, int>();

            foreach (var id in ids)
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

            return quantities;
        }

        // GET: Cart
        public ActionResult ShowCart()
        {
            var productIds = cartService.GetProducts();
            List<CartViewModel> cart = new List<CartViewModel>();

            var quantities = GetQuantities(productIds);

            var products = db.Products.Where(p => productIds.Contains(p.Id));

            // populate cart view model
            foreach (var p in products)
            {
                var qty = quantities[p.Id];
                var subTotal = qty * p.Price;

                cart.Add(new CartViewModel
                {
                    Id = p.Id,
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

        public ActionResult RmFromCart(int id)
        {
            cartService.RemoveProduct(id);
            return RedirectToAction("ShowCart");
        }

        public ActionResult AddToCart(int id)
        {
            cartService.AddProduct(id);
            return RedirectToAction("ShowCart");
        }

        [Authorize]
        public ActionResult ValidateCart()
        {
            // First check if it is the first order
            var isFirstOrder = db.Surveys.Where(s => s.Username == User.Identity.Name).Count() > 0 ? false : true;

            // If first order force fill in the survey
            if (isFirstOrder)
                return RedirectToAction("Create", "Surveys");


            var productIds = cartService.GetProducts();
            var quantities = GetQuantities(productIds);
            var orderitems = new List<OrderItem>();

            var products = db.Products.Where(p => productIds.Contains(p.Id));

            // populate order items
            foreach (var p in products)
            {
                orderitems.Add(new OrderItem
                {
                    ProductId = p.Id,
                    Quantity = quantities[p.Id],
                    UnitPrice = p.Price,
                    Name = p.Name,
                    Image = p.Image
                });
            }

            // Create the order
            var order = new Order { DateTime = DateTime.Now, Username = User.Identity.Name, Items = orderitems };

            // Save into the database
            db.Orders.Add(order);
            db.SaveChanges();

            return RedirectToAction("OrderSuccessful", "Orders");

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