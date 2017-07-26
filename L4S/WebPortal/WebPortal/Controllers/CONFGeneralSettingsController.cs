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
    public class CONFGeneralSettingsController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: CONFGeneralSettings
        public ActionResult Index()
        {
            return View(db.CONFGeneralSettings.Where(p => p.TCActive != 99).ToList());
        }

        // GET: CONFGeneralSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONFGeneralSettings cONFGeneralSettings = db.CONFGeneralSettings.Find(id);
            if (cONFGeneralSettings == null)
            {
                return HttpNotFound();
            }
            return View(cONFGeneralSettings);
        }

        // GET: CONFGeneralSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CONFGeneralSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ParamName,ParamValue,Note,TCInsertTime,TCLastUpdate,TCActive")] CONFGeneralSettings cONFGeneralSettings)
        {
            if (ModelState.IsValid)
            {
                cONFGeneralSettings.TCActive = 0;
                cONFGeneralSettings.TCInsertTime = DateTime.Now;
                cONFGeneralSettings.TCLastUpdate = DateTime.Now;
                db.CONFGeneralSettings.Add(cONFGeneralSettings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cONFGeneralSettings);
        }

        // GET: CONFGeneralSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONFGeneralSettings cONFGeneralSettings = db.CONFGeneralSettings.Find(id);
            if (cONFGeneralSettings == null)
            {
                return HttpNotFound();
            }
            return View(cONFGeneralSettings);
        }

        // POST: CONFGeneralSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ParamName,ParamValue,Note,TCInsertTime,TCLastUpdate,TCActive")] CONFGeneralSettings cONFGeneralSettings)
        {
            if (ModelState.IsValid)
            {
                cONFGeneralSettings.TCActive = 0;
                cONFGeneralSettings.TCLastUpdate = DateTime.Now;
                db.Entry(cONFGeneralSettings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cONFGeneralSettings);
        }

        // GET: CONFGeneralSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONFGeneralSettings cONFGeneralSettings = db.CONFGeneralSettings.Find(id);
            if (cONFGeneralSettings == null)
            {
                return HttpNotFound();
            }
            return View(cONFGeneralSettings);
        }

        // POST: CONFGeneralSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONFGeneralSettings cONFGeneralSettings = db.CONFGeneralSettings.Find(id);
            //db.CONFGeneralSettings.Remove(cONFGeneralSettings);
            if (cONFGeneralSettings != null)
            {
                cONFGeneralSettings.TCActive = 99;
                cONFGeneralSettings.TCLastUpdate = DateTime.Now;
            }
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
