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
    public class StageInputFileInfoController : Controller
    {
        private log4serviceEntities _db = new log4serviceEntities();

        // GET: Stage_InputFileInfo
        public ActionResult Index()
        {
            return View(_db.Stage_InputFileInfo.ToList());
        }

        // GET: Stage_InputFileInfo/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stage_InputFileInfo stageInputFileInfo = _db.Stage_InputFileInfo.Find(id);
            if (stageInputFileInfo == null)
            {
                return HttpNotFound();
            }
            return View(stageInputFileInfo);
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
        public ActionResult Create([Bind(Include = "id,fileName,checksum,insertDateTime")] Stage_InputFileInfo stageInputFileInfo)
        {
            if (ModelState.IsValid)
            {
                _db.Stage_InputFileInfo.Add(stageInputFileInfo);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stageInputFileInfo);
        }

        // GET: Stage_InputFileInfo/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stage_InputFileInfo stageInputFileInfo = _db.Stage_InputFileInfo.Find(id);
            if (stageInputFileInfo == null)
            {
                return HttpNotFound();
            }
            return View(stageInputFileInfo);
        }

        // POST: Stage_InputFileInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fileName,checksum,insertDateTime")] Stage_InputFileInfo stageInputFileInfo)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(stageInputFileInfo).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stageInputFileInfo);
        }

        // GET: Stage_InputFileInfo/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stage_InputFileInfo stageInputFileInfo = _db.Stage_InputFileInfo.Find(id);
            if (stageInputFileInfo == null)
            {
                return HttpNotFound();
            }
            return View(stageInputFileInfo);
        }

        // POST: Stage_InputFileInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Stage_InputFileInfo stageInputFileInfo = _db.Stage_InputFileInfo.Find(id);
            _db.Stage_InputFileInfo.Remove(stageInputFileInfo);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
