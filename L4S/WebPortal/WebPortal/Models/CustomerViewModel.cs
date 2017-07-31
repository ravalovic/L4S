using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebPortal.DataContexts;

namespace WebPortal.Models
{
    public class CustomerViewModel
    {
        public CATCustomerData Customer { get; set; }
        public List<ServicesViewModel> Services { get; set; }

        public bool isCompany
        {
            get
            {
                if (Customer.CustomerType == "PO") return true;
                else return false;
            }
        }

        public CustomerViewModel(CATCustomerData customer)
        {
            this.Customer = customer;          
            getAllServices();
        }

        /// <summary>
        /// Load Services for Customer (checked), others unchecked
        /// </summary>
        /// <returns></returns>
        private bool getAllServices()
        {
            L4SDb db = new L4SDb();
            this.Services = new List<ServicesViewModel>();
            List<CATServiceParameters> allServices = db.CATServiceParameters.Where(p=>p.TCActive!=99).ToList();
            List<CATCustomerServices> customerServces = Customer.CATCustomerServices.ToList();

            foreach(CATServiceParameters service in allServices)
            {
                List<CATCustomerServices> foundRecords = customerServces.Where(p => p.FKServiceID == service.PKServiceID && p.TCActive != 99).ToList();

                if (foundRecords.Count == 0) //not present in customer services, than add it as unchecked
                {
                    this.Services.Add(new ServicesViewModel { Checked = false, FKCustomerDataID = -1, FKServiceID = service.PKServiceID, ServiceCode = service.ServiceCode, ServiceName = service.ServiceDescription, ServiceNote = "", ServicePriceDiscount = 1 });
                }
                else if (foundRecords.Count == 1) //present in customer services, than add it as checked
                {
                    CATCustomerServices foundRecord = foundRecords.SingleOrDefault();
                    this.Services.Add(new ServicesViewModel { Checked = true, FKCustomerDataID = foundRecord.FKCustomerDataID, FKServiceID = foundRecord.FKServiceID, ServiceCode = foundRecord.ServiceCode, ServiceName = foundRecord.ServiceName, ServiceNote = foundRecord.ServiceNote, ServicePriceDiscount = foundRecord.ServicePriceDiscount, PKServiceCustomerIdentifiersID= foundRecord.PKServiceCustomerIdentifiersID });
                }
                else //found more active servicess with same code, error
                { return false; }              
            }
            return true;
        }
    }

    public class ServicesViewModel
    {
        public bool Checked { get; set; } 


        public int PKServiceCustomerIdentifiersID { get; set; }

        public int FKServiceID { get; set; }  

        public int FKCustomerDataID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Service_Name", ResourceType = typeof(Labels))]
        public string ServiceName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Service_Code", ResourceType = typeof(Labels))]
        public string ServiceCode { get; set; }

        [Display(Name = "Service_Discount", ResourceType = typeof(Labels))]
        public decimal ServicePriceDiscount { get; set; }

        [StringLength(100)]
        [Display(Name = "Service_Note", ResourceType = typeof(Labels))]
        public string ServiceNote { get; set; }

        public int? TCActive { get; set; }
    }
}