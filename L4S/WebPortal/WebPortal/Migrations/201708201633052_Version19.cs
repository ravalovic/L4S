namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "TCActive", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TCActive");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
