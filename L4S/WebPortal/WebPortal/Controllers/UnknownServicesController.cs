using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebPortal.DataContexts;
using PagedList;

namespace WebPortal.Controllers
{
    public class UnknownServicesController : Controller
    {
        private L4SDb db = new L4SDb();
        private const int PageSize = 30;

        private const int ToTake = 900;
        // GET: UnknownServices
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber * PageSize >= ToTake)
            {
                toSkip = PageSize * (pageNumber - 1);
            }
            List<CATUnknownService> catUnknown = db.CATUnknownService.OrderByDescending(d => d.TCInsertTime).Take(999).ToList();
            return View(catUnknown.ToPagedList(pageNumber:pageNumber, pageSize: PageSize));
            
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
            List<CATUnknownService> cATUnknownService = db.CATUnknownService.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).ToList();
            if (cATUnknownService.Count == 0)
            {
                cATUnknownService = db.CATUnknownService.ToList();
            }
            return View("Index", cATUnknownService.ToPagedList(pageNumber: pageNumber, pageSize: PageSize));

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
