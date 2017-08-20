using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources;

namespace WebPortal
{
    [Table("CONFGeneralSettings")]
    public  partial class CONFGeneralSettings
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(100)]
        [Display(Name = "TabHead_ParamName", ResourceType = typeof(Labels))]
        public string ParamName { get; set; }
        [Display(Name = "TabHead_ParamValue", ResourceType = typeof(Labels))]
        [StringLength(100)]
        public string ParamValue { get; set; }
        [Display(Name = "TabHead_ParamNote", ResourceType = typeof(Labels))]
        [StringLength(200)]
        public string Note { get; set; }
        public DateTime? TCInsertTime { get; set; }
        public DateTime? TCLastUpdate { get; set; }
        public int? TCActive { get; set; }
    }
}
