namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CATCustomerData", "Customer_TAXID", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.CATCustomerData", "Customer_VATID", c => c.String(maxLength: 20));
            DropColumn("dbo.CATCustomerData", "CompanyTAXID");
            DropColumn("dbo.CATCustomerData", "CompanyVATID");
            DropColumn("dbo.CATCustomerData", "IndividualTAXID");
            DropColumn("dbo.CATCustomerData", "IndividualVATID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CATCustomerData", "IndividualVATID", c => c.String(maxLength: 20));
            AddColumn("dbo.CATCustomerData", "IndividualTAXID", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.CATCustomerData", "CompanyVATID", c => c.String(maxLength: 20));
            AddColumn("dbo.CATCustomerData", "CompanyTAXID", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.CATCustomerData", "Customer_VATID");
            DropColumn("dbo.CATCustomerData", "Customer_TAXID");
        }
    }
}
