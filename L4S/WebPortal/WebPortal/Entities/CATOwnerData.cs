using System;
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
        [Required(ErrorMessage="Meno spoločnosti je povinné")]
        [StringLength(100)]
        public string OwnerCompanyName { get; set; }
        [Required(ErrorMessage = "Právna forma je povinná")]
        [StringLength(100)]
        public string OwnerCompanyType { get; set; }
        [Required(ErrorMessage = "IČO je povinné")]
        [RegularExpression(@"^\d{6}(\d{2})?$", ErrorMessage = "Musí byť 6 alebo 8 číslic")]
        [StringLength(100)]
        public string OwnerCompanyID { get; set; }

        [Required(ErrorMessage = "DIČ je povinné")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Musí byť 10 číslic")]
        [StringLength(100)]
        public string OwnerCompanyTAXID { get; set; }

        [RegularExpression(@"^SK\d{10}$", ErrorMessage = "Musí byť SK a 10 číslic")]
        [StringLength(100)]
        public string OwnerCompanyVATID { get; set; }

        [RegularExpression(@"^[A-Z][A-Z]\d{2}[ ]\d{4}[ ]\d{4}[ ]\d{4}[ ]\d{4}[ ]\d{4}|[A-Z][A-Z]\d{22}$", ErrorMessage = "IBAN v celku alebo s medzerami po 4 znakoch")]
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
        [RegularExpression(@"^\d{5}$|^\d{3}(\s\d{2})?$", ErrorMessage = "Format XXXXXX alebo XXX XX")]
        [StringLength(12)]
        public string OwnerAddressZipCode { get; set; }
        [StringLength(100)]
        public string OwnerAddressCountry { get; set; }
        [StringLength(100)]
        public string OwnerResponsibleFirstName { get; set; }
        [StringLength(100)]
        public string OwnerResponsiblelastName { get; set; }
        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Chybný email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,22}$", ErrorMessage = "Chybný email")]
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
