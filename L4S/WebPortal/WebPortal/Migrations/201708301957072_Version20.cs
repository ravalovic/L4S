namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version20 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CATCustomerData", "CompanyType", c => c.String(nullable: true, maxLength: 50));
            AlterColumn("dbo.CATCustomerData", "CompanyID", c => c.String(nullable: true, maxLength: 20));
            AlterColumn("dbo.CATCustomerData", "CompanyTAXID", c => c.String(nullable: true, maxLength: 20));
            AlterColumn("dbo.CATCustomerData", "IndividualTAXID", c => c.String(nullable: true, maxLength: 20));
            AlterColumn("dbo.CATCustomerData", "AddressStreet", c => c.String(nullable: true, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CATCustomerData", "AddressStreet", c => c.String(maxLength: 50));
            AlterColumn("dbo.CATCustomerData", "IndividualTAXID", c => c.String(maxLength: 20));
            AlterColumn("dbo.CATCustomerData", "CompanyTAXID", c => c.String(maxLength: 20));
            AlterColumn("dbo.CATCustomerData", "CompanyID", c => c.String(maxLength: 20));
            AlterColumn("dbo.CATCustomerData", "CompanyType", c => c.String(maxLength: 50));
        }
    }
}
