using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4S.Entities
{
    public class Web_Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Max. 50 znakov/characters.")]
        [Display(Name = "Názov firmy")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Max. 50 znakov/characters.")]
        public string Street { get; set; }

        [StringLength(50, ErrorMessage = "Max. 50 znakov/characters.")]
        public string City { get; set; }

        [Display(Name = "PSČ")]
        [StringLength(6, ErrorMessage = "Max. 6 znakov/characters.")]
        public string PSC { get; set; }

        [StringLength(50, ErrorMessage = "Max. 50 znakov/characters.")]
        [Display(Name = "Štát")]
        public string State { get; set; }

        [Display(Name = "E-mail")]
        public string Mail { get; set; }

        [Display(Name = "Tel.")]
        public string Tel { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "IČO")]
        public string ICO { get; set; }

        [Display(Name = "DIČ")]
        public string DIC { get; set; }

        [Display(Name = "IČ DPH")]
        public string ICDPH { get; set; }
        
        public bool Active { get; set; }

        public DateTime LastChange { get; set; }
                
    }
}
