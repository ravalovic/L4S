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
    public class InvoiceByMonthController : Controller
    {
        private L4SDb db = new L4SDb();

        // GET: InvoiceByMonth
        public ActionResult Index()
        {
            return View(db.view_InvoiceByMonth.ToList());
        }

        // GET: InvoiceByMonth/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_InvoiceByMonth view_InvoiceByMonth = db.view_InvoiceByMonth.Find(id);
            if (view_InvoiceByMonth == null)
            {
                return HttpNotFound();
            }
            return View(view_InvoiceByMonth);
        }

        // GET: InvoiceByMonth/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvoiceByMonth/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateOfRequest,CustomerID,ServiceID,NumberOfRequest,ReceivedBytes,RequestedTime,ServiceCode,ServiceName,BasicPriceWithoutVAT,VAT,BasicPriceWithVAT")] view_InvoiceByMonth view_InvoiceByMonth)
        {
            if (ModelState.IsValid)
            {
                db.view_InvoiceByMonth.Add(view_InvoiceByMonth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_InvoiceByMonth);
        }

        // GET: InvoiceByMonth/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_InvoiceByMonth view_InvoiceByMonth = db.view_InvoiceByMonth.Find(id);
            if (view_InvoiceByMonth == null)
            {
                return HttpNotFound();
            }
            return View(view_InvoiceByMonth);
        }

        // POST: InvoiceByMonth/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateOfRequest,CustomerID,ServiceID,NumberOfRequest,ReceivedBytes,RequestedTime,ServiceCode,ServiceName,BasicPriceWithoutVAT,VAT,BasicPriceWithVAT")] view_InvoiceByMonth view_InvoiceByMonth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view_InvoiceByMonth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view_InvoiceByMonth);
        }

        // GET: InvoiceByMonth/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            view_InvoiceByMonth view_InvoiceByMonth = db.view_InvoiceByMonth.Find(id);
            if (view_InvoiceByMonth == null)
            {
                return HttpNotFound();
            }
            return View(view_InvoiceByMonth);
        }

        // POST: InvoiceByMonth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            view_InvoiceByMonth view_InvoiceByMonth = db.view_InvoiceByMonth.Find(id);
            db.view_InvoiceByMonth.Remove(view_InvoiceByMonth);
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
