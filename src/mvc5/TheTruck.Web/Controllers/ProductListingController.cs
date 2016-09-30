using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using TheTruck.Web.DataContexts;
using TheTruck.Web.Services;

namespace TheTruck.Web.Controllers
{
    public class ProductListingController : Controller
    {
        private ProductDb db = new ProductDb();
        private CartService cartService;

        public ProductListingController()
        {
            cartService = new CartService(System.Web.HttpContext.Current.Session);
        }

        // GET: ProductListing
        public ActionResult Index()
        {
            return RedirectToAction("Search", new { startIndex = 0 });
        }

        // GET: Products/AddToCart/5
        public JsonResult AddToCart(int id)
        {
            cartService.AddProduct(id);
            return Json(cartService.GetProducts().Count, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search(int startIndex)
        {
            int pageSize = int.Parse(ConfigurationManager.AppSettings["productPerPage"]);

            var products = db.Products.OrderBy(x => x.Category).ThenBy(x => x.Name).Skip(startIndex).Take(pageSize);

            ViewBag.numberOfProducts = db.Products.Count();
            ViewBag.pageSize = pageSize;
            ViewBag.currentPage = startIndex / pageSize;

            return View(products);
        }
    }
}
