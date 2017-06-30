using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    [Table("STInputFileDuplicity")]
    public partial class STInputFileDuplicity
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
        public DateTime LoadDateTime { get; set; }

        [Required]
        public DateTime InsertDateTime { get; set; }

        [StringLength(200)]
        public string OriFileName { get; set; }

        [Required]
        [StringLength(50)]
        public string OriginalFileChecksum { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [System.ComponentModel.DefaultValue(-1)]
        public int LoaderBatchID { get; set; }
    }
}
//CreateTable(
//    "dbo.STInputFileDuplicity",
//    c => new
//        {
//            LoadDateTime = c.DateTime(defaultValueSql: "GETDATE()")
//            InsertDateTime = c.DateTime(defaultValueSql: "GETDATE()")
//        });