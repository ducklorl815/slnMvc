using prjMvc.Models;
using prjMvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvc.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult List()
        {
            var products = from p in (new dbDemoEntities()).tProducts
                           select p;
            return View(products);
        }
        public ActionResult CartView()
        {
            List<CShoppingCartItem> cart = Session[CDictionary.SK_已購買_產品_清單] as List<CShoppingCartItem>;
            if(cart==null)
                return RedirectToAction("List");
            return View(cart);
        }

        public ActionResult AddToSession(int? id)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProducts.FirstOrDefault(p => p.fId == id);
            if (id != null)
                return View(prod);
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult AddToSession(CAddToCartViewModel p)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProducts.FirstOrDefault(t => t.fId == p.txtfid);
            if (prod != null)
            {
                List<CShoppingCartItem> cart = Session[CDictionary.SK_已購買_產品_清單] as List<CShoppingCartItem>;
                if (cart == null)
                {
                    cart = new List<CShoppingCartItem>();
                    Session[CDictionary.SK_已購買_產品_清單] = cart;
                }
                CShoppingCartItem item = new CShoppingCartItem()
                {
                    count = p.txtCount,
                    price = (decimal)prod.fPrice,
                    productId = prod.fId
                };
                cart.Add(item);
            }

            return RedirectToAction("List");
        }



        public ActionResult AddToCart(int? id)
        { 
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProducts.FirstOrDefault(p => p.fId == id);
            if (id != null)
                return View(prod);
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel p)
        {
            dbDemoEntities db = new dbDemoEntities();
            tProduct prod = db.tProducts.FirstOrDefault(t => t.fId == p.txtfid);
            if (prod != null)
            {
                tShoppingCart item = new tShoppingCart();
                item.fCount = p.txtCount;
                item.fCustomer = 1;
                item.fDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                item.fPrice = prod.fPrice;
                item.fProduct = prod.fId;
                db.tShoppingCarts.Add(item);
                db.SaveChanges();
            }
 
            return RedirectToAction("List");
        }
    }
}