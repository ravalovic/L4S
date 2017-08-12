namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ARCHInputFileDuplicity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OriginalId = c.Int(nullable: false),
                        FileName = c.String(maxLength: 200),
                        LinesInFile = c.Int(nullable: false),
                        Checksum = c.String(nullable: false, maxLength: 50),
                        LoadDateTime = c.DateTime(nullable: false),
                        InsertDateTime = c.DateTime(nullable: false),
                        OriFileName = c.String(maxLength: 200),
                        OriginalFileChecksum = c.String(nullable: false, maxLength: 50),
                        LoaderBatchID = c.Int(nullable: false),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ARCHInputFileInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 200),
                        Checksum = c.String(nullable: false, maxLength: 50),
                        LinesInFile = c.Int(nullable: false),
                        InsertDateTime = c.DateTime(nullable: false),
                        LoaderBatchID = c.Int(nullable: false),
                        LoadedRecord = c.Int(nullable: false),
                        OriFileName = c.String(maxLength: 200),
                        OriginalFileChecksum = c.String(nullable: false, maxLength: 50),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ARCHProcessStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StepName = c.String(maxLength: 100),
                        BatchID = c.String(maxLength: 100),
                        BatchRecordNum = c.Int(),
                        NumberOfService = c.Int(),
                        NumberOfCustomer = c.Int(),
                        NumberOfUnknownService = c.Int(),
                        NumberOfPreprocessDelete = c.Int(),
                        TCInsertTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ARCHProcessStatus");
            DropTable("dbo.ARCHInputFileInfo");
            DropTable("dbo.ARCHInputFileDuplicity");
        }
    }
}
