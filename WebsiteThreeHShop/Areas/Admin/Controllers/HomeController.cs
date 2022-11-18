using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThreeHShop.Models;
using Microsoft.Web.Infrastructure;
using System.Web.Razor;
using System.Web.WebPages.Deployment;
using System.Web.WebPages.Razor;

namespace WebsiteThreeHShop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        QUANLYBANHANGEn dt=new QUANLYBANHANGEn();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var sHoten = collection["tendangnhap"];
            var sMatkhau = collection["matkhau"];
            var dangnhap = dt.ADMINs.SingleOrDefault(n => n.USERNAME == sHoten && n.PASSWORD == sMatkhau);
            if (dangnhap != null)
            {
                Session["Admin"] = dangnhap;
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu sai";
            }

            return View();
        }
    }
}
