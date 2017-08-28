namespace WebPortal.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; } 
        public string Email { get; set; }
        public string Phone { get; set; }


        public UserViewModel(ApplicationUser user)
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.Phone = user.PhoneNumber;
            this.UserFirstName = user.FirstName;
            this.UserLastName = user.LastName;

        }
    }
}