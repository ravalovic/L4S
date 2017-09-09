namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CATInvoiceByDay",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: false),
                        DateOfRequest = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        CustomerIdentification = c.String(maxLength: 20),
                        CustomerName = c.String(maxLength: 100),
                        ServiceID = c.Int(nullable: false),
                        ServiceCode = c.String(maxLength: 50),
                        ServiceDescription = c.String(maxLength: 150),
                        NumberOfRequest = c.Long(nullable: false),
                        ReceivedBytes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RequestedTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerServiceCode = c.String(maxLength: 50),
                        CustomerServicename = c.String(maxLength: 150),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MeasureOfUnits = c.String(maxLength: 12),
                        BasicPriceWithoutVAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BasicPriceWithVAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CATInvoiceByMonth",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: false),
                        DateOfRequest = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        CustomerIdentification = c.String(maxLength: 20),
                        CustomerName = c.String(maxLength: 100),
                        ServiceID = c.Int(nullable: false),
                        ServiceCode = c.String(maxLength: 50),
                        ServiceDescription = c.String(maxLength: 150),
                        NumberOfRequest = c.Long(nullable: false),
                        ReceivedBytes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RequestedTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerServiceCode = c.String(maxLength: 50),
                        CustomerServicename = c.String(maxLength: 150),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MeasureOfUnits = c.String(maxLength: 12),
                        BasicPriceWithoutVAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BasicPriceWithVAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            AlterColumn("dbo.CATInvoiceByMonth", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATInvoiceByMonth", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATInvoiceByMonth", "TCActive", c => c.Int(nullable: true, defaultValue: 0));
            AlterColumn("dbo.CATInvoiceByDay", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATInvoiceByDay", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATInvoiceByDay", "TCActive", c => c.Int(nullable: true, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropTable("dbo.CATInvoiceByMonth");
            DropTable("dbo.CATInvoiceByDay");
        }
    }
}
