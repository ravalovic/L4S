using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4S.Entities
{
    public class Stage_InputFileInfo
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [StringLength(100)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string Checksum { get; set; }

        public DateTime InsertDateTime { get; set; }
       
        public int LoaderBatchID { get; set; }
        
        public int LoadedRecord { get; set; }
    }
}
