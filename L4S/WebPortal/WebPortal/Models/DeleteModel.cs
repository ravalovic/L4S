using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.Models
{
    public class DeleteModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { 
            get { return "Naozaj vymazať " + Name + "?"; }
        }
        public DeleteModel(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        public DeleteModel()
        {
            this.Id = 0;
            this.Name = "";
        }
    }
}