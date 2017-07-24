using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebPortal
{
   [Table("CATCustomerServices")]
    public partial class CATCustomerServices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PKServiceCustomerIdentifiersID { get; set; }

        public int FKServiceID { get; set; }
        [ForeignKey("FKServiceID")]
        public virtual CATServiceParameters ServiceParameters { get; set; }


        public int FKCustomerDataID { get; set; }
        [ForeignKey("FKCustomerDataID")]
        public virtual CATCustomerData CATCustomerData { get; set; }


        [Required]
        [StringLength(100)]
        public string ServiceName { get; set; }

        [Required]
        [StringLength(50)]
        public string ServiceCode { get; set; }

        public decimal ServicePriceDiscount { get; set; }
        [StringLength(100)]
        public string ServiceNote { get; set; }
        
        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }
    }
}
