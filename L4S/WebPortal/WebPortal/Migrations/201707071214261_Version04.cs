namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version04 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CATVRMService");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CATVRMService",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BatchID = c.Int(nullable: false),
                        RecordID = c.Int(nullable: false),
                        CustomerID = c.Int(),
                        ServiceID = c.Int(),
                        UserID = c.String(maxLength: 50),
                        DateOfRequest = c.DateTime(),
                        RequestedURL = c.String(),
                        RequestStatus = c.String(maxLength: 5),
                        BytesSent = c.String(maxLength: 15),
                        RequestTime = c.String(maxLength: 15),
                        UserAgent = c.String(maxLength: 500),
                        UserIPAddress = c.String(maxLength: 1000),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
