namespace WebPortal.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Version06 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CATBillingInfo", "InvoiceNumber", c => c.String(maxLength: 50));
            AddColumn("dbo.CATBillingInfo", "ServiceID", c => c.Int(nullable: false));
            AddColumn("dbo.CATBillingInfo", "MeasureofUnits", c => c.String(maxLength: 50));
            AddColumn("dbo.CATCustomerIdentifiers", "TCInsertTime", c => c.DateTime());
            AddColumn("dbo.CATCustomerIdentifiers", "TCLastUpdate", c => c.DateTime());
            AddColumn("dbo.CATCustomerIdentifiers", "TCActive", c => c.Int());
            DropColumn("dbo.CATBillingInfo", "BankAccountIBAN");
            DropColumn("dbo.CATBillingInfo", "VariableSymbol");

            AlterColumn("dbo.CATCustomerIdentifiers", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerIdentifiers", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerIdentifiers", "TCActive", c => c.Int(nullable: true, defaultValue: 0));
        }
        
        public override void Down()
        {
            AddColumn("dbo.CATBillingInfo", "VariableSymbol", c => c.String());
            AddColumn("dbo.CATBillingInfo", "BankAccountIBAN", c => c.String(maxLength: 50));
            DropColumn("dbo.CATCustomerIdentifiers", "TCActive");
            DropColumn("dbo.CATCustomerIdentifiers", "TCLastUpdate");
            DropColumn("dbo.CATCustomerIdentifiers", "TCInsertTime");
            DropColumn("dbo.CATBillingInfo", "MeasureofUnits");
            DropColumn("dbo.CATBillingInfo", "ServiceID");
            DropColumn("dbo.CATBillingInfo", "InvoiceNumber");
        }
    }
}
