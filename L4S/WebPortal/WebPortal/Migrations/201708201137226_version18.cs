namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version18 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CONFGeneralSettings", "Note", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CONFGeneralSettings", "Note", c => c.String(maxLength: 100));
        }
    }
}
