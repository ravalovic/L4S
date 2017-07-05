using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{

    [Table("ARCHCustomerDailyData")]
    public partial class ARCHCustomerDailyData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime DateOfRequest { get; set; }
        
        public int CustomerID { get; set; }

        public int ServiceID { get; set; }

        public long? NumberOfRequest { get; set; }

        public long? ReceivedBytes { get; set; }
        
        public decimal? RequestedTime { get; set; }

        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }
    }
}

