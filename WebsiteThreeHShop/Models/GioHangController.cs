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

        public GioHang(int msp)
        {
            iMASP = msp;
            SANPHAM sp = data.SANPHAMs.Single(n => n.MASP == iMASP);
            sTENSP = sp.TENSP;
            sHINHMINHHOA = sp.HINHMINHHOA;
            dGIACA = double.Parse(sp.GIACA.ToString());
            iSOLUONG = 1;
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