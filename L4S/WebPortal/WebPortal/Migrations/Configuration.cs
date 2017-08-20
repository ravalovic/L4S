using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using WebPortal.Models;
using System.Web;
using Microsoft.AspNet.Identity.Owin;


namespace WebPortal.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WebPortal.DataContexts.L4SDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebPortal.DataContexts.L4SDb context)
        {
            //  This method will be called after migrating to the latest version.


            // Create all Roles and init Admin Account           
            CreateRoles();
            CreateUserAdminAccount();
        }

        private bool CreateRoles()
        {
            var _db = new DataContexts.L4SDb();
            var roleStore = new RoleStore<IdentityRole>(_db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            foreach (string roleName in WebRoles.All)
            {
                var role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    var roleresult = roleManager.Create(role);
                }
            }
            return true;
        }

        private void CreateUserAdminAccount()
        {
            var _db = new DataContexts.L4SDb();
            var userStore = new UserStore<ApplicationUser>(_db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var adminUser = userManager.FindByName("admin");
            if (adminUser != null)
            {
                if (!(userManager.IsInRole(adminUser.Id, WebRoles.Admin)))
                    userManager.AddToRole(adminUser.Id, WebRoles.Admin);
            }
            else
            {
                var newAdmin = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@admin.sk",
                };
                userManager.Create(newAdmin, "123456"); //password is 123456
                userManager.AddToRole(newAdmin.Id, WebRoles.Admin);
            }          
        }
    }
}
