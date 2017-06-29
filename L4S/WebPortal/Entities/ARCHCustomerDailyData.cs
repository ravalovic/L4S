using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{

    [Table("ARCHCustomerDailyData")]
    public partial class ARCHCustomerDailyData
    {
        [Key]
        [Column(Order = 0)]
        public DateTime RequestDate { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServiceID { get; set; }

        public long? NumberOfRequest { get; set; }

        public long? ReceivedBytes { get; set; }

        public decimal? RequestedTime { get; set; }

        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }
    }
}

