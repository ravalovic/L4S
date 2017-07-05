using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace Entities
{
    [Table("CATServiceParameters")]
    public sealed partial class CATServiceParameters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATServiceParameters()
        {
            CATCustomerServices = new HashSet<CATCustomerServices>();
            CATServicePatterns = new HashSet<CATServicePatterns>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PKServiceID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Service_Code", ResourceType = typeof(Label))]
        public string ServiceCode { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Service_Description", ResourceType = typeof(Label))]
        public string ServiceDescription { get; set; }

        [Display(Name = "Service_BasicPrice", ResourceType = typeof(Label))]
        public decimal ServiceBasicPrice { get; set; }

        [Display(Name = "Service_InsertTime", ResourceType = typeof(Label))]
        public DateTime? TCInsertTime { get; set; }

        [Display(Name = "Service_LastUpdate", ResourceType = typeof(Label))]
        public DateTime? TCLastUpdate { get; set; }

        [Display(Name = "Service_Active", ResourceType = typeof(Label))]
        [System.ComponentModel.DefaultValue(-1)]
        public int? TCActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<CATCustomerServices> CATCustomerServices { get; set; }

        [Display(Name = "Service_Patterns", ResourceType = typeof(Label))]
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