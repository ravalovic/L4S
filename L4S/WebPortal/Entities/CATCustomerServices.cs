using System;
using System.ComponentModel.DataAnnotations;



namespace Entities
{
    public partial class CATCustomerServices
    {
        [Key]
        public int PKServiceCustomerIdentifiersID { get; set; }

        public int ServiceID { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceName { get; set; }

        [Required]
        [StringLength(50)]
        public string ServiceCode { get; set; }

        [StringLength(100)]
        public string ServiceCustomerName { get; set; }

        [StringLength(100)]
        public string ServiceNote { get; set; }

        public int? FKCustomerDataID { get; set; }

        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }

        public virtual CATCustomerData CATCustomerData { get; set; }

        public virtual CATServiceParameters CATServiceParameters { get; set; }
    }
}
