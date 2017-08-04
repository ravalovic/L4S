using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPortal.DataContexts;
using PagedList;

namespace WebPortal.Controllers
{
    public class UnknownServicesController : Controller
    {
        private L4SDb db = new L4SDb();
        private const int pageSize = 30;
        // GET: UnknownServices
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            List<CATUnknownService> catUnknown = db.CATUnknownService.OrderByDescending(d => d.TCInsertTime).Take(999).ToList();
            return View(catUnknown.ToPagedList(pageNumber:pageNumber, pageSize: pageSize));
            
        }

        // GET: UnknownServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATUnknownService cATUnknownService = db.CATUnknownService.Find(id);
            if (cATUnknownService == null)
            {
                return HttpNotFound();
            }
            return View(cATUnknownService);
        }

        // GET: UnknownServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATUnknownService cATUnknownService = db.CATUnknownService.Find(id);
            if (cATUnknownService == null)
            {
                return HttpNotFound();
            }
            return View(cATUnknownService);
        }
        public ActionResult Search(int? page, string insertDateFrom, string insertDateTo)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            DateTime fromDate;
            DateTime toDate;
            DateTime.TryParse(insertDateFrom, out fromDate);
            if (!DateTime.TryParse(insertDateTo, out toDate))
            {
                toDate = DateTime.Now;
            }
            if (fromDate == toDate) toDate = toDate.AddDays(1);
            List<CATUnknownService> cATUnknownService = db.CATUnknownService.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).ToList();
            if (cATUnknownService.Count == 0)
            {
                cATUnknownService = db.CATUnknownService.ToList();
            }
            return View("Index", cATUnknownService.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

        }
        // POST: UnknownServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BatchID,RecordID,CustomerID,ServiceID,UserID,DateOfRequest,RequestedURL,RequestStatus,BytesSent,RequestTime,UserAgent,UserIPAddress,TCInsertTime,TCLastUpdate,TCActive")] CATUnknownService cATUnknownService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATUnknownService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATUnknownService);
        }

        // GET: UnknownServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATUnknownService cATUnknownService = db.CATUnknownService.Find(id);
            if (cATUnknownService == null)
            {
                return HttpNotFound();
            }
            return View(cATUnknownService);
        }

        // POST: UnknownServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATUnknownService cATUnknownService = db.CATUnknownService.Find(id);
            db.CATUnknownService.Remove(cATUnknownService);
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
