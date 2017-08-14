using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources;
namespace WebPortal
{


    [Table("view_MonthlyData")]
    public partial class view_MonthlyData
    {
        [Key]
        [Column(Order = 0)]
        [DataType(DataType.Date)]
        [Display(Name = "TabHead_DateOfRequest", ResourceType = typeof(Labels))]
        public DateTime? DateOfRequest { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "TabHead_CustomerID", ResourceType = typeof(Labels))]
        public int CustomerID { get; set; }

        [StringLength(100)]
        [Display(Name = "TabHead_CustomerIdentification", ResourceType = typeof(Labels))]
        public string CustomerIdentification { get; set; }

        [StringLength(101)]
        [Display(Name = "TabHead_CustomerName", ResourceType = typeof(Labels))]
        public string CustomerName { get; set; }

        [Key]
        [Column(Order = 2)]
        [Display(Name = "TabHead_ServiceID", ResourceType = typeof(Labels))]
        public int ServiceID { get; set; }

        [StringLength(50)]
        [Display(Name = "TabHead_ServiceCode", ResourceType = typeof(Labels))]
        public string ServiceCode { get; set; }

        [Display(Name = "TabHead_NumberOfRequest", ResourceType = typeof(Labels))]
        public long NumberOfRequest { get; set; }

        [Display(Name = "TabHead_ReceivedBytes", ResourceType = typeof(Labels))]
        public long ReceivedBytes { get; set; }

        [DataType("decimal(18,5)")]
        [Display(Name = "TabHead_RequestedTime", ResourceType = typeof(Labels))]
        public decimal RequestedTime { get; set; }

        public int TCActive { get; set; }
    }
}
