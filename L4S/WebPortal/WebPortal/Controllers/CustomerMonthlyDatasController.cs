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

namespace WebPortal.Models
{
    public class CustomerMonthlyDatasController : Controller
    {
        private L4SDb db = new L4SDb();
        private const int pageSize = 30;

        // GET: CATCustomerMonthlyDatas
        public ActionResult CustomerMonthly(int? page)
        {
            int pageNumber = (page ?? 1);
            int toSkip = 0;
            if (pageNumber != 1)
            {
                toSkip = pageSize * (pageNumber - 1);
            }
            List<CATCustomerMonthlyData> cAtMonthlyList = db.CATCustomerMonthlyData.OrderByDescending(d => d.DateOfRequest).ToList();
            return View(cAtMonthlyList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

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
            List<CATCustomerMonthlyData> cAtMonthlyList = db.CATCustomerMonthlyData.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate).OrderByDescending(d => d.DateOfRequest).ToList();
            if (cAtMonthlyList.Count == 0)
            {
                cAtMonthlyList = db.CATCustomerMonthlyData.ToList();
            }
            return View("CustomerMonthly", cAtMonthlyList.ToPagedList(pageNumber: pageNumber, pageSize: pageSize));

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
