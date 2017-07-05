using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        public string ServiceCode { get; set; }

        [Required]
        [StringLength(150)]
        public string ServiceDescription { get; set; }

        public decimal ServiceBasicPrice { get; set; }

        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        [System.ComponentModel.DefaultValue(-1)]
        public int? TCActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<CATCustomerServices> CATCustomerServices { get; set; }

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