namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTables_13062017_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stage_InputFileDuplicity", "LoadDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Stage_InputFileDuplicity", "InsertDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stage_InputFileDuplicity", "InsertDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Stage_InputFileDuplicity", "LoadDateTime", c => c.DateTime(nullable: false));
        }
    }
}
