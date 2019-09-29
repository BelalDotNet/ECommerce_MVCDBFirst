using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using System.Data.Entity;
using System.IO;

using PagedList.Mvc;
using PagedList;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        ECommerceEntities db = new ECommerceEntities();
        // GET: Product
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {

            ViewBag.CurrentSort = sortOrder;

            ViewBag.SortNameParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var products = from s in db.Products
                           select s;


            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName.ToUpper().Contains(searchString.ToUpper()));
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.ProductName);
                    break;

                default:
                    products = products.OrderBy(s => s.ProductName);
                    break;
            }


            int pageSize = 3;
            int pageNumber = page ?? 1;

            return View(products.ToPagedList(pageNumber, pageSize));



            
        }


        public ActionResult Create()
        {

            var brandList = db.MobileBrands.ToList();
            SelectList list = new SelectList(brandList, "BrandID", "BrandName");
            ViewBag.BrandName = list;

            return View();
                                            
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {

            string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            fileName = fileName + extension;
            product.ProductImage = "~/Uploads/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
            product.ImageFile.SaveAs(fileName);

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index", "ProductsForDashboard");
            }

            ModelState.Clear();
            return View();
        }

        [Route("Product/Details/{Id}")]
        public ActionResult Details(int? id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {
            Product product = db.Products.Find(id);
            if (ModelState.IsValid)
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }



    }
}