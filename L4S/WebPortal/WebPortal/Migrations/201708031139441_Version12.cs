namespace WebPortal.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Version12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CATOwnerData", "OwnerCompanyName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerCompanyType", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerCompanyID", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerCompanyTAXID", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerAddressStreet", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerAddressBuildingNumber", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerAddressCity", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerAddressZipCode", c => c.String(nullable: false, maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CATOwnerData", "OwnerAddressZipCode", c => c.String(maxLength: 12));
            AlterColumn("dbo.CATOwnerData", "OwnerAddressCity", c => c.String(maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerAddressBuildingNumber", c => c.String(maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerAddressStreet", c => c.String(maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerCompanyTAXID", c => c.String(maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerCompanyID", c => c.String(maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerCompanyType", c => c.String(maxLength: 100));
            AlterColumn("dbo.CATOwnerData", "OwnerCompanyName", c => c.String(maxLength: 100));
        }
    }
}
