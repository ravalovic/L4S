namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTable_add_constraints_13062017 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stage_InputFileDuplicity", "LoaderBatchID", c => c.Int(nullable: false, defaultValue: 0));
            AlterColumn("dbo.Stage_InputFileDuplicity", "InsertDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Stage_InputFileDuplicity", "LoadDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Stage_InputFileInfo", "InsertDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Stage_InputFileInfo", "LoaderBatchID", c => c.Int(nullable: false, defaultValue: -1 ));
            AlterColumn("dbo.Stage_InputFileInfo", "LoadedRecord", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stage_InputFileInfo", "LoadedRecord", c => c.Int(nullable: false,  defaultValue: 0));
            AlterColumn("dbo.Stage_InputFileInfo", "LoaderBatchID", c => c.Int(nullable: false, defaultValue: -1));
            AlterColumn("dbo.Stage_InputFileInfo", "InsertDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Stage_InputFileDuplicity", "LoaderBatchID", c => c.Int(nullable: false, defaultValue: 0));
            AlterColumn("dbo.Stage_InputFileDuplicity", "InsertDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Stage_InputFileDuplicity", "LoadDateTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
        }
    }
}
