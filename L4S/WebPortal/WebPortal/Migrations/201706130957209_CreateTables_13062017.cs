namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables_13062017 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catalog_LogService",
                c => new
                    {
                        BatchID = c.Long(nullable: false, identity: true),
                        FileID = c.Long(nullable: false),
                        InsertDateTime = c.DateTime(nullable: false),
                        CustomerID = c.String(),
                        ServiceID = c.Int(),
                    })
                .PrimaryKey(t => t.BatchID);
            
            CreateTable(
                "dbo.Catalog_Service",
                c => new
                    {
                        ServiceID = c.Int(nullable: false, identity: true),
                        Identifier = c.String(maxLength: 500),
                        Description = c.String(maxLength: 250),
                        UserID = c.Int(),
                        InsertDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Stage_InputFileDuplicity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 100),
                        Checksum = c.String(maxLength: 100),
                        LoadDateTime = c.DateTime(nullable: false),
                        InsertDateTime = c.DateTime(nullable: false),
                        OriFileName = c.String(maxLength: 100),
                        LoaderBatchID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stage_InputFileInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 100),
                        Checksum = c.String(maxLength: 100),
                        InsertDateTime = c.DateTime(nullable: false),
                        LoaderBatchID = c.Int(nullable: false),
                        LoadedRecord = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stage_LogImport",
                c => new
                    {
                        BatchID = c.Long(nullable: false, identity: true),
                        OriginalFileName = c.String(maxLength: 100),
                        OriginalCheckSum = c.String(maxLength: 100),
                        PreProcessFileName = c.String(maxLength: 100),
                        NodeIPAddress = c.String(maxLength: 100),
                        UserID = c.String(maxLength: 100),
                        DateOfRequest = c.String(maxLength: 50),
                        RequestedURL = c.String(maxLength: 4000),
                        RequestStatus = c.String(maxLength: 5),
                        BytesSent = c.String(maxLength: 20),
                        RequestTime = c.String(maxLength: 20),
                        HttpReferer = c.String(maxLength: 100),
                        UserAgent = c.String(maxLength: 4000),
                        UserIPAddress = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.BatchID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Stage_LogImport");
            DropTable("dbo.Stage_InputFileInfo");
            DropTable("dbo.Stage_InputFileDuplicity");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Catalog_Service");
            DropTable("dbo.Catalog_LogService");
        }
    }
}
