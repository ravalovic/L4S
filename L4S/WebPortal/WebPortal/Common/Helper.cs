using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace WebPortal.Common
{
    public class Helper
    {
        [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
        public class CheckSessionOutAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                System.Web.HttpContext context = HttpContext.Current;
                if (context.Session != null)
                {
                    if (context.Session.IsNewSession)
                    {
                        string sessionCookie = context.Request.Headers["Cookie"];

                        if ((sessionCookie != null) && (sessionCookie.IndexOf("ASP.NET_SessionId", StringComparison.Ordinal) >= 0))
                        {
                            FormsAuthentication.SignOut();
                            string redirectTo;
                            if (!string.IsNullOrEmpty(context.Request.RawUrl))
                            {
                                redirectTo = string.Format("~/Account/Login?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
                                filterContext.Result = new RedirectResult(redirectTo);
                                return;
                            }

                        }
                    }
                }

                base.OnActionExecuting(filterContext);
            }
        }

        public class Statistics
        {
            public int CustomerCount { get; set; }
            public int ServiceCount { get; set; }
            public long RequestCount { get; set; }
            public decimal ReceivedBytes { get; set; }
            public decimal ReceivedBytesInMeasureUnit { get; set; }
            public decimal SessionDuration {get;set;}
            public string MetricUnit { get; set; }
            
        }

        public static void SetUpFilterValues(ref string search, ref string fDate, ref string tDate, string currFilter,
            string currFrom, string currTo, out int searchId, out DateTime fromDate, out DateTime toDate, int? pageNum)
        {
            search = search?.Trim();
            fDate = fDate?.Trim();
            tDate = tDate?.Trim();
            DateTime.TryParse(fDate, out fromDate);
            DateTime.TryParse(tDate, out toDate);
            if ((fromDate > toDate) && !fDate.IsNullOrWhiteSpace() && !tDate.IsNullOrWhiteSpace())
            {
                fDate = String.Empty;
                tDate = String.Empty;
            }

            if (pageNum != null)
            {
                if (search.IsNullOrWhiteSpace())
                {
                    search = currFilter?.Trim();
                }
                if (fDate.IsNullOrWhiteSpace())
                {
                    fDate = currFrom?.Trim();
                }
                if (tDate.IsNullOrWhiteSpace())
                {
                    tDate = currTo?.Trim();
                }
            }

            if (fDate != null && Regex.Match(fDate, @"\d{2}\.\d{4}").Success)
            {
                DateTime.TryParse(fDate, out fromDate);
                if (!DateTime.TryParse(tDate, out toDate))
                {
                    toDate = DateTime.Now;
                }
                else
                {
                    if (fromDate > toDate) { 
                    toDate = toDate.AddMonths(1).AddTicks(-1);
                    }
                }
            }
            else
            {
                DateTime.TryParse(fDate, out fromDate);
                if (!DateTime.TryParse(tDate, out toDate))
                {
                    toDate = DateTime.Now;
                }
                else
                {
                    if (fromDate > toDate)
                    {
                        toDate = toDate.AddDays(1).AddTicks(-1);
                    }
                }
            }
            int.TryParse(search, out searchId);
        }

    }
}