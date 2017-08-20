﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebPortal.DataContexts;
using PagedList;
using Microsoft.Ajax.Utilities;
using WebPortal.Common;

namespace WebPortal.Controllers
{
    public class InvoiceByDayController : Controller
    {
        private readonly L4SDb _db = new L4SDb();
        private List<view_InvoiceByDay> _dataList;
        private List<view_InvoiceByDay> _model;
        private Pager _pager;

        // GET: InvoiceByDay
        public ActionResult Index(int? page, string insertDateFrom, string insertDateTo, string searchText, string currentFilter, string currentFrom, string currentTo, int? currentCustId, int? currentServId, DateTime? currentDate)
        {
            var dbAccess = _db.view_InvoiceByDay;

            if (currentCustId != 0 && currentServId != 0 && currentDate.HasValue)
            {
                ViewBag.CurrentCustId = currentCustId;
                ViewBag.CurrentServId = currentServId;
                ViewBag.CurrentReqDate = currentDate;
                var startDate = new DateTime(currentDate.Value.Year, currentDate.Value.Month, currentDate.Value.Day);
                var endDate = startDate.AddDays(1).AddTicks(-1);
                _model = dbAccess
                    .Where(p => p.DateOfRequest >= startDate && p.DateOfRequest <= endDate &&
                                p.CustomerID == currentCustId && p.ServiceID == currentServId)
                    .OrderBy(d => d.DateOfRequest).ToList();
                //return View(_dataList.ToPagedList(pageNumber: pager.CurrentPage, pageSize: pager.PageSize));
            }
            else
            {
                if (searchText.IsNullOrWhiteSpace()) { searchText = currentFilter; }
                if (insertDateFrom.IsNullOrWhiteSpace()) { insertDateFrom = currentFrom; }
                if (insertDateTo.IsNullOrWhiteSpace()) { insertDateTo = currentTo; }
                // set actual filter to VieBag
                ViewBag.CurrentFilter = searchText;
                ViewBag.CurrentFrom = insertDateFrom;
                ViewBag.CurrentTo = insertDateTo;
                if (searchText.IsNullOrWhiteSpace() && insertDateFrom.IsNullOrWhiteSpace() &&
                    insertDateTo.IsNullOrWhiteSpace())
                {
                    _model = dbAccess.OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ToList();
                    if (_model.FirstOrDefault() != null)
                    {
                        var actualdate = _model.FirstOrDefault().DateOfRequest;

                        insertDateFrom = actualdate.Date.AddDays(1-actualdate.Day).ToString("dd.MM.yyyy");
                        insertDateTo = actualdate.Date.AddDays(1 - actualdate.Day).AddMonths(1).AddTicks(-1).ToString("dd.MM.yyyy");
                    }
                    else
                    {
                        insertDateFrom = DateTime.Now.AddDays(1-DateTime.Now.Day).ToString("dd.MM.yyyy");
                        insertDateTo = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddTicks(-1).ToString("dd.MM.yyyy");
                        
                    }
                    ViewBag.CurrentFrom = insertDateFrom;
                    ViewBag.CurrentTo = insertDateTo;
                }
                bool datCondition = false;
                bool textCondition = false;

                int.TryParse(searchText, out int searchId);
                if (!insertDateFrom.IsNullOrWhiteSpace() || !insertDateTo.IsNullOrWhiteSpace()) datCondition = true;
                if (searchText != null) textCondition = true;

                DateTime.TryParse(insertDateFrom, out DateTime fromDate);
                if (!DateTime.TryParse(insertDateTo, out DateTime toDate))
                {
                    toDate = DateTime.Now;
                }
                toDate = toDate.AddDays(1).AddTicks(-1);

                if (datCondition && !textCondition)
                {
                    _model = dbAccess.Where(p => p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate)
                        .OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ThenBy(p => p.ServiceID).ToList();
                }
                if (textCondition && !datCondition)
                {
                    _model = dbAccess
                            .Where(p => p.CustomerID == searchId || p.ServiceID == searchId|| 
                                        p.CustomerIdentification.Contains(searchText) ||
                                        p.CustomerName.Contains(searchText) || p.ServiceCode.Contains(searchText))
                        .OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ThenBy(p => p.ServiceID).ToList();

                }
                if (textCondition && datCondition)
                {
                    
                        _model = dbAccess
                            .Where(p => (p.DateOfRequest >= fromDate && p.DateOfRequest <= toDate) &&
                                        (p.CustomerID == searchId || p.ServiceID == searchId||
                                         p.CustomerIdentification.Contains(searchText) ||
                                         p.CustomerName.Contains(searchText) || p.ServiceCode.Contains(searchText)))
                            .OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.CustomerID).ThenBy(p => p.ServiceID).ToList();

                }
            }
            if (_model == null || _model.Count == 0)
            {
                _model = dbAccess.OrderByDescending(d => d.DateOfRequest)
                    .ThenBy(p => p.CustomerID).ThenBy(p => p.ServiceID).ToList();
            }

            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<view_InvoiceByDay>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            //ViewBag.PageList = pageList;
            //return View(model.ToPagedList(pageNumber: _pager.CurrentPage, pageSize: _pager.PageSize));
            return View("Index", pageList);
        }
        public ActionResult Details(int? page, int custId, int servId, DateTime reqDate)
        {
            var dbAccess = _db.view_InvoiceByDay;
            var startDate = new DateTime(reqDate.Year, reqDate.Month, reqDate.Day);
            var endDate = startDate.AddMonths(1).AddTicks(-1);
            ViewBag.CurrentCustId = custId;
            ViewBag.CurrentServId = servId;
            ViewBag.CurrentReqDate = reqDate;
            _model = dbAccess
                .Where(p => p.DateOfRequest >= startDate.Date && p.DateOfRequest <= endDate && p.CustomerID == custId && p.ServiceID == servId)
                .OrderByDescending(d => d.DateOfRequest).ThenBy(p => p.ServiceID).ToList();
            if (_model == null || _model.Count == 0)
            {
                _model = dbAccess.OrderByDescending(p => p.DateOfRequest).ToList();
            }
            _pager = new Pager(_model.Count(), page);
            _dataList = _model.Skip(_pager.ToSkip).Take(_pager.ToTake).ToList();
            var pageList = new StaticPagedList<view_InvoiceByDay>(_dataList, _pager.CurrentPage, _pager.PageSize, _pager.TotalItems);
            //return View("CustomerDaily", _dataList.ToPagedList(pageNumber: _pager.CurrentPage, pageSize: _pager.PageSize));
            return View("Index", pageList);
        }

      


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
