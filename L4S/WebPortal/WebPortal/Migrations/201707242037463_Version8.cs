namespace WebPortal.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Version8 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.CATCustomerIdentifiers SET CATCustomerData_PKCustomerDataID = FKCustomerID");
            Sql("UPDATE dbo.CATCustomerServices SET CATCustomerData_PKCustomerDataID = FKCustomerDataID");
            Sql("UPDATE dbo.CATCustomerServices SET CATServiceParameters_PKServiceID = FKServiceID");
            Sql("UPDATE dbo.CATServicePatterns SET CATServiceParameters_PKServiceID = FKServiceID");

            DropForeignKey("dbo.CATCustomerIdentifiers", "CATCustomerData_PKCustomerDataID", "dbo.CATCustomerData");
            DropForeignKey("dbo.CATCustomerServices", "CATCustomerData_PKCustomerDataID", "dbo.CATCustomerData");
            DropForeignKey("dbo.CATCustomerServices", "CATServiceParameters_PKServiceID", "dbo.CATServiceParameters");
            DropForeignKey("dbo.CATServicePatterns", "CATServiceParameters_PKServiceID", "dbo.CATServiceParameters");

            DropIndex("dbo.CATCustomerIdentifiers", new[] { "CATCustomerData_PKCustomerDataID" });
            DropIndex("dbo.CATCustomerServices", new[] { "CATCustomerData_PKCustomerDataID" });
            DropIndex("dbo.CATCustomerServices", new[] { "CATServiceParameters_PKServiceID" });
            DropIndex("dbo.CATServicePatterns", new[] { "CATServiceParameters_PKServiceID" });

            DropColumn("dbo.CATCustomerIdentifiers", "FKCustomerID");
            DropColumn("dbo.CATCustomerServices", "FKCustomerDataID");
            DropColumn("dbo.CATCustomerServices", "FKServiceID");
            DropColumn("dbo.CATServicePatterns", "FKServiceID");

            RenameColumn(table: "dbo.CATCustomerIdentifiers", name: "CATCustomerData_PKCustomerDataID", newName: "FKCustomerID");
            RenameColumn(table: "dbo.CATCustomerServices", name: "CATCustomerData_PKCustomerDataID", newName: "FKCustomerDataID");
            RenameColumn(table: "dbo.CATCustomerServices", name: "CATServiceParameters_PKServiceID", newName: "FKServiceID");
            RenameColumn(table: "dbo.CATServicePatterns", name: "CATServiceParameters_PKServiceID", newName: "FKServiceID");

            AlterColumn("dbo.CATCustomerIdentifiers", "FKCustomerID", c => c.Int(nullable: false));
            AlterColumn("dbo.CATCustomerServices", "FKCustomerDataID", c => c.Int(nullable: false));
            AlterColumn("dbo.CATCustomerServices", "FKCustomerDataID", c => c.Int(nullable: false));
            AlterColumn("dbo.CATCustomerServices", "FKServiceID", c => c.Int(nullable: false));
            AlterColumn("dbo.CATServicePatterns", "FKServiceID", c => c.Int(nullable: false));

            CreateIndex("dbo.CATCustomerIdentifiers", "FKCustomerID");
            CreateIndex("dbo.CATCustomerServices", "FKServiceID");
            CreateIndex("dbo.CATCustomerServices", "FKCustomerDataID");
            CreateIndex("dbo.CATServicePatterns", "FKServiceID");

            AddForeignKey("dbo.CATCustomerIdentifiers", "FKCustomerID", "dbo.CATCustomerData", "PKCustomerDataID", cascadeDelete: true);
            AddForeignKey("dbo.CATCustomerServices", "FKCustomerDataID", "dbo.CATCustomerData", "PKCustomerDataID", cascadeDelete: true);
            AddForeignKey("dbo.CATCustomerServices", "FKServiceID", "dbo.CATServiceParameters", "PKServiceID", cascadeDelete: true);
            AddForeignKey("dbo.CATServicePatterns", "FKServiceID", "dbo.CATServiceParameters", "PKServiceID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CATServicePatterns", "FKServiceID", "dbo.CATServiceParameters");
            DropForeignKey("dbo.CATCustomerServices", "FKServiceID", "dbo.CATServiceParameters");
            DropForeignKey("dbo.CATCustomerServices", "FKCustomerDataID", "dbo.CATCustomerData");
            DropForeignKey("dbo.CATCustomerIdentifiers", "FKCustomerID", "dbo.CATCustomerData");
            DropIndex("dbo.CATServicePatterns", new[] { "FKServiceID" });
            DropIndex("dbo.CATCustomerServices", new[] { "FKCustomerDataID" });
            DropIndex("dbo.CATCustomerServices", new[] { "FKServiceID" });
            DropIndex("dbo.CATCustomerIdentifiers", new[] { "FKCustomerID" });
            AlterColumn("dbo.CATServicePatterns", "FKServiceID", c => c.Int());
            AlterColumn("dbo.CATCustomerServices", "FKServiceID", c => c.Int());
            AlterColumn("dbo.CATCustomerServices", "FKCustomerDataID", c => c.Int());
            AlterColumn("dbo.CATCustomerServices", "FKCustomerDataID", c => c.Int());
            AlterColumn("dbo.CATCustomerIdentifiers", "FKCustomerID", c => c.Int());
            RenameColumn(table: "dbo.CATServicePatterns", name: "FKServiceID", newName: "CATServiceParameters_PKServiceID");
            RenameColumn(table: "dbo.CATCustomerServices", name: "FKServiceID", newName: "CATServiceParameters_PKServiceID");
            RenameColumn(table: "dbo.CATCustomerServices", name: "FKCustomerDataID", newName: "CATCustomerData_PKCustomerDataID");
            RenameColumn(table: "dbo.CATCustomerIdentifiers", name: "FKCustomerID", newName: "CATCustomerData_PKCustomerDataID");
            AddColumn("dbo.CATServicePatterns", "FKServiceID", c => c.Int(nullable: false));
            AddColumn("dbo.CATCustomerServices", "FKServiceID", c => c.Int(nullable: false));
            AddColumn("dbo.CATCustomerServices", "FKCustomerDataID", c => c.Int());
            AddColumn("dbo.CATCustomerIdentifiers", "FKCustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.CATServicePatterns", "CATServiceParameters_PKServiceID");
            CreateIndex("dbo.CATCustomerServices", "CATServiceParameters_PKServiceID");
            CreateIndex("dbo.CATCustomerServices", "CATCustomerData_PKCustomerDataID");
            CreateIndex("dbo.CATCustomerIdentifiers", "CATCustomerData_PKCustomerDataID");
            AddForeignKey("dbo.CATServicePatterns", "CATServiceParameters_PKServiceID", "dbo.CATServiceParameters", "PKServiceID");
            AddForeignKey("dbo.CATCustomerServices", "CATServiceParameters_PKServiceID", "dbo.CATServiceParameters", "PKServiceID");
            AddForeignKey("dbo.CATCustomerServices", "CATCustomerData_PKCustomerDataID", "dbo.CATCustomerData", "PKCustomerDataID");
            AddForeignKey("dbo.CATCustomerIdentifiers", "CATCustomerData_PKCustomerDataID", "dbo.CATCustomerData", "PKCustomerDataID");
        }
    }
}
