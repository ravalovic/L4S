namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CATBillingInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartBillingPeriod = c.DateTime(nullable: false),
                        StopBillingPeriod = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        NumberOfRequest = c.Long(nullable: false),
                        ReceivedBytes = c.Long(nullable: false),
                        RequestedTime = c.Decimal(nullable: false, precision: 18, scale: 5),
                        BankAccountIBAN = c.String(maxLength: 50),
                        VariableSymbol = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 5),
                        TotalPriceWithoutVAT = c.Decimal(nullable: false, precision: 18, scale: 5),
                        VAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPriceWithVAT = c.Decimal(nullable: false, precision: 18, scale: 5),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
           
            AlterColumn("dbo.CATBillingInfo", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATBillingInfo", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATBillingInfo", "TCActive", c => c.Int(nullable: true, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropTable("dbo.CATBillingInfo");
        }
    }
}
