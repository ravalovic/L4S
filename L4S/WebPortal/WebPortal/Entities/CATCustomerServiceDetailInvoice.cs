using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebPortal
{
    [Table("CATCustomerServiceDetailInvoice")]
    public partial class CATCustomerServiceDetailInvoice
    {
        public int ID { get; set; }
        [StringLength(50)]
        public string InvoiceNumber { get; set; }
        public DateTime StartBillingPeriod { get; set; }
        [DataType(DataType.Date)]
        public DateTime StopBillingPeriod { get; set; }
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public int CustomerID { get; set; }
        [StringLength(20)]
        public string CustomerIdentification { get; set; }
        [StringLength(100)]
        public string CustomerName { get; set; }
        public int ServiceID { get; set; }
        [StringLength(50)]
        public string ServiceCode { get; set; }
        [StringLength(150)]
        public string ServiceDescription { get; set; }
        public long NumberOfRequest { get; set; }
        [DataType("decimal(18,5)")]
        public decimal ReceivedBytes { get; set; }
        [DataType("decimal(18,5)")]
        public decimal RequestedTime { get; set; }
        [StringLength(50)]
        public string MeasureofUnits { get; set; }
        [StringLength(50)]
        public string CustomerServiceCode { get; set; }
        [StringLength(150)]
        public string CustomerServicename { get; set; }
        [DataType("decimal(18,5)")]
        public decimal UnitPrice { get; set; }
        [DataType("decimal(18,5)")]
        public decimal TotalPriceWithoutVAT { get; set; }
        [DataType("decimal(18,5)")]
        public decimal VAT { get; set; }
        [DataType("decimal(18,5)")]
        public decimal TotalPriceWithVAT { get; set; }
        public DateTime? TCInsertTime { get; set; }
        public DateTime? TCLastUpdate { get; set; }
        public int? TCActive { get; set; }
    }
}
