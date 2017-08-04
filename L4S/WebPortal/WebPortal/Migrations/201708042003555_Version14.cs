namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CATCustomerServiceDetailInvoice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(maxLength: 50),
                        StartBillingPeriod = c.DateTime(nullable: false),
                        StopBillingPeriod = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        CustomerIdentification = c.String(maxLength: 20),
                        CustomerName = c.String(maxLength: 100),
                        ServiceID = c.Int(nullable: false),
                        ServiceCode = c.String(maxLength: 50),
                        ServiceDescription = c.String(maxLength: 150),
                        NumberOfRequest = c.Long(nullable: false),
                        ReceivedBytes = c.Decimal(nullable: false, precision: 18, scale: 5),
                        RequestedTime = c.Decimal(nullable: false, precision: 18, scale: 5),
                        MeasureofUnits = c.String(maxLength: 50),
                        CustomerServiceCode = c.String(maxLength: 50),
                        CustomerServicename = c.String(maxLength: 150),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 5),
                        TotalPriceWithoutVAT = c.Decimal(nullable: false, precision: 18, scale: 5),
                        VAT = c.Decimal(nullable: false, precision: 18, scale: 5),
                        TotalPriceWithVAT = c.Decimal(nullable: false, precision: 18, scale: 5),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                        
                })
                .PrimaryKey(t => t.ID);
            AlterColumn("dbo.CATCustomerServiceDetailInvoice", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerServiceDetailInvoice", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerServiceDetailInvoice", "TCActive", c => c.Int(nullable: true, defaultValue: 0));
        }
        
        public override void Down()
        {
            
            DropTable("dbo.CATCustomerServiceDetailInvoice");
        }
    }
}
