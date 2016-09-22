using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using TheTruck.Entities;
using TheTruck.Web.DataContexts;
using TheTruck.Web.Services;

namespace TheTruck.Web.Controllers
{
    public class ProductsController : Controller
    {
        private ProductDb db = new ProductDb();
        private string ImagePath = "/images/";
        private CartService cartService;

        public ProductsController()
        {
            cartService = new CartService(System.Web.HttpContext.Current.Session);
        }

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/AddToCart/5
        public JsonResult AddToCart(int id)
        {
            cartService.AddProduct(id);
            return Json(cartService.GetProducts().Count, JsonRequestBehavior.AllowGet);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Category,Name,Price,Description")] Product product)
        {
            if (ModelState.IsValid && Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    SaveImage(file, product);
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        private void SaveImage(HttpPostedFileBase file, Product product)
        {
            var filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var relativePath = ImagePath + filename;
            var fullPath = HostingEnvironment.MapPath(relativePath);
            file.SaveAs(fullPath);
            product.Image = relativePath;
        }

        private void DeleteImage(Product product)
        {
            if (!String.IsNullOrEmpty(product.Image))
            {
                var fullPath = HostingEnvironment.MapPath(product.Image);
                System.IO.File.Delete(fullPath);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Category,Name,Price,Description,Image")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        if (!String.IsNullOrEmpty(product.Image))
                        {
                            var path = HostingEnvironment.MapPath(product.Image);
                            file.SaveAs(path);
                        }
                        else
                        {
                            SaveImage(file, product);
                        }
                    }
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            DeleteImage(product);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
