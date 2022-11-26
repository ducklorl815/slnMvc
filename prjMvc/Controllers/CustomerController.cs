using prjMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvc.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult List()
        {
            string keyword = Request.Form["txtkeyword"];
            List<CCustomer> datas = null;
            if(string.IsNullOrEmpty(keyword))
                datas = (new CCustomerFactory()).queryAll();
            else
                datas = (new CCustomerFactory()).queryByKeyword(keyword);
            return View(datas);
        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Save()
        {
            CCustomer x = new CCustomer();
            x.fName = Request.Form["txtName"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fAddress = Request.Form["txtAddress"];
            x.fPassword = Request.Form["txtPassword"];
            x.fActived = bool.Parse(Request.Form["txtActived"]);
            (new CCustomerFactory()).create(x);
            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)
        {
            if(id!=null)
            {
                CCustomer x = (new CCustomerFactory()).queryById((int)id);
                if(x!=null)
                    if (x.fActived)
                        ViewBag.x = "false";
                    else
                        ViewBag.y= "true";
                return View(x);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(CCustomer x)  //用name與value 同名方式 屬性命名 可省略
        {
            (new CCustomerFactory()).update(x);
            if (x != null)
                if (x.fActived)
                    ViewBag.x = "false";
                else
                    ViewBag.y = "true";
            return RedirectToAction("List");
        }
        //public ActionResult Edit()
        //{
        //    CCustomer x = new CCustomer();
        //    x.fid = int.Parse(Request.Form["txtfid"]);
        //    x.fName = Request.Form["txtName"];
        //    x.fPhone = Request.Form["txtPhone"];
        //    x.fEmail = Request.Form["txtEmail"];
        //    x.fAddress = Request.Form["txtAddress"];
        //    x.fPassword = Request.Form["txtPassword"];
        //    x.fActived = bool.Parse(Request.Form["txtActived"]);
        //    (new CCustomerFactory()).update(x);
        //    return RedirectToAction("List");
        //}
        public ActionResult Delete(int? id)
        {
            if (id != null)
                (new CCustomerFactory()).delete((int)id);
            return RedirectToAction("List");
        }



    }
}