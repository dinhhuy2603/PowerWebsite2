using PowerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (DBModel db = new DBModel())
            {
                if (Session["UserID"] != null)
                {
                    var hienthi = db.hienthi.FirstOrDefault();
                    var trangthai = db.trangthai.FirstOrDefault();
                    ViewBag.hienthi = hienthi != null ? hienthi : new Hienthiweb();
                    ViewBag.trangthai = trangthai != null ? trangthai : new Trangthai();
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }

        public ActionResult ChartCb1()
        {
            using (DBModel db = new DBModel())
            {
                if (Session["UserID"] != null)
                {
                    var hienthi = db.hienthi.FirstOrDefault();
                    ViewBag.hienthi = hienthi != null ? hienthi : new Hienthiweb();
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }

        public ActionResult ChartCb2()
        {
            using (DBModel db = new DBModel())
            {
                if (Session["UserID"] != null)
                {
                    var hienthi = db.hienthi.FirstOrDefault();
                    ViewBag.hienthi = hienthi != null ? hienthi : new Hienthiweb();
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }

        public ActionResult ChartCb3()
        {
            using (DBModel db = new DBModel())
            {
                if (Session["UserID"] != null)
                {
                    var hienthi = db.hienthi.FirstOrDefault();
                    ViewBag.hienthi = hienthi != null ? hienthi : new Hienthiweb();
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }

        [HttpGet]
        public JsonResult GetHienThiOverview()
        {
            using (DBModel db = new DBModel())
            {
                var hienthi = db.hienthi.FirstOrDefault();
                var trangthai = db.trangthai.FirstOrDefault();
                var data = new List<object>();
                data.Add(hienthi);
                data.Add(trangthai);
                return Json(data.ToArray(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}