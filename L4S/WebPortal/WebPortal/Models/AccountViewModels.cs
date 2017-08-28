using Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;


namespace WebPortal.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessageResourceName = "ErrorMessage_Email", ErrorMessageResourceType = typeof(Labels))]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessageResourceName = "ErrorMessage_Email", ErrorMessageResourceType = typeof(Labels))]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "LoginName", ResourceType = typeof(Labels))]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(Labels))]
        public string Password { get; set; }

        [Display(Name = "Remember", ResourceType = typeof(Labels))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public RegisterViewModel() { }
        public RegisterViewModel(ApplicationUser user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            ReadRoles();
        }

        public string Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Labels))]
        [Display(Name = "Customer_IndividualFirstName", ResourceType = typeof(Labels))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Labels))]      
        [Display(Name = "Customer_IndividualLastName", ResourceType = typeof(Labels))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceName = "ErrorMessage_Email", ErrorMessageResourceType = typeof(Labels))]        
        [Display(Name = "Email", ResourceType = typeof(Labels))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Labels))]
        [Display(Name = "LoginName", ResourceType = typeof(Labels))]
        public string UserName { get; set; }
        
        [Display(Name = "OwnerContactPhone", ResourceType = typeof(Labels))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Labels))]
        [StringLength(100, ErrorMessage = "{0} musí mať aspoň {2} znaky", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Labels))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Labels))]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm", ResourceType = typeof(Labels))]
        [Compare("Password", ErrorMessage = "Heslo a potvrdené heslo musí byť rovnaké.")]
        public string ConfirmPassword { get; set; }

        public Dictionary<string,bool> UserRoles { get; set; }
        
        
        public void ReadRoles()
        {
            DataContexts.L4SDb _db = new DataContexts.L4SDb();
            Dictionary<string, bool> list = new Dictionary<string, bool>();

            var userStore = new UserStore<ApplicationUser>(_db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(_db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (this.UserName != null)
            {
                var user = userManager.FindByName(this.UserName);

                if (user == null)
                {
                    foreach (var item in roleManager.Roles)
                    {
                        list.Add(item.Name, false);
                    }
                }
                else
                {
                    foreach (var item in roleManager.Roles)
                    {
                        list.Add(item.Name, userManager.IsInRole(user.Id, item.Name));
                    }
                }
              
            }
            else
            {
                foreach (var item in roleManager.Roles)
                {
                    list.Add(item.Name, false);
                }
            }
            UserRoles = list;
        }
    }

    public class ResetPasswordViewModel
    {

        public string Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Labels))]
        [StringLength(100, ErrorMessage = "{0} musí mať aspoň {2} znaky", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Labels))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Labels))]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm", ResourceType = typeof(Labels))]
        [Compare("Password", ErrorMessage = "Heslo a potvrdené heslo musí byť rovnaké.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = "ErrorMessage_Email", ErrorMessageResourceType = typeof(Labels))]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
