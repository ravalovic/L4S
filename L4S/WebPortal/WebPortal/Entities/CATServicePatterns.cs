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
        public string PatternLike { get; set; }

        [StringLength(2000)]
        public string PatternRegExp { get; set; }

        [StringLength(50)]
        public string PatternDescription { get; set; }

        public int FKServiceID { get; set; }
        [ForeignKey("FKServiceID")]
        public virtual CATServiceParameters CATServiceParameters { get; set; }


        [StringLength(150)]
        public string Entity { get; set; }

        [StringLength(150)]
        public string Explanation { get; set; } 

        [StringLength(150)]
        public string DatSelectMethod { get; set; }

        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }

        //public virtual CATServiceParameters CATServiceParameters { get; set; }

    }
}
