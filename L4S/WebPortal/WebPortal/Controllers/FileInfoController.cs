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
    public class FileInfoController : Controller
    {
        private L4SDb db = new L4SDb();
        private const int pageSize = 30;

        // GET: FileInfo
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            List<STInputFileInfo> stFile = db.STInputFileInfo.OrderByDescending(f => f.LoaderBatchID).ToList();
            return View(stFile.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));
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
            List<STInputFileInfo> sTInputFileInfo = db.STInputFileInfo.Where(p => p.InsertDateTime >= fromDate && p.InsertDateTime <= toDate).OrderByDescending(f => f.LoaderBatchID).ToList();
            if (sTInputFileInfo.Count == 0)
            {
                sTInputFileInfo = db.STInputFileInfo.OrderByDescending(f => f.LoaderBatchID).ToList();
            }
            return View("Index", sTInputFileInfo.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

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

            DeleteModel model = new DeleteModel(sTInputFileInfo.Id, Resources.Labels.FileInfo_PageTitle);
            return PartialView("_deleteModal", model);

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //STInputFileInfo sTInputFileInfo = db.STInputFileInfo.Find(id);
            //if (sTInputFileInfo == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(sTInputFileInfo);
        }

        // POST: FileInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STInputFileInfo sTInputFileInfo = db.STInputFileInfo.Find(id);
            sTInputFileInfo.TCActive = 99;
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
