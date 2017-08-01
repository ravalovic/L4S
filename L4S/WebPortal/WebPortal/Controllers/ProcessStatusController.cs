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
using PagedList;
using WebPortal.Models;

namespace WebPortal.Controllers
{
   
    public class ProcessStatusController : Controller
    {
        private L4SDb db = new L4SDb();

        private const int pageSize = 30;
        // GET: ProcessStatus
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            List<CATProcessStatus> cATProcessStatus = db.CATProcessStatus.OrderByDescending(d => d.TCInsertTime).ToList();
            return View(cATProcessStatus.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));
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
                toDate=DateTime.Now;
            }
            List<CATProcessStatus> cATProcessStatus = db.CATProcessStatus.Where(p => p.TCInsertTime >= fromDate && p.TCInsertTime <= toDate).OrderByDescending(d => d.TCInsertTime).ToList();
            if (cATProcessStatus.Count == 0)
                {
                    cATProcessStatus = db.CATProcessStatus.ToList();
                }
              return View("Index", cATProcessStatus.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));
        
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
