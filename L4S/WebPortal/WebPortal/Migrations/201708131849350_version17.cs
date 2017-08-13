namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version17 : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.view_CustomerMontlyTotalInvoice");
            //AddColumn("dbo.view_InvoiceByDay", "MeasureOfUnits", c => c.String(maxLength: 12));
            //AddColumn("dbo.view_InvoiceByMonth", "MeasureOfUnits", c => c.String(maxLength: 12));
            //AlterColumn("dbo.view_CustomerMontlyTotalInvoice", "InvoiceNumber", c => c.String(nullable: false, maxLength: 50));
            //AddPrimaryKey("dbo.view_CustomerMontlyTotalInvoice", "InvoiceNumber");
            //DropColumn("dbo.view_CustomerMontlyTotalInvoice", "ID");
            //DropColumn("dbo.view_CustomerMontlyTotalInvoice", "UnitPrice");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.view_CustomerMontlyTotalInvoice", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            //AddColumn("dbo.view_CustomerMontlyTotalInvoice", "ID", c => c.Int(nullable: false, identity: true));
            //DropPrimaryKey("dbo.view_CustomerMontlyTotalInvoice");
            //AlterColumn("dbo.view_CustomerMontlyTotalInvoice", "InvoiceNumber", c => c.String(maxLength: 50));
            //DropColumn("dbo.view_InvoiceByMonth", "MeasureOfUnits");
            //DropColumn("dbo.view_InvoiceByDay", "MeasureOfUnits");
            //AddPrimaryKey("dbo.view_CustomerMontlyTotalInvoice", "ID");
        }
    }
}
