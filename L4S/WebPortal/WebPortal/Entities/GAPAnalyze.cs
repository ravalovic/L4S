using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources;



namespace WebPortal
{
    [Table("GAPAnalyze")]
    public partial class GAPAnalyze
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "Date")]
       [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "GAP_DayOfProcess", ResourceType = typeof(Labels))]
        public DateTime Day { get; set; }
        [Display(Name = "GAP_FileNumber", ResourceType = typeof(Labels))]
        public int? FileNumber { get; set; }
        [StringLength(1000)]
        [Display(Name = "GAP_FileName", ResourceType = typeof(Labels))]
        public string FileName { get; set; }
        [Display(Name = "GAP_TCInsert", ResourceType = typeof(Labels))]
        public DateTime? TCInsertTime { get; set; }
    }
}
