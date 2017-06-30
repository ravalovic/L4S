using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
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
        public DateTime InsertDateTime { get; set; }

        [Required]
        public int LoaderBatchID { get; set; }

        [Required]
        public int LoadedRecord { get; set; }

        [StringLength(200)]
        public string OriFileName { get; set; }

        [Required]
        [StringLength(50)]
        public string OriginalFileChecksum { get; set; }
    }
}
