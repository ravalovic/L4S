namespace WebPortal.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Version10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CATSummaryInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(maxLength: 50),
                        StartBillingPeriod = c.DateTime(nullable: false),
                        StopBillingPeriod = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        NumberOfUnits = c.Long(nullable: false),
                        MeasureofUnits = c.String(maxLength: 50),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPriceWithoutVAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPriceWithVAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            AlterColumn("dbo.CATSummaryInvoice", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATSummaryInvoice", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATSummaryInvoice", "TCActive", c => c.Int(nullable: true, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropTable("dbo.CATSummaryInvoice");
        }
    }
}
