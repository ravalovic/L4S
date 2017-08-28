namespace WebPortal.Models
{
    public class DeleteModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Message { 
            get { return "Naozaj vymazať " + Name + "?"; }
        }
        public DeleteModel(string Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        public DeleteModel()
        {
            this.Id = "0";
            this.Name = "";
        }

        public DeleteModel(int Id, string Name)
        {
            this.Id = Id.ToString();
            this.Name = Name;
        }
    }
}