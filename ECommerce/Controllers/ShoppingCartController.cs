using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
namespace ECommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        ECommerceEntities db = new ECommerceEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View(db.ShoppingCarts.ToList());
        }
    }
}