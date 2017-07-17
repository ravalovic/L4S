using System.ComponentModel.DataAnnotations;

namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version05 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.CONFGeneralSettings",
                    c => new
                    {
                       ID = c.Int(nullable: false, identity: true),
                       ParamName = c.String(maxLength: 100),
                       ParamValue = c.String(maxLength: 100),
                       Note = c.String(maxLength: 100),
                       TCInsertTime = c.DateTime(),
                       TCLastUpdate = c.DateTime(),
                       TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
          DropTable("dbo.CONFGeneralSettings"); 
        }
    }
}
