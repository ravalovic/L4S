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
        public Int64  batchID { get; set; }

        [StringLength(100)]
        public string originalFileName { get; set; }

        [StringLength(100)]
        public string originalCheckSum { get; set; }

        [StringLength(100)]
        public string preProcessFileName { get; set; }

        [StringLength(100)]
        public string Node_IP_Address { get; set; }

        [StringLength(100)]
        public string UserID { get; set; }

        [StringLength(100)]
        public string Date_Of_Request { get; set; }

        [StringLength(100)]
        public string Requested_URL { get; set; }

        [StringLength(100)]
        public string Request_Status { get; set; }

        [StringLength(100)]
        public string Bytes_Sent { get; set; }

        [StringLength(100)]
        public string Request_Time { get; set; }

        [StringLength(100)]
        public string Unknown { get; set; }

        [StringLength(100)]
        public string User_Agent { get; set; }

        [StringLength(100)]
        public string User_IP_Address { get; set; }
    }
}
