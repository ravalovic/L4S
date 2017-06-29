using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    [Table("STInputFileInfo")]
    public partial class STInputFileInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string FileName { get; set; }

        [Key]
        [StringLength(50)]
        public string Checksum { get; set; }

        [System.ComponentModel.DefaultValue(-1)]
        public int LinesInFile { get; set; }

        public DateTime InsertDateTime { get; set; }

        [System.ComponentModel.DefaultValue(-1)]
        public int LoaderBatchID { get; set; }

        [System.ComponentModel.DefaultValue(-1)]
        public int LoadedRecord { get; set; }

        [StringLength(200)]
        public string OriFileName { get; set; }

        [Required]
        [StringLength(50)]
        public string OriginalFileChecksum { get; set; }
    }
}
//CreateTable(
//    "dbo.STInputFileInfo",
//    c => new
//        {
//            InsertDateTime = c.DateTime(defaultValueSql: "GETDATE()")
//        });