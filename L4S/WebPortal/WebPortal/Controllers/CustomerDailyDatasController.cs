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

namespace WebPortal.Views
{
    public class CustomerDailyDatasController : Controller
    {
        private L4SDb db = new L4SDb();
        private const int pageSize = 30;

        // GET: CATCustomerDailyDatas
        public ActionResult CustomerDaily(int? page)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            List<CATCustomerDailyData> cAtDailyList = db.CATCustomerDailyData.OrderByDescending(d => d.DateOfRequest).ToList();
            return View(cAtDailyList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));
           
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
            List<CATCustomerDailyData> cAtDailyList = db.CATCustomerDailyData.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).ToList();
            if (cAtDailyList.Count == 0)
            {
                cAtDailyList = db.CATCustomerDailyData.ToList();
            }
            return View("CustomerDaily", cAtDailyList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

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
