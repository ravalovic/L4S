namespace WebPortal.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Version11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GAPAnalyze",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.DateTime(nullable: false, storeType: "date"),
                        FileNumber = c.Int(nullable: true),
                        FileName = c.String(maxLength: 1000),
                        TCInsertTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            AlterColumn("dbo.GAPAnalyze", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            DropTable("dbo.GAPAnalyze");
        }
    }
}
