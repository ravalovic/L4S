namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTable_add_constraints_130620171 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stage_InputFileDuplicity", "LoaderBatchID", c => c.Int(nullable: false, defaultValue: 0));
            AlterColumn("dbo.Stage_InputFileDuplicity", "InsertDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Stage_InputFileDuplicity", "LoadDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Stage_InputFileInfo", "InsertDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Stage_InputFileInfo", "LoaderBatchID", c => c.Int(nullable: false, defaultValue: -1));
            AlterColumn("dbo.Stage_InputFileInfo", "LoadedRecord", c => c.Int(nullable: false, defaultValue: 0));
            DropColumn("dbo.Stage_InputFileDuplicity", "OriFileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stage_InputFileDuplicity", "OriFileName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Stage_InputFileDuplicity", "LoaderBatchID", c => c.Int(nullable: false, defaultValue: 0));
            AlterColumn("dbo.Stage_InputFileDuplicity", "InsertDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Stage_InputFileDuplicity", "LoadDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Stage_InputFileInfo", "InsertDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Stage_InputFileInfo", "LoaderBatchID", c => c.Int(nullable: false, defaultValue: -1));
            AlterColumn("dbo.Stage_InputFileInfo", "LoadedRecord", c => c.Int(nullable: false, defaultValue: 0));
        }
    }
}
