
using Business.SalesOrder;
using Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Controllers
{
    public class HomeController : Controller
    {

        Service buss = new Service();
        public ActionResult Index()
        {
            var res = buss.GetOrderId();
            ViewData["OrderId"] = res.OrderId;
            return View();
        }

        [HttpPost]
        public ActionResult Manage(OrderListViewModel model)
        {
            var res = buss.Manage(model);
            return RedirectToAction("Index");
        }



        #region Json
        public JsonResult GetCustomerList()
        {
            var list = buss.GetCustomerList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }  
        public JsonResult GetProductList()
        {
            var list = buss.GetProductList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadAutoCompete(string Prefix)
        {
            var list= buss.GetProductNameList(Prefix);
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchCustomer(string Param)
        {
            var list =buss.GetCustomerName(Param);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchProduct(string Param)
        {
            var list = buss.SearchProduct(Param);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion Json

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}