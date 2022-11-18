using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThreeHShop.Models;

namespace WebsiteThreeHShop.Controllers
{
    public class HomeController : Controller
    {
        QUANLYBANHANGEn data = new QUANLYBANHANGEn();
        public ActionResult Index()
        {
            return View();
        }

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
        public ActionResult Theloai()
        {
            var theloai = from n in data.DANHMUCs select n;
            return PartialView(theloai);
        }
        public ActionResult Sanphamnoibat()
        {
            var sp = from n in data.SANPHAMs select n;
            return PartialView(sp.Take(8));
        }
        public ActionResult Sanphammoi()
        {
            var sp = from n in data.SANPHAMs where n.MALSP == 6 select n;
            return PartialView(sp.Take(8));
        }
    }
}