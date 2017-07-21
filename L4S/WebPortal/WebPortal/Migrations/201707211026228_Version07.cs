namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version07 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CATBillingInfo", "ServiceCode", c => c.String(maxLength: 50));
            AddColumn("dbo.CATBillingInfo", "ServiceName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CATBillingInfo", "ServiceName");
            DropColumn("dbo.CATBillingInfo", "ServiceCode");
        }
    }
}
