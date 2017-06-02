using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPortal;

namespace WebPortal.Controllers
{
    public class Stage_InputFileInfoController : Controller
    {
        private log4serviceEntities db = new log4serviceEntities();

        // GET: Stage_InputFileInfo
        public ActionResult Index()
        {
            return View(db.Stage_InputFileInfo.ToList());
        }

        // GET: Stage_InputFileInfo/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stage_InputFileInfo stage_InputFileInfo = db.Stage_InputFileInfo.Find(id);
            if (stage_InputFileInfo == null)
            {
                return HttpNotFound();
            }
            return View(stage_InputFileInfo);
        }

        // GET: Stage_InputFileInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stage_InputFileInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fileName,checksum,insertDateTime")] Stage_InputFileInfo stage_InputFileInfo)
        {
            if (ModelState.IsValid)
            {
                db.Stage_InputFileInfo.Add(stage_InputFileInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stage_InputFileInfo);
        }

        // GET: Stage_InputFileInfo/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stage_InputFileInfo stage_InputFileInfo = db.Stage_InputFileInfo.Find(id);
            if (stage_InputFileInfo == null)
            {
                return HttpNotFound();
            }
            return View(stage_InputFileInfo);
        }

        // POST: Stage_InputFileInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fileName,checksum,insertDateTime")] Stage_InputFileInfo stage_InputFileInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stage_InputFileInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stage_InputFileInfo);
        }

        // GET: Stage_InputFileInfo/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stage_InputFileInfo stage_InputFileInfo = db.Stage_InputFileInfo.Find(id);
            if (stage_InputFileInfo == null)
            {
                return HttpNotFound();
            }
            return View(stage_InputFileInfo);
        }

        // POST: Stage_InputFileInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Stage_InputFileInfo stage_InputFileInfo = db.Stage_InputFileInfo.Find(id);
            db.Stage_InputFileInfo.Remove(stage_InputFileInfo);
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
