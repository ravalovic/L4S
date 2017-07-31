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

namespace WebPortal.Models
{
    public class CATCustomerMonthlyDatasController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: CATCustomerMonthlyDatas
        public ActionResult CustomerMonthly()
        {
            return View(db.CATCustomerMonthlyData.ToList());
        }

        // GET: CATCustomerMonthlyDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerMonthlyData cATCustomerMonthlyData = db.CATCustomerMonthlyData.Find(id);
            if (cATCustomerMonthlyData == null)
            {
                return HttpNotFound();
            }
            return View(cATCustomerMonthlyData);
        }

        // GET: CATCustomerMonthlyDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CATCustomerMonthlyDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateOfRequest,CustomerID,ServiceID,NumberOfRequest,ReceivedBytes,RequestedTime,TCInsertTime,TCLastUpdate,TCActive")] CATCustomerMonthlyData cATCustomerMonthlyData)
        {
            if (ModelState.IsValid)
            {
                db.CATCustomerMonthlyData.Add(cATCustomerMonthlyData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cATCustomerMonthlyData);
        }

        // GET: CATCustomerMonthlyDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerMonthlyData cATCustomerMonthlyData = db.CATCustomerMonthlyData.Find(id);
            if (cATCustomerMonthlyData == null)
            {
                return HttpNotFound();
            }
            return View(cATCustomerMonthlyData);
        }

        // POST: CATCustomerMonthlyDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateOfRequest,CustomerID,ServiceID,NumberOfRequest,ReceivedBytes,RequestedTime,TCInsertTime,TCLastUpdate,TCActive")] CATCustomerMonthlyData cATCustomerMonthlyData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATCustomerMonthlyData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATCustomerMonthlyData);
        }

        // GET: CATCustomerMonthlyDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATCustomerMonthlyData cATCustomerMonthlyData = db.CATCustomerMonthlyData.Find(id);
            if (cATCustomerMonthlyData == null)
            {
                return HttpNotFound();
            }
            return View(cATCustomerMonthlyData);
        }

        // POST: CATCustomerMonthlyDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATCustomerMonthlyData cATCustomerMonthlyData = db.CATCustomerMonthlyData.Find(id);
            db.CATCustomerMonthlyData.Remove(cATCustomerMonthlyData);
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
