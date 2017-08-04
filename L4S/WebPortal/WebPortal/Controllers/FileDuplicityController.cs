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
using PagedList;

namespace WebPortal.Controllers
{
    public class FileDuplicityController : Controller
    {
        private L4SDb db = new L4SDb();
        private const int pageSize = 30;

        // GET: FileDuplicity
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            List<STInputFileDuplicity> sTInputFileDuplicity = db.STInputFileDuplicity.OrderByDescending(f => f.InsertDateTime).ToList();
            return View(sTInputFileDuplicity.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));
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
            List<STInputFileDuplicity> sTInputFileDuplicity = db.STInputFileDuplicity.Where(p => p.InsertDateTime >= fromDate && p.InsertDateTime <= toDate).OrderByDescending(f => f.InsertDateTime).ToList();
            if (sTInputFileDuplicity.Count == 0)
            {
                sTInputFileDuplicity = db.STInputFileDuplicity.OrderByDescending(f => f.InsertDateTime).ToList();
            }
            return View("Index", sTInputFileDuplicity.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

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

            DeleteModel model = new DeleteModel(sTInputFileDuplicity.ID, sTInputFileDuplicity.OriFileName);
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
