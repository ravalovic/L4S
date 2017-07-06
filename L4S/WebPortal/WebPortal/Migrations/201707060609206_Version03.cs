namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.STInputFileDuplicity", "TCLastUpdate", c => c.DateTime());
            AddColumn("dbo.STInputFileDuplicity", "TCActive", c => c.Int());
            AddColumn("dbo.STInputFileInfo", "TCLastUpdate", c => c.DateTime());
            AddColumn("dbo.STInputFileInfo", "TCActive", c => c.Int());
            AlterColumn("dbo.STInputFileDuplicity", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.STInputFileDuplicity", "TCActive", c => c.Int(nullable: true, defaultValue: 0));
            AlterColumn("dbo.STInputFileInfo", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.STInputFileInfo", "TCActive", c => c.Int(nullable: true, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.STInputFileInfo", "TCActive");
            DropColumn("dbo.STInputFileInfo", "TCLastUpdate");
            DropColumn("dbo.STInputFileDuplicity", "TCActive");
            DropColumn("dbo.STInputFileDuplicity", "TCLastUpdate");
        }
    }
}
