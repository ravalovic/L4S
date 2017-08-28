using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebPortal.Models;
using WebPortal.DataContexts;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;
using System.Net;

namespace WebPortal.Controllers
{
   
    [Authorize] //!!! important only Authorize users can call this controller
    public class AccountController : Controller
    {
        private L4SDb _db = new L4SDb();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ActionResult UserList()
        {
            //show errors
            if (TempData["ModelState"] != null && !ModelState.Equals(TempData["ModelState"]))
                ModelState.Merge((ModelStateDictionary)TempData["ModelState"]);
            //show ok message
            if (TempData["ModelStateOk"] != null) ViewBag.message = TempData["ModelStateOk"];
                       
            var userprofiles = _db.Users.Where(p=>p.TCActive!=99).ToList();

            List<UserViewModel> users = new List<UserViewModel>();
            foreach (ApplicationUser user in userprofiles) users.Add(new UserViewModel(user));
            
            return View(users);
        }

        [AllowAnonymous]
        public ActionResult LoginPartial(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            LoginViewModel model = new LoginViewModel();
            return PartialView("_LoginForm", model);
        }
   
        
        
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

      
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");               
                case SignInStatus.Failure:
                default:
                   // ModelState.AddModelError("", "Invalid login attempt.");
                    // return View(model); 
                    return RedirectToAction("Index", "Home", new { LoginError=true });
            }
        }

        
        // GET: /Account/Register
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.ReadRoles();           
            return PartialView("_Register", model);
        }

        
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, FirstName=model.FirstName, LastName=model.LastName, PhoneNumber=model.PhoneNumber };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {                   
                    var userStore = new UserStore<ApplicationUser>(_db);
                    var userManager = new UserManager<ApplicationUser>(userStore);

                    foreach (var item in model.UserRoles)
                    {
                        if (item.Value == true)
                        {
                            result = userManager.AddToRole(user.Id, item.Key);
                            if (!result.Succeeded)
                            {
                                AddErrors(result);
                            }
                        }
                    }       
                }
                else
                {
                    AddErrors(result);
                }                
            }
                      
            if (ModelState.IsValid)
            {
                TempData["ModelStateOk"] = "Používateľ " + model.UserName + " bol úspešne vytvorený.";
            }
            else
            {
                TempData["ModelState"] = ModelState;
            }
            return RedirectToAction("UserList");
        }

        // GET: Account/Password  change password
        public ActionResult Password(string id)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Id = id;
            return PartialView("_ChangePassword");
        }


        // POST: /Account/Password   change password Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Password(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = UserManager.GeneratePasswordResetToken(model.Id);
                var result = UserManager.ResetPassword(model.Id, token, model.Password);

                if (result.Succeeded)
                {
                    TempData["ModelStateOk"] = "Heslo bolo zmenené.";                                   
                }
                else
                {
                    AddErrors(result);
                }
            }

            TempData["ModelState"] = ModelState;            
            return RedirectToAction("UserList");
        }

        public ActionResult Edit(string id)
        {
            ApplicationUser user = _db.Users.Find(id);
            RegisterViewModel model = new RegisterViewModel(user);

            //fake password to be sure ModelState is valid
            model.Password = "fake123";
            model.ConfirmPassword= "fake123";

            return PartialView("_Edit", model);
        }

        //
        // POST: /Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Users.Where(p => p.Id == model.Id).FirstOrDefault();
                user.PhoneNumber = model.PhoneNumber;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                
                IdentityResult result = UserManager.Update(user);
               
                var userStore = new UserStore<ApplicationUser>(_db);
                var userManager = new UserManager<ApplicationUser>(userStore);

                if (result.Succeeded) //if user details OK than update roles
                {
                    foreach (var rola in model.UserRoles)
                    {
                        result = new IdentityResult();
                        if (rola.Value == true)
                        {
                            //add to new role
                            if (!userManager.IsInRole(user.Id, rola.Key))
                            {
                                result = userManager.AddToRole(user.Id, rola.Key);
                            }
                        }
                        else
                        {
                            //remove role
                            if (userManager.IsInRole(user.Id, rola.Key))
                            {
                                result = userManager.RemoveFromRole(user.Id, rola.Key);
                            }
                        }
                        if (!result.Succeeded)
                        {
                            AddErrors(result);
                        }
                    }
                }
                else // update user error
                {
                    AddErrors(result);
                }
            }

            if (ModelState.IsValid) { TempData["ModelStateOk"] = "Údaje boli zmenené."; } //return ok
            
            TempData["ModelState"] = ModelState;

            return RedirectToAction("UserList");
        }


 
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = _db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            DeleteModel model = new DeleteModel(user.Id,
                "používateľa "+user.UserName+": "+user.FirstName + ' ' + user.LastName);
            return PartialView("_deleteModal", model);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = UserManager.Users.Where(p => p.Id == id).FirstOrDefault();
            user.TCActive = 99;
            IdentityResult result = UserManager.Update(user);

            if (!result.Succeeded)
            {
                AddErrors(result);
                TempData["ModelState"] = ModelState;
            }
            else
            { TempData["ModelStateOk"] = "Používateľ bol zrušený."; } //return ok
           
            return RedirectToAction("UserList");
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}