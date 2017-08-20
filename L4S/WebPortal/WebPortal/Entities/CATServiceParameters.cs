using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources;

namespace WebPortal
{
    [Table("CATServiceParameters")]
    public sealed partial class CATServiceParameters
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public CATServiceParameters()
        //{
        //    CATCustomerServices = new HashSet<CATCustomerServices>();
        //    CATServicePatterns = new HashSet<CATServicePatterns>();
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PKServiceID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Service_Code", ResourceType = typeof(Labels))]
        public string ServiceCode { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Service_Description", ResourceType = typeof(Labels))]
        public string ServiceDescription { get; set; }

        [Display(Name = "Service_BasicPrice", ResourceType = typeof(Labels))]
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public decimal ServiceBasicPrice { get; set; }

        [Display(Name = "Service_InsertTime", ResourceType = typeof(Labels))]
        public DateTime? TCInsertTime { get; set; }

        [Display(Name = "Service_LastUpdate", ResourceType = typeof(Labels))]
        public DateTime? TCLastUpdate { get; set; }

        [Display(Name = "Service_Active", ResourceType = typeof(Labels))]
        [System.ComponentModel.DefaultValue(-1)]
        public int? TCActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<CATCustomerServices> CATCustomerServices { get; set; }

        [Display(Name = "Service_Patterns", ResourceType = typeof(Labels))]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<CATServicePatterns> CATServicePatterns { get; set; }
    }
}

//CreateTable(
//    "dbo.CATServiceParameters",
//    c => new
//        {
//            TCInsertTime = c.DateTime(defaultValueSql: "GETDATE()")
//            TCLastUpdate = c.DateTime(defaultValueSql: "GETDATE()")
//        });