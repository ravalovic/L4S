using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    [Table("CATChangeDetect")]
    public partial class CATChangeDetect
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(50)]
        public string TableName { get; set; }

        public int? Status { get; set; }

        [StringLength(100)]
        public string StatusName { get; set; }

        public DateTime? TCInsertTime { get; set; } //DEFAULT (getdate())

        public DateTime? TCLastUpdate { get; set; } //DEFAULT (getdate())
    }
}

