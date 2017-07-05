﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPortal.DataContexts;

namespace WebPortal.Controllers
{
    public class PatternsController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: Patterns
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return View(db.CATServicePatterns.ToList());
            }
            List<CATServicePatterns> cATServicePatterns = db.CATServicePatterns.Where(p=>p.FKServiceID==id).ToList() ;
            if (cATServicePatterns == null)
            {
                return HttpNotFound();
            }
            return View(cATServicePatterns);
        }

        // GET: Patterns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATServicePatterns cATServicePatterns = db.CATServicePatterns.Find(id);
            if (cATServicePatterns == null)
            {
                return HttpNotFound();
            }
            return View(cATServicePatterns);
        }

        // GET: Patterns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patterns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKServicePatternID,PatternLike,PatternRegExp,PatternDescription,FKServiceID,Entity,Explanation,DatSelectMethod,TCInsertTime,TCLastUpdate,TCActive")] CATServicePatterns cATServicePatterns)
        {
            if (ModelState.IsValid)
            {
                db.CATServicePatterns.Add(cATServicePatterns);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cATServicePatterns);
        }

        // GET: Patterns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATServicePatterns cATServicePatterns = db.CATServicePatterns.Find(id);
            if (cATServicePatterns == null)
            {
                return HttpNotFound();
            }
            return View(cATServicePatterns);
        }

        // POST: Patterns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKServicePatternID,PatternLike,PatternRegExp,PatternDescription,FKServiceID,Entity,Explanation,DatSelectMethod,TCInsertTime,TCLastUpdate,TCActive")] CATServicePatterns cATServicePatterns)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATServicePatterns).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATServicePatterns);
        }

        // GET: Patterns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATServicePatterns cATServicePatterns = db.CATServicePatterns.Find(id);
            if (cATServicePatterns == null)
            {
                return HttpNotFound();
            }
            return View(cATServicePatterns);
        }

        // POST: Patterns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATServicePatterns cATServicePatterns = db.CATServicePatterns.Find(id);
            db.CATServicePatterns.Remove(cATServicePatterns);
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