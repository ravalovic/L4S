using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4S.Entities
{
    // [Table("Catalog_Service")] //use different name in DB, if not Class name
    public class Catalog_Service
    {
        [Key, Column(Order = 0)]
        public int ServiceID { get; set; }

        [StringLength(500)]
        public string Identifier { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int? UserID { get; set; }  //if nullable int? else int

        public DateTime InsertDateTime { get; set; }
    }
}
