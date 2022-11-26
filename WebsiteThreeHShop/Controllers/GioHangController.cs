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
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            var makh = kh.MAKH;
            var magh = data.GIOHANGs.FirstOrDefault(gh => gh.MAKH == makh).MAGH;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                var ctgh = data.CHITIETGHs.Where(c => c.MAGH == magh).ToList();
                foreach(CHITIETGH ct in ctgh)
                {
                    GioHang gh = new GioHang(magh, ct.MASP);
                    lstGioHang.Add(gh);
                }
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        // Them sp vao gio hang
        public ActionResult ThemGioHang(int msp, string url)
        {
            List<GioHang> lstGioHang = LayGioHang();
            KHACHHANG kh = (KHACHHANG) Session["TaiKhoan"];
            var makh = kh.MAKH;
            var magh = data.GIOHANGs.FirstOrDefault(gh => gh.MAKH == makh).MAGH;
            GioHang sp = lstGioHang.Find(n => n.iMASP == msp);
            if (sp == null)
            {
               
                var ct = new CHITIETGH();

                ct.MAGH = magh;
                ct.MASP = msp;
                ct.SOLUONG = 1;
                data.CHITIETGHs.Add(ct);
                data.SaveChanges();
                sp = new GioHang(msp, magh);
                lstGioHang.Add(sp);
            }
            else
            {
                sp.iSOLUONG++;
                var ct = data.CHITIETGHs.FirstOrDefault(c=> c.MAGH== magh && c.MASP== msp);
                ct.SOLUONG = sp.iSOLUONG;
                data.SaveChanges();
                
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
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            var makh = kh.MAKH;
            var idkm= data.GIOHANGs.FirstOrDefault(g => g.MAKH == makh).IDKM;
            var pt = data.KHUYENMAIs.FirstOrDefault(k => k.ID == idkm).PHANTRAMKHUYENMAI;

            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            //ViewBag.TongTien = TongTien()*(1-pt);
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
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            var makh = kh.MAKH;
            var magh = data.GIOHANGs.FirstOrDefault(gh => gh.MAKH == makh).MAGH;
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMASP == iMASP);
            if (sp != null)
            {
                var ct = data.CHITIETGHs.FirstOrDefault(c=> c.MAGH == magh && c.MASP == iMASP);
                data.CHITIETGHs.Remove(ct);
                data.SaveChanges();

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
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            var makh = kh.MAKH;
            var magh = data.GIOHANGs.FirstOrDefault(gh => gh.MAKH == makh).MAGH;
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMASP == iMASP);
            if (sp != null)
            {
                var ct = data.CHITIETGHs.FirstOrDefault(c => c.MAGH == magh && c.MASP == iMASP);
                ct.SOLUONG = int.Parse(f["txtSOLUONG"].ToString());
                data.SaveChanges();
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
            var makm = f["makhuyenmai"];
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            var makh = kh.MAKH;
            var gh = data.GIOHANGs.FirstOrDefault(g => g.MAKH == makh);
            gh.IDKM = data.KHUYENMAIs.FirstOrDefault(km => km.MAKM == makm).ID;
            data.SaveChanges();


            return RedirectToAction("GioHang");
        }
    }
}