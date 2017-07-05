using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("view_InvoiceByMonth")]
    public class view_InvoiceByMonth
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime DateOfRequest { get; set; }

        public int CustomerID { get; set; }

        public int ServiceID { get; set; }

        public long NumberOfRequest { get; set; }

        public long ReceivedBytes { get; set; }
        public decimal RequestedTime { get; set; }

        [StringLength(50)]
        public string ServiceCode { get; set; }

        [StringLength(150)]
        public string ServiceName { get; set; }
        [StringLength(10)]
        public string AccountVariableSymbol { get; set; }
        public decimal BasicPriceWithoutVAT { get; set; }
        public decimal VAT { get; set; }
        public decimal BasicPriceWithVAT { get; set; }
    }
}