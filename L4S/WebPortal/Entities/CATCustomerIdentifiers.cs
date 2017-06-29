using System;
using System.ComponentModel.DataAnnotations;


namespace Entities
{

    public partial class CATCustomerIdentifiers
    {
        [Key]
        public int PKCustomerIdentifiersID { get; set; }

        [Required]
        [StringLength(200)]
        public string CustomerIdentifier { get; set; }

        [StringLength(50)]
        public string CustomerIdentifierDescription { get; set; }

        public int FKCustomerID { get; set; }

        public virtual CATCustomerData CATCustomerData { get; set; }
    }
}
