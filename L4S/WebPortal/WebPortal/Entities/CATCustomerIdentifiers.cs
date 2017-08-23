using Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPortal
{
[Table("CATCustomerIdentifiers")]
    public partial class CATCustomerIdentifiers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PKCustomerIdentifiersID { get; set; }

        [Required]
        [StringLength(200)]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "Musí mať tvar IP adresy")]
        [Display(Name = "Customer_IdentifierName", ResourceType = typeof(Labels))]
        public string CustomerIdentifier { get; set; }

        [StringLength(50)]
        [Display(Name = "Customer_IdentifierDescription", ResourceType = typeof(Labels))]
        public string CustomerIdentifierDescription { get; set; }
               
        public int FKCustomerID { get; set; }

        [ForeignKey("FKCustomerID")]
        public virtual CATCustomerData CATCustomerData { get; set; }

        public DateTime? TCInsertTime { get; set; }

        public DateTime? TCLastUpdate { get; set; }

        public int? TCActive { get; set; }

     
    }
}
