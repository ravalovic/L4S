using Resources;
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
        [Display(Name = "Service_Name", ResourceType = typeof(Labels))]
        public string ServiceName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Service_Code", ResourceType = typeof(Labels))]
        public string ServiceCode { get; set; }

        [Display(Name = "Service_Discount", ResourceType = typeof(Labels))]
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public decimal ServicePriceDiscount { get; set; }

        [StringLength(100)]
        [Display(Name = "Service_Note", ResourceType = typeof(Labels))]
        public string ServiceNote { get; set; }
        
        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }
    }
}
