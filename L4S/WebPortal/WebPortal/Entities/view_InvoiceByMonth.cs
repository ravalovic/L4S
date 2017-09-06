using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources;

namespace WebPortal
{
    [Table("view_InvoiceByMonth")]
    public class view_InvoiceByMonth
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "TabHead_DateOfRequest", ResourceType = typeof(Labels))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfRequest { get; set; }

        [Display(Name = "TabHead_CustomerID", ResourceType = typeof(Labels))]
        public int CustomerID { get; set; }

        [StringLength(20)]
        [Display(Name = "TabHead_CustomerIdentification", ResourceType = typeof(Labels))]
        public string CustomerIdentification { get; set; }

        [StringLength(100)]
        [Display(Name = "TabHead_CustomerName", ResourceType = typeof(Labels))]
        public string CustomerName { get; set; }

        [Display(Name = "TabHead_ServiceID", ResourceType = typeof(Labels))]
        public int ServiceID { get; set; }

        [StringLength(50)]
        [Display(Name = "TabHead_ServiceCode", ResourceType = typeof(Labels))]
        public string ServiceCode { get; set; }

        [StringLength(150)]
        [Display(Name = "TabHead_ServiceDescription", ResourceType = typeof(Labels))]
        public string ServiceDescription { get; set; }

        [Display(Name = "TabHead_NumberOfRequest", ResourceType = typeof(Labels))]
        public long NumberOfRequest { get; set; }

        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_InvoiceReceivedBytes", ResourceType = typeof(Labels))]
        public decimal ReceivedBytes { get; set; }

        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_RequestedTime", ResourceType = typeof(Labels))]
        public decimal RequestedTime { get; set; }

        [StringLength(50)]
        [Display(Name = "TabHead_CustomerServiceCode", ResourceType = typeof(Labels))]
        public string CustomerServiceCode { get; set; }

        [StringLength(150)]
        [Display(Name = "TabHead_CustomerServicename", ResourceType = typeof(Labels))]
        public string CustomerServicename { get; set; }

        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_UnitPrice", ResourceType = typeof(Labels))]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal UnitPrice { get; set; }

        [StringLength(12)]
        [Display(Name = "TabHead_MeasureOfUnits", ResourceType = typeof(Labels))]
        public string MeasureOfUnits { get; set; }

        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_BasicPriceWithoutVAT", ResourceType = typeof(Labels))]
        public decimal BasicPriceWithoutVAT { get; set; }

        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_VAT", ResourceType = typeof(Labels))]
        public decimal VAT { get; set; }

        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_BasicPriceWithVAT", ResourceType = typeof(Labels))]
        public decimal BasicPriceWithVAT { get; set; }

        public int TCActive { get; set; }

    }
}