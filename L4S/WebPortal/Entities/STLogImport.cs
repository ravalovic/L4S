using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    [Table("STLogImport")]
    public partial class STLogImport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int BatchID { get; set; }

        [Required]
        public int RecordID { get; set; }

        [Required]
        [StringLength(50)]
       public string OriginalCheckSum { get; set; }

        [StringLength(50)]
        public string NodeIPAddress { get; set; }

        [StringLength(50)]
        public string UserID { get; set; }

        [StringLength(30)]
        public string DateOfRequest { get; set; }
        [StringLength(8000)]
        public string RequestedURL { get; set; }

        [StringLength(5)]
        public string RequestStatus { get; set; }

        [StringLength(15)]
        public string BytesSent { get; set; }

        [StringLength(15)]
        public string RequestTime { get; set; }
        [StringLength(8000)]
        public string HttpRefferer { get; set; }

        [StringLength(500)]
        public string UserAgent { get; set; }

        [StringLength(1000)]
        public string UserIPAddress { get; set; }

        public int? CustomerID { get; set; }

        public DateTime? DatDate { get; set; }
    }
}

