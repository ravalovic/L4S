using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPortal;
using WebPortal.DataContexts;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class FileDuplicityController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: FileDuplicity
        public ActionResult Index()
        {
            return View(db.STInputFileDuplicity.ToList());
        }

        // GET: FileDuplicity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STInputFileDuplicity sTInputFileDuplicity = db.STInputFileDuplicity.Find(id);
            if (sTInputFileDuplicity == null)
            {
                return HttpNotFound();
            }
            return View(sTInputFileDuplicity);
        }

        // GET: FileDuplicity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileDuplicity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OriginalId,FileName,LinesInFile,Checksum,LoadDateTime,InsertDateTime,OriFileName,OriginalFileChecksum,LoaderBatchID")] STInputFileDuplicity sTInputFileDuplicity)
        {
            if (ModelState.IsValid)
            {
                db.STInputFileDuplicity.Add(sTInputFileDuplicity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sTInputFileDuplicity);
        }

        // GET: FileDuplicity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STInputFileDuplicity sTInputFileDuplicity = db.STInputFileDuplicity.Find(id);
            if (sTInputFileDuplicity == null)
            {
                return HttpNotFound();
            }
            return View(sTInputFileDuplicity);
        }

        // POST: FileDuplicity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OriginalId,FileName,LinesInFile,Checksum,LoadDateTime,InsertDateTime,OriFileName,OriginalFileChecksum,LoaderBatchID")] STInputFileDuplicity sTInputFileDuplicity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTInputFileDuplicity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sTInputFileDuplicity);
        }

        // GET: FileDuplicity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            STInputFileDuplicity sTInputFileDuplicity = db.STInputFileDuplicity.Find(id);
            if (sTInputFileDuplicity == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(sTInputFileDuplicity.ID, Resources.Labels.FileDuplicity_PageTitle);
            return PartialView("_deleteModal", model);

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //STInputFileDuplicity sTInputFileDuplicity = db.STInputFileDuplicity.Find(id);
            //if (sTInputFileDuplicity == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(sTInputFileDuplicity);
        }

        // POST: FileDuplicity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STInputFileDuplicity sTInputFileDuplicity = db.STInputFileDuplicity.Find(id);
            sTInputFileDuplicity.TCActive = 99;
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
