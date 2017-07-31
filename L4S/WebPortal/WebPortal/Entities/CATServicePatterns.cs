using Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPortal
{
    [Table("CATServicePatterns")]
    public partial class CATServicePatterns
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PKServicePatternID { get; set; }

        [Required]
        [StringLength(2000)]
        [Display(Name = "Pattern_Like", ResourceType = typeof(Labels))]
        public string PatternLike { get; set; }

        [StringLength(2000)]
        [Display(Name = "Pattern_Regex", ResourceType = typeof(Labels))]
        public string PatternRegExp { get; set; }

        [StringLength(50)]
        [Display(Name = "Pattern_Description", ResourceType = typeof(Labels))]
        public string PatternDescription { get; set; }

        public int FKServiceID { get; set; }
        [ForeignKey("FKServiceID")]
        public virtual CATServiceParameters CATServiceParameters { get; set; }


        [StringLength(150)]
        [Display(Name = "Pattern_Entity", ResourceType = typeof(Labels))]
        public string Entity { get; set; }

        [StringLength(150)]
        [Display(Name = "Pattern_Explanation", ResourceType = typeof(Labels))]
        public string Explanation { get; set; } 

        [StringLength(150)]
        [Display(Name = "Pattern_DatSelectMethod", ResourceType = typeof(Labels))]
        public string DatSelectMethod { get; set; }

        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }

        //public virtual CATServiceParameters CATServiceParameters { get; set; }

    }
}
