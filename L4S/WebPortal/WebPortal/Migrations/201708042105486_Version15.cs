namespace WebPortal.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Version15 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.CATCustomerServiceDetailInvoice", "TCInsertTime", c => c.DateTime());
            //AddColumn("dbo.CATCustomerServiceDetailInvoice", "TCLastUpdate", c => c.DateTime());
            //AlterColumn("dbo.CATCustomerServiceDetailInvoice", "TCActive", c => c.Int());
            //DropColumn("dbo.view_CustomerMontlyTotalInvoice", "CustomerServiceCode");
            //DropColumn("dbo.view_CustomerMontlyTotalInvoice", "CustomerServicename");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.view_CustomerMontlyTotalInvoice", "CustomerServicename", c => c.String(maxLength: 150));
            //AddColumn("dbo.view_CustomerMontlyTotalInvoice", "CustomerServiceCode", c => c.String(maxLength: 50));
            //AlterColumn("dbo.CATCustomerServiceDetailInvoice", "TCActive", c => c.Int(nullable: false));
            //DropColumn("dbo.CATCustomerServiceDetailInvoice", "TCLastUpdate");
            //DropColumn("dbo.CATCustomerServiceDetailInvoice", "TCInsertTime");
        }
    }
}
