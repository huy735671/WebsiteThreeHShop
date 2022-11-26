using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThreeHShop.Models;

namespace WebsiteThreeHShop.Models
{
    public class GioHang
    {
        QUANLYBANHANGEn data = new QUANLYBANHANGEn();
        public int iMASP { get; set; }
        public string sTENSP { get; set; }
        public string sHINHMINHHOA { get; set; }
        public double dGIACA { get; set; }
        public int iSOLUONG { get; set; }
        public double dTHANHTIEN
        {
            get { return iSOLUONG * dGIACA; }
        }

        public GioHang(int magh, int msp)
        {
            iMASP = msp;
            SANPHAM sp = data.SANPHAMs.FirstOrDefault(n => n.MASP == iMASP);
            sTENSP = sp.TENSP;
            sHINHMINHHOA = sp.HINHMINHHOA;
            dGIACA = double.Parse(sp.GIACA.ToString());
            var ct = data.CHITIETGHs.FirstOrDefault(c => c.MAGH == magh && c.MASP == msp);
            if (ct == null)
                iSOLUONG = 1;
            else
                iSOLUONG = Convert.ToInt32( ct.SOLUONG);
        }
        public class GioHangController : Controller
        {
            // GET: GioHang
            public ActionResult Index()
            {
                return View();
            }
        }


    }
}