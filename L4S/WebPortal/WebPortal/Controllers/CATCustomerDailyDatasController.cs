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

namespace WebPortal.Views
{
    public class CATCustomerDailyDatasController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: CATCustomerDailyDatas
        public ActionResult CustomerDaily()
        {
            return View(db.CATCustomerDailyData.ToList());
        }

        // GET: CATCustomerDailyDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerDailyData cATCustomerDailyData = db.CATCustomerDailyData.Find(id);
            if (cATCustomerDailyData == null)
            {
                return HttpNotFound();
            }
            return View(cATCustomerDailyData);
        }

        // GET: CATCustomerDailyDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CATCustomerDailyDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateOfRequest,CustomerID,ServiceID,NumberOfRequest,ReceivedBytes,RequestedTime,TCInsertTime,TCLastUpdate,TCActive")] CATCustomerDailyData cATCustomerDailyData)
        {
            if (ModelState.IsValid)
            {
                db.CATCustomerDailyData.Add(cATCustomerDailyData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cATCustomerDailyData);
        }

        // GET: CATCustomerDailyDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerDailyData cATCustomerDailyData = db.CATCustomerDailyData.Find(id);
            if (cATCustomerDailyData == null)
            {
                return HttpNotFound();
            }
            return View(cATCustomerDailyData);
        }

        // POST: CATCustomerDailyDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateOfRequest,CustomerID,ServiceID,NumberOfRequest,ReceivedBytes,RequestedTime,TCInsertTime,TCLastUpdate,TCActive")] CATCustomerDailyData cATCustomerDailyData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATCustomerDailyData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATCustomerDailyData);
        }

        // GET: CATCustomerDailyDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerDailyData cATCustomerDailyData = db.CATCustomerDailyData.Find(id);
            if (cATCustomerDailyData == null)
            {
                return HttpNotFound();
            }
            return View(cATCustomerDailyData);
        }

        // POST: CATCustomerDailyDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATCustomerDailyData cATCustomerDailyData = db.CATCustomerDailyData.Find(id);
            db.CATCustomerDailyData.Remove(cATCustomerDailyData);
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
