using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThreeHShop.Models;

namespace WebsiteThreeHShop.Controllers
{
    public class SanphamController : Controller
    {
        // GET: Sanpham
        QUANLYBANHANGEn data = new QUANLYBANHANGEn();
        // GET: Sanpham
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Shoplist()
        {
            return View();
        }
        public ActionResult Danhmuc(int id)
        {
            /* var sanpham = data.SANPHAMs.Where(n => n.MADM == 1).Take(50).ToList();*/
            var sanpham = from s in data.SANPHAMs where s.MADM == id select s;
            return View(sanpham);
        }

        public ActionResult DanhmucPartial()
        {
            return PartialView(data.DANHMUCs);
        }
        public ActionResult Sanpham()
        {
            var sp = from n in data.SANPHAMs select n;
            return View(sp.Take(8));
        }
        public ActionResult Chitietsanpham(int id)
        {
            var sp = from n in data.SANPHAMs where n.MASP == id select n;
            return View(sp);
        }
    }
}