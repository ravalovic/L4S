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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [System.ComponentModel.DefaultValue(-1)]
        public int OriginalId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string FileName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [System.ComponentModel.DefaultValue(-1)]
        public int LinesInFile { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string Checksum { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime LoadDateTime { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime InsertDateTime { get; set; }

        [StringLength(200)]
        public string OriFileName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string OriginalFileChecksum { get; set; }

        [Key]
        [Column(Order = 7)]
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