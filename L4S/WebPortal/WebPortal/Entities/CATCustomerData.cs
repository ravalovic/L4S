using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebPortal
{
    [Table("CATCustomerData")]

    public partial class CATCustomerData
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public CATCustomerData()
        //{
        //    CATCustomerServices = new HashSet<CATCustomerServices>();
        //    CATCustomerIdentifiers = new HashSet<CATCustomerIdentifiers>();
        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PKCustomerDataID { get; set; }

        [StringLength(6)]
        [Display(Name = "Customer_CustomerType", ResourceType = typeof(Labels))]
        public string CustomerType { get; set; }

        [StringLength(100)]
        [Display(Name = "Customer_CompanyName", ResourceType = typeof(Labels))]
        public string CompanyName { get; set; }

        [StringLength(50)]
        //[Required(ErrorMessage = "Právna forma je povinná")]
        [RegularExpression(@"^[^\s]+", ErrorMessage = "Právna forma je povinná")]
        [Display(Name = "Customer_CompanyType", ResourceType = typeof(Labels))]
        public string CompanyType { get; set; }

        [StringLength(20)]
        //[Required(ErrorMessage = "IČO je povinné")]
        [RegularExpression(@"^\d{6}(\d{2})?$", ErrorMessage = "Musí byť 6 alebo 8 číslic")]
        [Display(Name = "Customer_CompanyID", ResourceType = typeof(Labels))]
        public string CompanyID { get; set; }

        //[StringLength(20)]
        //[Required(ErrorMessage = "DIČ je povinné")]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Musí byť 10 číslic")]
        //[Display(Name = "Customer_CompanyTAXID", ResourceType = typeof(Labels))]
        //public string CompanyTAXID { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "DIČ je povinné")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Musí byť 10 číslic")]
        [Display(Name = "Customer_TAXID", ResourceType = typeof(Labels))]
        public string Customer_TAXID { get; set; }

        //[StringLength(20)]
        //[RegularExpression(@"^[A-Z][A-Z]\d{10}$", ErrorMessage = "Musí byť Kód krajiny a 10 číslic")]
        //[Display(Name = "Customer_CompanyVATID", ResourceType = typeof(Labels))]
        //public string CompanyVATID { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[A-Z][A-Z]\d{10}$", ErrorMessage = "Musí byť Kód krajiny a 10 číslic")]
        [Display(Name = "Customer_VATID", ResourceType = typeof(Labels))]
        public string Customer_VATID { get; set; }

        [StringLength(10)]
        [Display(Name = "Customer_IndividualTitle", ResourceType = typeof(Labels))]
        public string IndividualTitle { get; set; }

        [StringLength(50)]
        [Display(Name = "Customer_IndividualFirstName", ResourceType = typeof(Labels))]
        public string IndividualFirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Customer_IndividualLastName", ResourceType = typeof(Labels))]
        public string IndividualLastName { get; set; }

        [StringLength(20)]
        [Display(Name = "Customer_IndividualID", ResourceType = typeof(Labels))]
        public string IndividualID { get; set; }

        //[StringLength(20)]
        //[Required(ErrorMessage = "DIČ je povinné")]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Musí byť 10 číslic")]
        //[Display(Name = "Customer_IndividualTAXID", ResourceType = typeof(Labels))]
        //public string IndividualTAXID { get; set; }

        //[StringLength(20)]
        //[RegularExpression(@"^[A-Z][A-Z]\d{10}$", ErrorMessage = "Musí byť Kód krajiny a 10 číslic")]
        //[Display(Name = "Customer_IndividualVATID", ResourceType = typeof(Labels))]
        //public string IndividualVATID { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^[A-Z][A-Z]\d{2}[ ]\d{4}[ ]\d{4}[ ]\d{4}[ ]\d{4}[ ]\d{4}|[A-Z][A-Z]\d{22}$", ErrorMessage = "IBAN v celku alebo s medzerami po 4 znakoch")]
        [Display(Name = "Customer_BankAccountIBAN", ResourceType = typeof(Labels))]
        public string BankAccountIBAN { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Adresa je povinná")]
        [Display(Name = "Customer_AddressStreet", ResourceType = typeof(Labels))]
        public string AddressStreet { get; set; }

        [Required(ErrorMessage = "Číslo je povinné")]
        [StringLength(20)]
        [Display(Name = "Customer_AddressBuildingNumber", ResourceType = typeof(Labels))]
        public string AddressBuildingNumber { get; set; }

       
        [StringLength(50)]
        [Required(ErrorMessage = "Mesto je povinné")]
        [Display(Name = "Customer_AddressCity", ResourceType = typeof(Labels))]
        public string AddressCity { get; set; }

        [Required(ErrorMessage = "PSČ je povinné")]
        [RegularExpression(@"^\d{5}$|^\d{3}(\s\d{2})?$", ErrorMessage = "Format XXXXXX alebo XXX XX")]
        [StringLength(6)]
        [Display(Name = "Customer_AddressZipCode", ResourceType = typeof(Labels))]
        public string AddressZipCode { get; set; }

        [StringLength(50)]
        [Display(Name = "Customer_AddressCountry", ResourceType = typeof(Labels))]
        public string AddressCountry { get; set; }

        
        [StringLength(50)]
        [Display(Name = "Customer_ContactEmail", ResourceType = typeof(Labels))]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,22}$", ErrorMessage = "Chybný email")]
        public string ContactEmail { get; set; }

        [StringLength(50)]
        [Display(Name = "Customer_ContactMobile", ResourceType = typeof(Labels))]
        public string ContactMobile { get; set; }

        [StringLength(50)]
        [Display(Name = "Customer_ContactPhone", ResourceType = typeof(Labels))]
        public string ContactPhone { get; set; }

        [StringLength(100)]
        [Display(Name = "Customer_ContactWeb", ResourceType = typeof(Labels))]
        public string ContactWeb { get; set; }

        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CATCustomerServices> CATCustomerServices { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CATCustomerIdentifiers> CATCustomerIdentifiers { get; set; }


        public virtual string FullName
        { get
            { if (CustomerType == "PO") return CompanyName + " " + CompanyType;
                else return IndividualTitle + " " + IndividualFirstName + " " + IndividualLastName;
            }
        }

        [Display(Name = "Customer_Address", ResourceType = typeof(Labels))]
        public virtual string Address { get { return AddressStreet + " " + AddressBuildingNumber + ", " + AddressZipCode + " " + AddressCity + ", " + AddressCountry; } }

    }
}
