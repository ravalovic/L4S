using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using WebPortal.DataContexts;

namespace WebPortal.Common
{
    public class Helper
    {
        public static void SetUpFilterValues(ref string search, ref string fDate, ref string tDate, string currFilter,
            string currFrom, string currTo, out int searchId, out DateTime fromDate, out DateTime toDate, int? pageNum)
        {
            search = search?.Trim();
            fDate = fDate?.Trim();
            tDate = tDate?.Trim();
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
                    toDate = toDate.AddMonths(1).AddTicks(-1);
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
                    toDate = toDate.AddDays(1).AddTicks(-1);
                }
            }
            int.TryParse(search, out searchId);
        }

    }
}