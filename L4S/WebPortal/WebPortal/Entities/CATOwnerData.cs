using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPortal
{
    [Table("CATOwnerData")]
    public  partial class CATOwnerData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(100)]
        public string OwnerCompanyName { get; set; }
        [StringLength(12)]
        public string OwnwerCompanyType { get; set; }
        [StringLength(100)]
        public string OwnerCompanyType { get; set; }
        [StringLength(100)]
        public string OwnerCompanyID { get; set; }
        [StringLength(100)]
        public string OwnerCompanyTAXID { get; set; }
        [StringLength(100)]
        public string OwnerCompanyVATID { get; set; }
        [StringLength(50)]
        public string OwnerBankAccountIban { get; set; }
        [StringLength(100)]
        public string OwnerAddressStreet { get; set; }
        [StringLength(100)]
        public string OwnerAddressBuildingNumber { get; set; }
        [StringLength(100)]
        public string OwnerAddressCity { get; set; }
        [StringLength(12)]
        public string OwnerAddressZipCode { get; set; }
        [StringLength(100)]
        public string OwnerAddressCountry { get; set; }
        [StringLength(100)]
        public string OwnerResponsibleFirstName { get; set; }
        [StringLength(100)]
        public string OwnerResponsiblelastName { get; set; }
        [StringLength(100)]
        public string OwnerContactEmail { get; set; }
        [StringLength(100)]
        public string OwnerContactMobile { get; set; }
        [StringLength(100)]
        public string OwnerContactPhone { get; set; }
        [StringLength(100)]
        public string OwnerContactWeb { get; set; }
        public DateTime? TCInsertTime { get; set; }
        public DateTime? TCLastUpdate { get; set; }
        public int? TCActive { get; set; }
    }
}
