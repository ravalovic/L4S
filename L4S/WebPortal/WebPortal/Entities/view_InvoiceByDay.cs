namespace WebPortal
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("view_InvoiceByDay")]
    public partial class view_InvoiceByDay
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime DateOfRequest { get; set; }

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
        public string CustomerServiceCode { get; set; }
        [StringLength(150)]
        public string CustomerServicename { get; set; }
        [DataType("decimal(18,5)")]
        public decimal UnitPrice { get; set; }
        [StringLength(12)]
        public string MeasureOfUnits { get; set; }
        [DataType("decimal(18,5)")]
        public decimal BasicPriceWithoutVAT { get; set; }
        [DataType("decimal(18,5)")]
        public decimal VAT { get; set; }
        [DataType("decimal(18,5)")]
        public decimal BasicPriceWithVAT { get; set; }
        public int TCActive { get; set; }

    }
}
