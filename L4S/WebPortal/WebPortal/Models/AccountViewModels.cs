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
        [Required]
        [EmailAddress]
        [Display(Name = "Customer_IndividualFirstName", ResourceType = typeof(Labels))]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Customer_IndividualLastsName", ResourceType = typeof(Labels))]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Labels))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "LoginName", ResourceType = typeof(Labels))]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musí mať aspoň {2} znaky", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Labels))]
        public string Password { get; set; }

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
            var user = userManager.FindByName(this.UserName);

            var roleStore = new RoleStore<IdentityRole>(_db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

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
            UserRoles= list;
        }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
