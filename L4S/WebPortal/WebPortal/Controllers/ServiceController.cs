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
    public class ServiceController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: CATServiceParameters
        public ActionResult Index()
        {
            return View(db.CATServiceParameters.ToList());
        }

        // GET: CATServiceParameters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATServiceParameters cATServiceParameters = db.CATServiceParameters.Find(id);
            if (cATServiceParameters == null)
            {
                return HttpNotFound();
            }
            return View(cATServiceParameters);
        }

        // GET: CATServiceParameters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CATServiceParameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKServiceID,ServiceCode,ServiceDescription,ServiceBasicPrice,TCInsertTime,TCLastUpdate,TCActive")] CATServiceParameters cATServiceParameters)
        {
            if (ModelState.IsValid)
            {
                db.CATServiceParameters.Add(cATServiceParameters);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cATServiceParameters);
        }

        // GET: CATServiceParameters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATServiceParameters cATServiceParameters = db.CATServiceParameters.Find(id);
            if (cATServiceParameters == null)
            {
                return HttpNotFound();
            }
            return View(cATServiceParameters);
        }

        // POST: CATServiceParameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKServiceID,ServiceCode,ServiceDescription,ServiceBasicPrice,TCInsertTime,TCLastUpdate,TCActive")] CATServiceParameters cATServiceParameters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATServiceParameters).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATServiceParameters);
        }

        // GET: CATServiceParameters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATServiceParameters cATServiceParameters = db.CATServiceParameters.Find(id);
            if (cATServiceParameters == null)
            {
                return HttpNotFound();
            }
            return View(cATServiceParameters);
        }

        // POST: CATServiceParameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATServiceParameters cATServiceParameters = db.CATServiceParameters.Find(id);
            db.CATServiceParameters.Remove(cATServiceParameters);
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
