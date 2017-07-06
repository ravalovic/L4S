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

namespace WebPortal.Controllers
{
    public class FileInfoController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: FileInfo
        public ActionResult Index()
        {
            return View(db.STInputFileInfo.ToList());
        }

        // GET: FileInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STInputFileInfo sTInputFileInfo = db.STInputFileInfo.Find(id);
            if (sTInputFileInfo == null)
            {
                return HttpNotFound();
            }
            return View(sTInputFileInfo);
        }

        // GET: FileInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FileName,Checksum,LinesInFile,InsertDateTime,LoaderBatchID,LoadedRecord,OriFileName,OriginalFileChecksum")] STInputFileInfo sTInputFileInfo)
        {
            if (ModelState.IsValid)
            {
                db.STInputFileInfo.Add(sTInputFileInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sTInputFileInfo);
        }

        // GET: FileInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STInputFileInfo sTInputFileInfo = db.STInputFileInfo.Find(id);
            if (sTInputFileInfo == null)
            {
                return HttpNotFound();
            }
            return View(sTInputFileInfo);
        }

        // POST: FileInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FileName,Checksum,LinesInFile,InsertDateTime,LoaderBatchID,LoadedRecord,OriFileName,OriginalFileChecksum")] STInputFileInfo sTInputFileInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTInputFileInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sTInputFileInfo);
        }

        // GET: FileInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STInputFileInfo sTInputFileInfo = db.STInputFileInfo.Find(id);
            if (sTInputFileInfo == null)
            {
                return HttpNotFound();
            }
            return View(sTInputFileInfo);
        }

        // POST: FileInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STInputFileInfo sTInputFileInfo = db.STInputFileInfo.Find(id);
            db.STInputFileInfo.Remove(sTInputFileInfo);
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
