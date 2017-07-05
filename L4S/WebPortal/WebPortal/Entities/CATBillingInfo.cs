using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebPortal
{
    [Table("CATBillingInfo")]
    public partial class CATBillingInfo
    {
       
        public int ID { get; set; }
        public DateTime StartBillingPeriod { get; set; }
        public DateTime StopBillingPeriod { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime DueDate { get; set; }
        public int CustomerID { get; set; }
        public long NumberOfRequest { get; set; }
        public long ReceivedBytes { get; set; }
        public decimal RequestedTime { get; set; }
        [StringLength(50)]
        public string BankAccountIBAN { get; set; }
        public string VariableSymbol { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPriceWithoutVAT { get; set; }
        public decimal VAT { get; set; }
        public decimal TotalPriceWithVAT { get; set; }
        public DateTime? TCInsertTime { get; set; }
        public DateTime? TCLastUpdate { get; set; }
        public int? TCActive { get; set; }
    }
}
