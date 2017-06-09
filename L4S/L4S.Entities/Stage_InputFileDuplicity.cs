using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4S.Entities
{
    public class Stage_InputFileDuplicity
    {
        [Key, Column(Order = 0)]
        public int id { get; set; }

        [StringLength(100)]
        public string fileName { get; set; }

        [StringLength(100)]
        public string checksum { get; set; }

        public DateTime loadDateTime { get; set; }

        public DateTime insertDateTime { get; set; }

        [StringLength(100)]
        public string oriFileName { get; set; }

        public int loaderBatchID { get; set; }
    }
}
