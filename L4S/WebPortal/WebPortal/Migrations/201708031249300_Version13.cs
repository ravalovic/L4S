namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version13 : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.view_InvoiceByMonth", "ReceivedBytes", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
           // AlterColumn("dbo.view_InvoiceByMonth", "ReceivedBytes", c => c.Long(nullable: false));
        }
    }
}
