using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPortal.DataContexts;
using WebPortal.Models;
using PagedList;

namespace WebPortal.Controllers
{
    public class FileDuplicityController : Controller
    {
        private L4SDb db = new L4SDb();
        private const int PageSize = 30;
        private const int ToTake = 900;

        // GET: FileDuplicity
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber * PageSize >= ToTake)
            {
                toSkip = PageSize * (pageNumber - 1);
            }
            List<STInputFileDuplicity> sTInputFileDuplicity = db.STInputFileDuplicity.OrderByDescending(f => f.InsertDateTime).ToList();
            return View(sTInputFileDuplicity.ToPagedList(pageNumber: pageNumber, pageSize: PageSize));
        }
        public ActionResult Search(int? page, string insertDateFrom, string insertDateTo)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber * PageSize >= ToTake)
            {
                toSkip = PageSize * (pageNumber - 1);
            }
            
            DateTime.TryParse(insertDateFrom, out DateTime fromDate);
            if (!DateTime.TryParse(insertDateTo, out DateTime toDate))
            {
                toDate = DateTime.Now;
            }
            if (fromDate == toDate) toDate = toDate.AddDays(1);
            List<STInputFileDuplicity> sTInputFileDuplicity = db.STInputFileDuplicity.Where(p => p.InsertDateTime >= fromDate && p.InsertDateTime <= toDate).OrderByDescending(f => f.InsertDateTime).ToList();
            if (sTInputFileDuplicity.Count == 0)
            {
                sTInputFileDuplicity = db.STInputFileDuplicity.OrderByDescending(f => f.InsertDateTime).ToList();
            }
            return View("Index", sTInputFileDuplicity.ToPagedList(pageNumber: pageNumber, pageSize: PageSize));

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
