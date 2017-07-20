﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPortal;
using WebPortal.DataContexts;

namespace WebPortal.Controllers
{
    public class CATOwnerDatasController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: CATOwnerDatas
        public ActionResult Index()
        {
            return View(db.CATOwnerData.ToList());
        }

        // GET: CATOwnerDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATOwnerData cATOwnerData = db.CATOwnerData.Find(id);
            if (cATOwnerData == null)
            {
                return HttpNotFound();
            }
            return View(cATOwnerData);
        }

        // GET: CATOwnerDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CATOwnerDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            //[Bind(Include = "ID,OwnerCompanyName,OwnwerCompanyType,OwnerCompanyType,OwnerCompanyID,OwnerCompanyTAXID,OwnerCompanyVATID,OwnerBankAccountIban,OwnerAddressStreet,OwnerAddressBuildingNumber,OwnerAddressCity,OwnerAddressZipCode,OwnerAddressCountry,OwnerResponsibleFirstName,OwnerResponsiblelastName,OwnerContactEmail,OwnerContactMobile,OwnerContactPhone,OwnerContactWeb,TCInsertTime,TCLastUpdate,TCActive")]
        CATOwnerData cATOwnerData)
        {
            if (ModelState.IsValid)
            {
                db.CATOwnerData.Add(cATOwnerData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cATOwnerData);
        }

        // GET: CATOwnerDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATOwnerData cATOwnerData = db.CATOwnerData.Find(id);
            if (cATOwnerData == null)
            {
                return HttpNotFound();
            }
            return View(cATOwnerData);
        }

        // POST: CATOwnerDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OwnerCompanyName,OwnwerCompanyType,OwnerCompanyType,OwnerCompanyID,OwnerCompanyTAXID,OwnerCompanyVATID,OwnerBankAccountIban,OwnerAddressStreet,OwnerAddressBuildingNumber,OwnerAddressCity,OwnerAddressZipCode,OwnerAddressCountry,OwnerResponsibleFirstName,OwnerResponsiblelastName,OwnerContactEmail,OwnerContactMobile,OwnerContactPhone,OwnerContactWeb,TCInsertTime,TCLastUpdate,TCActive")] CATOwnerData cATOwnerData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATOwnerData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATOwnerData);
        }

        // GET: CATOwnerDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATOwnerData cATOwnerData = db.CATOwnerData.Find(id);
            if (cATOwnerData == null)
            {
                return HttpNotFound();
            }
            return View(cATOwnerData);
        }

        // POST: CATOwnerDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATOwnerData cATOwnerData = db.CATOwnerData.Find(id);
            db.CATOwnerData.Remove(cATOwnerData);
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
