using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4S.Entities
{
    public class Stage_LogImport
    {
        [Key, Column(Order = 0)]
        public Int64  BatchID { get; set; }

        [StringLength(100)]
        public string OriginalFileName { get; set; }

        [StringLength(100)]
        public string OriginalCheckSum { get; set; }

        [StringLength(100)]
        public string PreProcessFileName { get; set; }

        [StringLength(100)]
        public string NodeIPAddress { get; set; }

        [StringLength(100)]
        public string UserID { get; set; }

        [StringLength(50)]
        public string DateOfRequest { get; set; }

        [StringLength(4000)]
        public string RequestedURL { get; set; }

        [StringLength(5)]
        public string RequestStatus { get; set; }

        [StringLength(20)]
        public string BytesSent { get; set; }

        [StringLength(20)]
        public string RequestTime { get; set; }

        [StringLength(100)]
        public string HttpReferer { get; set; }

        [StringLength(4000)]
        public string UserAgent { get; set; }

        [StringLength(50)]
        public string UserIPAddress { get; set; }
    }
}
