using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources;
namespace WebPortal
{
    [Table("view_CustomerMontlyTotalInvoice")]
    public partial class view_CustomerMontlyTotalInvoice
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        [Display(Name = "TabHead_InvoiceNumber", ResourceType = typeof(Labels))]
        public string InvoiceNumber { get; set; }
        [Display(Name = "TabHead_StartBillingPeriod", ResourceType = typeof(Labels))]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartBillingPeriod { get; set; }
        [Display(Name = "TabHead_StopBillingPeriod", ResourceType = typeof(Labels))]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StopBillingPeriod { get; set; }
       [Display(Name = "TabHead_DeliveryDate", ResourceType = typeof(Labels))]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }
       [Display(Name = "TabHead_DueDate", ResourceType = typeof(Labels))]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        [Display(Name = "TabHead_CustomerID", ResourceType = typeof(Labels))]
        public int CustomerID { get; set; }
        [StringLength(20)]
        [Display(Name = "TabHead_CustomerIdentification", ResourceType = typeof(Labels))]
        public string CustomerIdentification { get; set; }
        [StringLength(100)]
        [Display(Name = "TabHead_CustomerName", ResourceType = typeof(Labels))]
        public string CustomerName { get; set; }
        [Display(Name = "TabHead_NumberOfRequest", ResourceType = typeof(Labels))]
        public long NumberOfRequest { get; set; }
        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_InvoiceReceivedBytes", ResourceType = typeof(Labels))]
        public decimal ReceivedBytes { get; set; }
        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_RequestedTime", ResourceType = typeof(Labels))]
        public decimal RequestedTime { get; set; }
        [StringLength(50)]
        [Display(Name = "TabHead_MeasureOfUnits", ResourceType = typeof(Labels))]
        public string MeasureOfUnits { get; set; }
        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_TotalPriceWithoutVAT", ResourceType = typeof(Labels))]
        public decimal TotalPriceWithoutVAT { get; set; }
        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_VAT", ResourceType = typeof(Labels))]
        public decimal VAT { get; set; }
        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_TotalPriceWithVAT", ResourceType = typeof(Labels))]
        public decimal TotalPriceWithVAT { get; set; }
    }
}
