namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTable_add_constraints_13062017_03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stage_InputFileDuplicity", "OriFileName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stage_InputFileDuplicity", "OriFileName");
        }
    }
}
