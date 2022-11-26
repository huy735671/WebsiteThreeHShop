using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteThreeHShop.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;

namespace WebsiteThreeHShop.Areas.Admin.Controllers
{
    public class QuanTriController : Controller
    {
        QUANLYBANHANGEn db = new QUANLYBANHANGEn();
        // GET: Admin/QuanTri
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 10;
            return View(db.SANPHAMs.ToList().OrderBy(n => n.MASP).ToPagedList(iPageNum, iPageSize));
        }
        public ActionResult Quanlinguoidung(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MAKH).ToPagedList(iPageNum, iPageSize));
        }
        public ActionResult Quanlidanhmuc(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.DANHMUCs.ToList().OrderBy(n => n.MADM).ToPagedList(iPageNum, iPageSize));
        }

      /*  public ActionResult Hopthulienhe(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.DANHMUCs.ToList().OrderBy(n => n.MADM).ToPagedList(iPageNum, iPageSize));
        } */

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MADM = new SelectList(db.DANHMUCs.ToList().OrderBy(n => n.TENDM), "MADM", "TENDM");
            ViewBag.LSP = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TENLSP), "MALSP", "TENLSP");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SANPHAM Sanpham, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            ViewBag.MADM = new SelectList(db.DANHMUCs.ToList().OrderBy(n => n.TENDM), "MADM", "TENDM");
            ViewBag.LSP = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TENLSP), "MALSP", "TENLSP");

            if (fFileUpload == null)
            {
                ViewBag.ThongBao = "Hay chon anh bia";
                ViewBag.TENSP = f["sTenSach"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MADM = new SelectList(db.DANHMUCs.ToList().OrderBy(n => n.TENDM), "MADM", "TENDM", int.Parse(f["MADM"]));
                ViewBag.LSP = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TENLSP), "MALSP", "TENLSP", int.Parse(f["TENLSP"]));
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/imgweb"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    Sanpham.TENSP = f["sTenSach"];
                    Sanpham.NOIDUNGTT = f["sMoTa"].Replace("<p>", "").Replace("</p>", "/n");
                    Sanpham.HINHMINHHOA = sFileName;
                    Sanpham.GIACA = float.Parse(f["mGiaBan"]);
                    Sanpham.MADM = int.Parse(f["MaCD"]);
                    Sanpham.MALSP = int.Parse(f["MaNXB"]);
                    db.SANPHAMs.Add(Sanpham);
                    db.SaveChanges();
                }
                return View();
            }
            
            }
        public ActionResult Details(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sach = db.SANPHAMs.SingleOrDefault(n => n.MASP == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh = db.CHITIETGHs.Where(n => n.MASP == id);
            if (ctdh.Count() > 0)
            {
                ViewBag.ThongBao = "Sach nay da co trong bang chi tiet dat <br>+ hang";
                return View(sp);
            }
            var vietsach = (db.SANPHAMs.Where(vs => vs.MASP == id)).SingleOrDefault();
            if (vietsach != null)
            {
                db.SANPHAMs.Remove(vietsach);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADM = new SelectList(db.DANHMUCs, "MADM", "TENDM", sANPHAM.MADM);
            ViewBag.MALSP = new SelectList(db.LOAISANPHAMs, "MALSP", "TENLSP", sANPHAM.MALSP);
            return View(sANPHAM);
        }

        // POST: Admin/NHAP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MASP,TENSP,NOIDUNGTT,GIACA,HINHMINHHOA,MALSP,MADM")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADM = new SelectList(db.DANHMUCs, "MADM", "TENDM", sANPHAM.MADM);
            ViewBag.MALSP = new SelectList(db.LOAISANPHAMs, "MALSP", "TENLSP", sANPHAM.MALSP);
            return View(sANPHAM);
        }


    }
}