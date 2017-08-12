using Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebPortal
{
    [Table("ARCHInputFileDuplicity")]
    public partial class ARCHInputFileDuplicity
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [System.ComponentModel.DefaultValue(-1)]
        public int OriginalId { get; set; }

        [StringLength(200)]
        public string FileName { get; set; }


        [Required]
        public int LinesInFile { get; set; }

        [Required]
        [StringLength(50)]
        public string Checksum { get; set; }

        [Required]
        public DateTime? LoadDateTime { get; set; }

        [Required] 
        [Display(Name = "File_CreationTime", ResourceType = typeof(Labels))]
        public DateTime InsertDateTime { get; set; }


        [StringLength(200)]
        [Display(Name = "File_Name", ResourceType = typeof(Labels))]
        public string OriFileName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "File_CheckSum", ResourceType = typeof(Labels))]
        public string OriginalFileChecksum { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [System.ComponentModel.DefaultValue(-1)]
        public int LoaderBatchID { get; set; }
        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }
    }
}
