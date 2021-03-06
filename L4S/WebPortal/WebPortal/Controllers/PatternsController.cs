﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPortal.Common;
using WebPortal.DataContexts;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    [OutputCache(Duration = 0)]
    [Helper.CheckSessionOutAttribute]
    [Authorize] //!!! important only Authorize users can call this controller
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
            List<CATServicePatterns> cAtServicePatterns = db.CATServicePatterns.Where(p=>p.FKServiceID==id && p.TCActive!=99).ToList() ;
            ViewBag.FKServiceID = id.Value; //inportat Id for map FK to new if create 
            return View(cAtServicePatterns);
        }

        // GET: Patterns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATServicePatterns cAtServicePatterns = db.CATServicePatterns.Find(id);
            if (cAtServicePatterns == null)
            {
                return HttpNotFound();
            }
            return View(cAtServicePatterns);
        }

        // GET: Patterns/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATServicePatterns model = new CATServicePatterns {FKServiceID = id.Value};
            return PartialView("_Create", model);
        }

        // POST: Patterns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CATServicePatterns cAtServicePatterns)
        {
            if (ModelState.IsValid)
            {
                cAtServicePatterns.TCActive = 0;
                cAtServicePatterns.TCInsertTime = DateTime.Now;
                cAtServicePatterns.TCLastUpdate = DateTime.Now;
               
                db.CATServicePatterns.Add(cAtServicePatterns);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = cAtServicePatterns.FKServiceID });
            }

            return View(cAtServicePatterns);
        }

        // GET: Patterns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATServicePatterns cAtServicePatterns = db.CATServicePatterns.Find(id);
            if (cAtServicePatterns == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Edit",cAtServicePatterns);
        }

        // POST: Patterns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKServicePatternID,PatternLike,PatternRegExp,PatternDescription,FKServiceID,Entity,Explanation,DatSelectMethod,TCInsertTime,TCLastUpdate,TCActive")] CATServicePatterns cAtServicePatterns)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAtServicePatterns).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = cAtServicePatterns.FKServiceID });
            }
            return View(cAtServicePatterns);
        }

        // GET: Patterns/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CATServicePatterns cAtServicePatterns = db.CATServicePatterns.Find(id);
            if (cAtServicePatterns == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(cAtServicePatterns.PKServicePatternID, cAtServicePatterns.PatternDescription);
            return PartialView("_deleteModal", model);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CATServicePatterns cATServicePatterns = db.CATServicePatterns.Find(id);
            //if (cATServicePatterns == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cATServicePatterns);
        }

        // POST: Patterns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATServicePatterns cAtServicePatterns = db.CATServicePatterns.Find(id);
            cAtServicePatterns.TCActive = 99;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = cAtServicePatterns.FKServiceID });
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
