using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThreeHShop.Models;

namespace WebsiteThreeHShop.Controllers
{
    public class GioHangController : Controller
    {
        QUANLYBANHANGEn data = new QUANLYBANHANGEn();
        // GET: GioHang

        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        // Them sp vao gio hang
        public ActionResult ThemGioHang(int msp, string url)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Find(n => n.iMASP == msp);
            if (sp == null)
            {
                sp = new GioHang(msp);
                lstGioHang.Add(sp);
            }
            else
            {
                sp.iSOLUONG++;
            }
            return Redirect(url);
        }

        // Tinh tong so luong
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSOLUONG);
            }
            return iTongSoLuong;
        }

        //Tinh tong tien
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dTHANHTIEN);
            }
            return dTongTien;
        }
        //Tinh tien thue VAT(10%)
        private double TongVAT()
        {
            double dVAT = 0;
            double dTongVAT = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dVAT = lstGioHang.Sum(n => n.dTHANHTIEN);
                dTongVAT = (dVAT * 10) / 100;
            }
            return dTongVAT;
        }

        //Action tra ve view GioHang
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            /* Neu khong co đn, không chạy
            if(lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }*/

            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            ViewBag.TongVAT = TongVAT();
            //Tinh tong tien = Tong tien sp + thue VAT(10%)
            ViewBag.TongTienVAT = TongTien() + TongVAT();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xoa san pham khoi gio hang
        public ActionResult XoaSPKhoiGioHang(int iMASP)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMASP == iMASP);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.iMASP == iMASP);
                /*   if(lstGioHang.Count ==0)
                   {
                       return RedirectToAction("Index", "Home");
                   }*/
            }
            return RedirectToAction("GioHang");
        }
        //Cap nhat gio hang
        public ActionResult CapNhatGioHang(int iMASP, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMASP == iMASP);
            if (sp != null)
            {
                sp.iSOLUONG = int.Parse(f["txtSOLUONG"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult khuyenmai1(FormCollection f)
        {
            var magg = f["makhuyenmai"];
            ViewBag.giamgia = TongTien();

            foreach (var a in data.KHUYENMAIs)
            {
                if (String.Compare(a.MAKM, magg, false) < 0)
                {
                    ViewBag.thongbao = "Mã giảm giá không tồn tại";
                }
                if (String.Compare(a.MAKM, magg, false) == 0)
                {
                    ViewBag.TongTien = Convert.ToInt32(ViewBag.giamgia * a.PHANTRAMKHUYENMAI / 100);

                }
            }
            return RedirectToAction("GioHang");
        }
    }
}