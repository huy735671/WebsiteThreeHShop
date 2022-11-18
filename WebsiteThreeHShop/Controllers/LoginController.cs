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
namespace WebsiteThreeHShop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        QUANLYBANHANGEn data = new QUANLYBANHANGEn();


        // GET: Users
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Dangky(KHACHHANG kh, FormCollection collection)
        {
            int year = DateTime.Now.Year;

            var sHoten = collection["HoTen"];
            var sTenDN = collection["TenDN"];
            var sMatkhau = collection["Matkhau"];
            var sMatkhauNhaplai = collection["MatKhauNL"];
            var sDiachi = collection["Diachi"];
            var sDienthoai = collection["DienThoai"];
            var sEmail = collection["Email"];
            var dNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            if (data.KHACHHANGs.SingleOrDefault(n => n.GMAIL == sEmail) != null)
            {
                ViewBag.thongbao = "Email này đã được sử dụng";
            }
            else if (data.KHACHHANGs.SingleOrDefault(n => n.USERNAME == sTenDN) != null)
            {
                ViewBag.thongbao = "Tên đăng nhập này đã được sử dụng";
            }
            else
            {
                kh.NAME = sHoten;
                kh.USERNAME = sTenDN;
                kh.PASSWORD = sMatkhau;
                kh.GMAIL = sEmail;
                kh.ADDRESS = sDiachi;
                kh.TUOI = kh.TUOI;
                kh.PHONE = sDienthoai;

                data.KHACHHANGs.Add(kh);

                data.SaveChanges();
                return RedirectToAction("DangNhap");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var sTenDn = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.USERNAME == sTenDn && n.PASSWORD == sMatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["Taikhoan"] = kh;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tên Đăng Nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogout");
        }
    }
}