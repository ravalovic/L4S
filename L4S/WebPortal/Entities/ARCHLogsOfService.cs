using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("ARCHLogsOfService")]
    public partial class ARCHLogsOfService
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecordID { get; set; }

        public int? CustomerID { get; set; }

        public int? ServiceID { get; set; }

        [StringLength(50)]
        public string UserID { get; set; }

        public DateTime? DateOfRequest { get; set; }

        public string RequestedURL { get; set; }

        [StringLength(5)]
        public string RequestStatus { get; set; }

        [StringLength(15)]
        public string BytesSent { get; set; }

        [StringLength(15)]
        public string RequestTime { get; set; }

        [StringLength(500)]
        public string UserAgent { get; set; }

        [StringLength(1000)]
        public string UserIPAddress { get; set; }

        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }
    }
}
