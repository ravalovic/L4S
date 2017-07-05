using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebPortal
{
    [Table("CATCustomerData")]

    public partial class CATCustomerData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATCustomerData()
        {
            CATCustomerServices = new HashSet<CATCustomerServices>();
            CATCustomerIdentifiers = new HashSet<CATCustomerIdentifiers>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PKCustomerDataID { get; set; }

        [StringLength(6)]
        public string CustomerType { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string CompanyType { get; set; }

        [StringLength(20)]
        public string CompanyID { get; set; }

        [StringLength(20)]
        public string CompanyTAXID { get; set; }

        [StringLength(20)]
        public string CompanyVATID { get; set; }


        [StringLength(10)]
        public string IndividualTitle { get; set; }

        [StringLength(50)]
        public string IndividualFirstName { get; set; }

        [StringLength(50)]
        public string IndividualLastName { get; set; }

        [StringLength(20)]
        public string IndividualID { get; set; }

        [StringLength(20)]
        public string IndividualTAXID { get; set; }

        [StringLength(20)]
        public string IndividualVATID { get; set; }

        [StringLength(50)]
        public string BankAccountIBAN { get; set; }

        [StringLength(50)]
        public string AddressStreet { get; set; }

        [Required]
        [StringLength(20)]
        public string AddressBuildingNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string AddressCity { get; set; }

        [Required]
        [StringLength(6)]
        public string AddressZipCode { get; set; }

        [StringLength(50)]
        public string AddressCountry { get; set; }

        [StringLength(50)]
        public string ContactEmail { get; set; }

        [StringLength(50)]
        public string ContactMobile { get; set; }

        [StringLength(50)]
        public string ContactPhone { get; set; }

        [StringLength(100)]
        public string ContactWeb { get; set; }

        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<CATCustomerServices> CATCustomerServices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<CATCustomerIdentifiers> CATCustomerIdentifiers { get; set; }
    }
}
