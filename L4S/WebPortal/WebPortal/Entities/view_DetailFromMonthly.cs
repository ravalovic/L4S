namespace WebPortal
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("view_DetailFromMonthly")]
    public partial class view_DetailFromMonthly
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecordID { get; set; }

        public DateTime? DateOfRequest { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Monthdate { get; set; }

        public int? CustomerID { get; set; }
        [StringLength(100)]
        public string CustomerIdentification { get; set; }
        [StringLength(100)]
        public string CustomerName { get; set; }
        public int? ServiceID { get; set; }
        [StringLength(50)]
        public string ServiceCode { get; set; }
        public long ReceivedBytes { get; set; }

        [DataType("decimal(18,5)")]
        public DateTime RequestTime { get; set; }

        [StringLength(8000)]
        public string RequestedURL { get; set; }

        [StringLength(5)]
        public string RequestStatus { get; set; }

        [StringLength(1000)]
        public string UserIPAddress { get; set; }
        public int TCActive { get; set; }
        public DateTime? TCInsertTime { get; set; }

    }
}
