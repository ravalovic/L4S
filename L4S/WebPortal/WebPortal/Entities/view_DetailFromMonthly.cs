using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources;

namespace WebPortal
{

    [Table("view_DetailFromMonthly")]
    public partial class view_DetailFromMonthly
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "TabHead_BatchID", ResourceType = typeof(Labels))]
        public int BatchID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "TabHead_RecordID", ResourceType = typeof(Labels))]
        public int RecordID { get; set; }

        public DateTime? DateOfRequest { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "TabHead_Monthdate", ResourceType = typeof(Labels))]
        public DateTime? Monthdate { get; set; }

        public int? CustomerID { get; set; }
        [StringLength(100)]
        [Display(Name = "TabHead_CustomerIdentification", ResourceType = typeof(Labels))]
        public string CustomerIdentification { get; set; }

        [StringLength(100)]
        [Display(Name = "TabHead_CustomerName", ResourceType = typeof(Labels))]
        public string CustomerName { get; set; }

        public int? ServiceID { get; set; }
        [StringLength(50)]
        [Display(Name = "TabHead_ServiceCode", ResourceType = typeof(Labels))]
        public string ServiceCode { get; set; }

        [Display(Name = "TabHead_ReceivedBytes", ResourceType = typeof(Labels))]
        public long ReceivedBytes { get; set; }

        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_RequestTime", ResourceType = typeof(Labels))]
        public DateTime RequestTime { get; set; }

        [StringLength(8000)]
        [Display(Name = "TabHead_RequestedURL", ResourceType = typeof(Labels))]
        public string RequestedURL { get; set; }

        [StringLength(5)]
        [Display(Name = "TabHead_RequestStatus", ResourceType = typeof(Labels))]
        public string RequestStatus { get; set; }

        [StringLength(1000)]
        [Display(Name = "TabHead_UserIPAddress", ResourceType = typeof(Labels))]
        public string UserIPAddress { get; set; }

        public int TCActive { get; set; }
        public DateTime? TCInsertTime { get; set; }

    }
}
