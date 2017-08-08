using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebPortal.DataContexts;
using PagedList;

namespace WebPortal.Controllers
{
   
    public class ProcessStatusController : Controller
    {
        private L4SDb db = new L4SDb();

        private const int PageSize = 30;

        private const int ToTake = 900;
        // GET: ProcessStatus
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber * PageSize >= ToTake)
            {
                toSkip = PageSize * (pageNumber - 1);
            }
            List<CATProcessStatus> cATProcessStatus = db.CATProcessStatus.OrderByDescending(d => d.TCInsertTime).ToList();
            return View(cATProcessStatus.ToPagedList(pageNumber: pageNumber, pageSize: PageSize));
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
                toDate=DateTime.Now;
            }
            if (fromDate == toDate) toDate = toDate.AddDays(1);
            List<CATProcessStatus> cATProcessStatus = db.CATProcessStatus.Where(p => p.TCInsertTime >= fromDate && p.TCInsertTime <= toDate).OrderByDescending(d => d.TCInsertTime).ToList();
            if (cATProcessStatus.Count == 0)
                {
                    cATProcessStatus = db.CATProcessStatus.ToList();
                }
              return View("Index", cATProcessStatus.ToPagedList(pageNumber: pageNumber, pageSize: PageSize));
        
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
