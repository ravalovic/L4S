using Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebPortal
{
    [Table("STInputFileInfo")]
    public partial class STInputFileInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string FileName { get; set; }

        [Required]
        [StringLength(50)]
        public string Checksum { get; set; }

        [Required]
        public int LinesInFile { get; set; }

        [Required]
        [Display(Name = "File_CreationTime", ResourceType = typeof(Labels))]
        public DateTime InsertDateTime { get; set; }

        [Required]
        public int LoaderBatchID { get; set; }

        [Required]
        public int LoadedRecord { get; set; }

        [StringLength(200)]
        [Display(Name = "File_Name", ResourceType = typeof(Labels))]
        public string OriFileName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "File_CheckSum", ResourceType = typeof(Labels))]
        public string OriginalFileChecksum { get; set; }
        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }
    }
}
