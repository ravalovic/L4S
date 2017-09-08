namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version22 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CATCustomerData", "CompanyType", c => c.String(maxLength: 50));
            AlterColumn("dbo.CATCustomerData", "CompanyID", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CATCustomerData", "CompanyID", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CATCustomerData", "CompanyType", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
