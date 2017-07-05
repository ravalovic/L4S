using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace WebPortal
{
    [Table("CATProcessStatus")]
    public partial class CATProcessStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public string StepName { get; set; }

        [StringLength(100)]
        public string BatchID { get; set; }

        public int? BatchRecordNum { get; set; }

        public int? NumberOfService { get; set; }

        public int? NumberOfCustomer { get; set; }

        public int? NumberOfUnknownService { get; set; }

        public int? NumberOfPreprocessDelete { get; set; }

        public DateTime? TCInsertTime { get; set; }
    }
}
