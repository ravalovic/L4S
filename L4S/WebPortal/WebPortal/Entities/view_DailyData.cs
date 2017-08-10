namespace WebPortal
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("view_DailyData")]
    public partial class view_DailyData
    {
        [Key]
        [Column(Order = 0)]
        //[DataType(DataType.Date)]
        public DateTime? DateOfRequest { get; set; }
        [Key]
        [Column(Order = 1)]
        public int CustomerID { get; set; }
        [StringLength(100)]
        public string CustomerIdentification { get; set; }
        [StringLength(101)]
        public string CustomerName { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ServiceID { get; set; }
        [StringLength(50)]
        public string ServiceCode { get; set; }
        public long NumberOfRequest { get; set; }
        public long ReceivedBytes { get; set; }
        [DataType("decimal(18,5)")]
        public decimal RequestedTime { get; set; }
        public int TCActive { get; set; }
    }
}
