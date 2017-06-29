using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Entities
{
    [Table("CATProcessStatus")]
    public partial class CATProcessStatus
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StepName { get; set; }

        /**********  BatchID nie je jasne pouzitie ci Int ci varchar(100)  **********************/
        //[Key]
        //[Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int BatchID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(100)]
        public string BatchID { get; set; }

        public int BatchRecordNum { get; set; }

        public int NumberOfService { get; set; }

        public int NumberOfCustomer { get; set; }

        public int NumberOfUnknownService { get; set; }

        public int NumberOfPreprocessDelete { get; set; }

        public DateTime TCInsertTime { get; set; }
    }
}
