using System;
using Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebPortal.DataContexts;

namespace WebPortal.Models
{
    //public class UnknownServicesViewModel
    //{
    //    public CATCustomerData Customers { get; set; }
    //    public List<UnknownServiceViewModel> UnknownServices { get; set; }

    //    public UnknownServicesViewModel(CATCustomerData customer)
    //    {
    //        this.Customers = customer;
    //        getAll();
    //    }
    //    private bool getAll()
    //    {
    //        L4SDb db = new L4SDb();
    //        UnknownServices = new List<UnknownServiceViewModel>();
    //        var services = db.CATUnknownService;

    //        foreach (var service in services)
    //        {

    //            this.UnknownServices.Add(new UnknownServiceViewModel
    //            {
                    
    //                ID = service.ID,
    //                BatchID = service.BatchID,
    //                RecordID = service.RecordID,
    //                CustomerID = service.CustomerID,
    //                CustomerName = db.CATCustomerData.Where(a => a.PKCustomerDataID == service.CustomerID && a.CompanyName != null).Select(a => a.CompanyName) != null ? db.CATCustomerData.Where(c => (c.PKCustomerDataID == service.CustomerID && c.CompanyName != null)).Select(c => c.CompanyName).ToString() : db.CATCustomerData.Where(c => (c.PKCustomerDataID == service.CustomerID && c.CompanyName == null)).Select(c => c.IndividualFirstName+" "+ c.IndividualLastName).ToString(),
    //                ServiceID = service.ServiceID,
    //                DateOfRequest = service.DateOfRequest,
    //                RequestedURL = service.RequestedURL,
    //                RequestStatus = service.RequestStatus,
    //                BytesSent = service.BytesSent,
    //                UserIPAddress = service.UserIPAddress
    //            });
    //        }
    //        return true;
    //    }
    //}

    public class UnknownServicesViewModel
    {
        public int ID { get; set; }

        public int BatchID { get; set; }

        public int RecordID { get; set; }

        public int? CustomerID { get; set; }

        public string CustomerName { get; set; }

        public int? ServiceID { get; set; }

        public string UserID { get; set; }

        public DateTime? DateOfRequest { get; set; }
       
        public string RequestedURL { get; set; }

        public string RequestStatus { get; set; }

        public string BytesSent { get; set; }
       
        public string RequestTime { get; set; }

       // public string UserAgent { get; set; }
        public string UserIPAddress { get; set; }


    }
}