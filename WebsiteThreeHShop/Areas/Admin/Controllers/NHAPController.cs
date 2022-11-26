using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteThreeHShop.Models;

namespace WebsiteThreeHShop.Areas.Admin.Controllers
{
    public class NHAPController : Controller
    {
        private QUANLYBANHANGEn db = new QUANLYBANHANGEn();

        // GET: Admin/NHAP
        public ActionResult Index()
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.DANHMUC).Include(s => s.LOAISANPHAM);
            return View(sANPHAMs.ToList());
        }

        // GET: Admin/NHAP/Details/5
        public ActionResult Details(int? id)
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
            return View(sANPHAM);
        }

        // GET: Admin/NHAP/Create
        public ActionResult Create()
        {
            ViewBag.MADM = new SelectList(db.DANHMUCs, "MADM", "TENDM");
            ViewBag.MALSP = new SelectList(db.LOAISANPHAMs, "MALSP", "TENLSP");
            return View();
        }

        // POST: Admin/NHAP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MASP,TENSP,NOIDUNGTT,GIACA,HINHMINHHOA,MALSP,MADM")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADM = new SelectList(db.DANHMUCs, "MADM", "TENDM", sANPHAM.MADM);
            ViewBag.MALSP = new SelectList(db.LOAISANPHAMs, "MALSP", "TENLSP", sANPHAM.MALSP);
            return View(sANPHAM);
        }

        // GET: Admin/NHAP/Edit/5
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

        // GET: Admin/NHAP/Delete/5
        public ActionResult Delete(int? id)
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
            return View(sANPHAM);
        }

        // POST: Admin/NHAP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
