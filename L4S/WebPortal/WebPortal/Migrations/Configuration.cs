namespace WebPortal.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebPortal.DataContexts.L4SDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebPortal.DataContexts.L4SDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            var hasher = new PasswordHasher();
            context.Users.AddOrUpdate(
              p => p.UserName,
              new Models.ApplicationUser {Email="admin@admin.sk", UserName = "admin", PasswordHash = hasher.HashPassword("admin") }
            );

            context.SaveChanges();
        }
    }
}
