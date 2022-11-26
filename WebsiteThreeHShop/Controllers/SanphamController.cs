using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThreeHShop.Models;
using PagedList;
using PagedList.Mvc;

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
        public ActionResult Danhmuc(int id, int? page)
        {
            /* var sanpham = data.SANPHAMs.Where(n => n.MADM == 1).Take(50).ToList();*/
            ViewBag.MADM = id;
            int isize = 9;
            var ipageNum = (page ?? 1);
            var sanpham = from s in data.SANPHAMs where s.MADM == id orderby s.MADM select s;
            return View(sanpham.ToPagedList(ipageNum, isize));
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