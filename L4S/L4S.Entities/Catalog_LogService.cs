using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4S.Entities
{

   // [Table("Catalog_LogService")] //use different name in DB, if not Class name
    public class Catalog_LogService
    {

        [Key, Column(Order = 0)]
        public long BatchID { get; set; }

        [Required]
        public long FileID { get; set; }

        [Required]
        public DateTime InsertDateTime { get; set; }

        public string CustomerID { get; set; }

        public Nullable<int> ServiceID { get; set; }

        //[StringLength(100)]
        //public string col1 { get; set; }

        //[StringLength(100)]
        //public string col2 { get; set; }

        //[StringLength(100)]
        //public string col3 { get; set; }

        //[StringLength(100)]
        //public string col4 { get; set; }

        //[StringLength(100)]
        //public string col5 { get; set; }

        //[StringLength(100)]
        //public string col6 { get; set; }

        //[StringLength(100)]
        //public string col7 { get; set; }

        //[StringLength(100)]
        //public string col8 { get; set; }

        //[StringLength(100)]
        //public string col9 { get; set; }

      //  [ForeignKey("Pomenovanie")]
      //  public virtual ClassName Name { get; set; } //priradenie cudzieho kluca
    }
}
