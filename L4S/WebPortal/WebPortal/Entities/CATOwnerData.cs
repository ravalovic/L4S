using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace WebPortal
{
    [Table("CATOwnerData")]
    public  partial class CATOwnerData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required(ErrorMessage="Meno spoločnosti je povinné")]
        [StringLength(100)]
        public string OwnerCompanyName { get; set; }
        [Required(ErrorMessage = "Právna forma je povinná")]
        [StringLength(100)]
        public string OwnerCompanyType { get; set; }
        [Required(ErrorMessage = "IČO je povinné")]
        [StringLength(100)]
        public string OwnerCompanyID { get; set; }
        [Required(ErrorMessage = "DIČ je povinné")]
        [StringLength(100)]
        public string OwnerCompanyTAXID { get; set; }
        [StringLength(100)]
        public string OwnerCompanyVATID { get; set; }
        [StringLength(50)]
        public string OwnerBankAccountIban { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Adresa je povinná")]
        public string OwnerAddressStreet { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Číslo je povinné")]
        public string OwnerAddressBuildingNumber { get; set; }
        [Required(ErrorMessage = "Mesto je povinné")]
        [StringLength(100)]
        public string OwnerAddressCity { get; set; }
        [Required(ErrorMessage = "PSČ je povinné")]
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
        [NotMapped]
        public virtual bool Active {
            get
            {
                if (TCActive.HasValue && TCActive.Value == 1) return true;
                else return false;
            }
            set
            {
                if (value) TCActive = 1;
                else TCActive = 0;
            } }
               
    }
}
