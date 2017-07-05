namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class view_DetailFromMonthly
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BatchID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecordID { get; set; }

        public DateTime? DateOfRequest { get; set; }

        public DateTime? Monthdate { get; set; }

        public int? CustomerID { get; set; }

        public int? ServiceID { get; set; }

        [StringLength(15)]
        public string BytesSent { get; set; }

        [StringLength(15)]
        public string RequestTime { get; set; }

        [StringLength(8000)]
        public string RequestedURL { get; set; }

        [StringLength(5)]
        public string RequestStatus { get; set; }

        [StringLength(1000)]
        public string UserIPAddress { get; set; }
    }
}
