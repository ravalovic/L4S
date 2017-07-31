namespace WebPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Version01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.ARCHCustomerDailyData",
                    c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateOfRequest = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        NumberOfRequest = c.Long(),
                        ReceivedBytes = c.Long(),
                        RequestedTime = c.Decimal(nullable:false, precision: 18, scale: 5),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                    "dbo.ARCHCustomerMonthlyData",
                    c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateOfRequest = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        NumberOfRequest = c.Long(),
                        ReceivedBytes = c.Long(),
                        RequestedTime = c.Decimal(nullable: false, precision: 18, scale: 5),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                    "dbo.ARCHLogsOfService",
                    c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BatchID = c.Int(),
                        RecordID = c.Int(),
                        CustomerID = c.Int(),
                        ServiceID = c.Int(),
                        UserID = c.String(maxLength: 50),
                        DateOfRequest = c.DateTime(),
                        RequestedURL = c.String(),
                        RequestStatus = c.String(maxLength: 10),
                        BytesSent = c.String(maxLength: 20),
                        RequestTime = c.String(maxLength: 20),
                        UserAgent = c.String(maxLength: 1000),
                        UserIPAddress = c.String(maxLength: 1000),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                    "dbo.CATCustomerDailyData",
                    c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateOfRequest = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        NumberOfRequest = c.Long(),
                        ReceivedBytes = c.Long(),
                        RequestedTime = c.Decimal(nullable: false, precision: 18, scale: 5),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                    "dbo.CATCustomerData",
                    c => new
                    {
                        PKCustomerDataID = c.Int(nullable: false, identity: true),
                        CustomerType = c.String(maxLength: 6),
                        CompanyName = c.String(maxLength: 100),
                        CompanyType = c.String(maxLength: 50),
                        CompanyID = c.String(maxLength: 20),
                        CompanyTAXID = c.String(maxLength: 20),
                        CompanyVATID = c.String(maxLength: 20),
                        IndividualTitle = c.String(maxLength: 10),
                        IndividualFirstName = c.String(maxLength: 50),
                        IndividualLastName = c.String(maxLength: 50),
                        IndividualID = c.String(maxLength: 20),
                        IndividualTAXID = c.String(maxLength: 20),
                        IndividualVATID = c.String(maxLength: 20),
                        BankAccountIBAN = c.String(maxLength: 50),
                        AddressStreet = c.String(maxLength: 50),
                        AddressBuildingNumber = c.String(nullable: false, maxLength: 20),
                        AddressCity = c.String(nullable: false, maxLength: 50),
                        AddressZipCode = c.String(nullable: false, maxLength: 6),
                        AddressCountry = c.String(maxLength: 50),
                        ContactEmail = c.String(maxLength: 50),
                        ContactMobile = c.String(maxLength: 50),
                        ContactPhone = c.String(maxLength: 50),
                        ContactWeb = c.String(maxLength: 100),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.PKCustomerDataID);

            CreateTable(
                    "dbo.CATCustomerIdentifiers",
                    c => new
                    {
                        PKCustomerIdentifiersID = c.Int(nullable: false, identity: true),
                        CustomerIdentifier = c.String(nullable: false, maxLength: 200),
                        CustomerIdentifierDescription = c.String(maxLength: 50),
                        FKCustomerID = c.Int(nullable: false),
                        CATCustomerData_PKCustomerDataID = c.Int(),
                    })
                .PrimaryKey(t => t.PKCustomerIdentifiersID)
                .ForeignKey("dbo.CATCustomerData", t => t.CATCustomerData_PKCustomerDataID)
                .Index(t => t.CATCustomerData_PKCustomerDataID);

            CreateTable(
                    "dbo.CATCustomerServices",
                    c => new
                    {
                        PKServiceCustomerIdentifiersID = c.Int(nullable: false, identity: true),
                        FKServiceID = c.Int(nullable: false),
                        ServiceName = c.String(nullable: false, maxLength: 100),
                        ServiceCode = c.String(nullable: false, maxLength: 50),
                        ServicePriceDiscount = c.Decimal(nullable: true, precision: 18, scale: 5),
                        ServiceNote = c.String(maxLength: 100),
                        FKCustomerDataID = c.Int(),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                        CATCustomerData_PKCustomerDataID = c.Int(),
                        CATServiceParameters_PKServiceID = c.Int(),
                    })
                .PrimaryKey(t => t.PKServiceCustomerIdentifiersID)
                .ForeignKey("dbo.CATCustomerData", t => t.CATCustomerData_PKCustomerDataID)
                .ForeignKey("dbo.CATServiceParameters", t => t.CATServiceParameters_PKServiceID)
                .Index(t => t.CATCustomerData_PKCustomerDataID)
                .Index(t => t.CATServiceParameters_PKServiceID);

            CreateTable(
                    "dbo.CATCustomerMonthlyData",
                    c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateOfRequest = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        NumberOfRequest = c.Long(),
                        ReceivedBytes = c.Long(),
                        RequestedTime = c.Decimal(nullable: false, precision: 18, scale: 5),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                    "dbo.CATChangeDetect",
                    c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TableName = c.String(maxLength: 50),
                        Status = c.Int(),
                        StatusName = c.String(maxLength: 100),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                    "dbo.CATLogsOfService",
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

            CreateTable(
                    "dbo.CATOwnerData",
                    c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OwnerCompanyName = c.String(maxLength: 100),
                        OwnerCompanyType = c.String(maxLength: 100),
                        OwnerCompanyID = c.String(maxLength: 100),
                        OwnerCompanyTAXID = c.String(maxLength: 100),
                        OwnerCompanyVATID = c.String(maxLength: 100),
                        OwnerBankAccountIban = c.String(maxLength: 50),
                        OwnerAddressStreet = c.String(maxLength: 100),
                        OwnerAddressBuildingNumber = c.String(maxLength: 100),
                        OwnerAddressCity = c.String(maxLength: 100),
                        OwnerAddressZipCode = c.String(maxLength: 12),
                        OwnerAddressCountry = c.String(maxLength: 100),
                        OwnerResponsibleFirstName = c.String(maxLength: 100),
                        OwnerResponsiblelastName = c.String(maxLength: 100),
                        OwnerContactEmail = c.String(maxLength: 100),
                        OwnerContactMobile = c.String(maxLength: 100),
                        OwnerContactPhone = c.String(maxLength: 100),
                        OwnerContactWeb = c.String(maxLength: 100),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                    "dbo.CATProcessStatus",
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

            CreateTable(
                    "dbo.CATServiceParameters",
                    c => new
                    {
                        PKServiceID = c.Int(nullable: false),
                        ServiceCode = c.String(nullable: false, maxLength: 50),
                        ServiceDescription = c.String(nullable: false, maxLength: 150),
                        ServiceBasicPrice = c.Decimal(nullable: false, precision: 18, scale: 5),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                    })
                .PrimaryKey(t => t.PKServiceID);

            CreateTable(
                    "dbo.CATServicePatterns",
                    c => new
                    {
                        PKServicePatternID = c.Int(nullable: false, identity: true),
                        PatternLike = c.String(nullable: false, maxLength: 2000),
                        PatternRegExp = c.String(maxLength: 2000),
                        PatternDescription = c.String(maxLength: 50),
                        FKServiceID = c.Int(nullable: false),
                        Entity = c.String(maxLength: 150),
                        Explanation = c.String(maxLength: 150),
                        DatSelectMethod = c.String(maxLength: 150),
                        TCInsertTime = c.DateTime(),
                        TCLastUpdate = c.DateTime(),
                        TCActive = c.Int(),
                        CATServiceParameters_PKServiceID = c.Int(),
                    })
                .PrimaryKey(t => t.PKServicePatternID)
                .ForeignKey("dbo.CATServiceParameters", t => t.CATServiceParameters_PKServiceID)
                .Index(t => t.CATServiceParameters_PKServiceID);

            CreateTable(
                    "dbo.CATUnknownService",
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
                    "dbo.STInputFileDuplicity",
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
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                    "dbo.STInputFileInfo",
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
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.STLogImport",
                    c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BatchID = c.Int(nullable: false),
                        RecordID = c.Int(nullable: false),
                        OriginalCheckSum = c.String(nullable: false, maxLength: 50),
                        NodeIPAddress = c.String(maxLength: 50),
                        UserID = c.String(maxLength: 50),
                        DateOfRequest = c.String(maxLength: 30),
                        RequestedURL = c.String(),
                        RequestStatus = c.String(maxLength: 5),
                        BytesSent = c.String(maxLength: 15),
                        RequestTime = c.String(maxLength: 15),
                        HttpRefferer = c.String(),
                        UserAgent = c.String(maxLength: 500),
                        UserIPAddress = c.String(maxLength: 1000),
                        CustomerID = c.Int(),
                        DatDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);

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

            //CreateTable(
            //    "dbo.view_DetailFromDaily",
            //    c => new
            //        {
            //            BatchID = c.Int(nullable: false),
            //            RecordID = c.Int(nullable: false),
            //            DateOfRequest = c.DateTime(),
            //            DayDate = c.DateTime(storeType: "date"),
            //            CustomerID = c.Int(),
            //            ServiceID = c.Int(),
            //            BytesSent = c.String(maxLength: 15),
            //            RequestTime = c.String(maxLength: 15),
            //            RequestedURL = c.String(),
            //            RequestStatus = c.String(maxLength: 5),
            //            UserIPAddress = c.String(maxLength: 1000),
            //        })
            //    .PrimaryKey(t => new { t.BatchID, t.RecordID });

            //CreateTable(
            //    "dbo.view_DetailFromMonthly",
            //    c => new
            //        {
            //            BatchID = c.Int(nullable: false),
            //            RecordID = c.Int(nullable: false),
            //            DateOfRequest = c.DateTime(),
            //            Monthdate = c.DateTime(),
            //            CustomerID = c.Int(),
            //            ServiceID = c.Int(),
            //            BytesSent = c.String(maxLength: 15),
            //            RequestTime = c.String(maxLength: 15),
            //            RequestedURL = c.String(),
            //            RequestStatus = c.String(maxLength: 5),
            //            UserIPAddress = c.String(maxLength: 1000),
            //        })
            //    .PrimaryKey(t => new { t.BatchID, t.RecordID });

            //CreateTable(
            //    "dbo.view_InvoiceByDay",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            DateOfRequest = c.DateTime(nullable: false),
            //            CustomerID = c.Int(nullable: false),
            //            ServiceID = c.Int(nullable: false),
            //            NumberOfRequest = c.Long(nullable: false),
            //            ReceivedBytes = c.Long(nullable: false),
            //            RequestedTime = c.Decimal(nullable: false, precision: 18, scale: 5),
            //            ServiceCode = c.String(maxLength: 50),
            //            ServiceName = c.String(maxLength: 150),
            //            BasicPriceWithoutVAT = c.Decimal(nullable: false, precision: 18, scale: 5),
            //            VAT = c.Decimal(nullable: false, precision: 18, scale: 5),
            //            BasicPriceWithVAT = c.Decimal(nullable: false, precision: 18, scale: 5),
            //        })
            //    .PrimaryKey(t => t.ID);

            //CreateTable(
            //    "dbo.view_InvoiceByMonth",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            DateOfRequest = c.DateTime(nullable: false),
            //            CustomerID = c.Int(nullable: false),
            //            ServiceID = c.Int(nullable: false),
            //            NumberOfRequest = c.Long(nullable: false),
            //            ReceivedBytes = c.Long(nullable: false),
            //            RequestedTime = c.Decimal(nullable: false, precision: 18, scale: 5),
            //            ServiceCode = c.String(maxLength: 50),
            //            ServiceName = c.String(maxLength: 150),
            //            BasicPriceWithoutVAT = c.Decimal(nullable: false, precision: 18, scale: 5),
            //            VAT = c.Decimal(nullable: false, precision: 18, scale: 5),
            //            BasicPriceWithVAT = c.Decimal(nullable: false, precision: 18, scale: 5),
            //        })
            //    .PrimaryKey(t => t.ID);
            Sql("EXEC('create view [dbo].[view_DetailFromDaily] as " +
                "select s.BatchID, s.RecordID, s.DateOfRequest, convert(date, s.DateOfRequest) DayDate, s.CustomerID, s.ServiceID, " +
                "s.BytesSent, s.RequestTime, s.RequestedURL, s.RequestStatus, s.UserIPAddress from CATLogsOfService s " +
                "where exists( select d.CustomerID from CATCustomerDailyData d " +
                "where s.CustomerID = d.CustomerID and s.ServiceID = d.ServiceID and convert(date, s.DateOfRequest) = d.DateOfRequest)')");

            Sql("EXEC('create view [dbo].[view_DetailFromMonthly] as " +
                "select s.BatchID, s.RecordID, s.DateOfRequest, DATEADD(month, DATEDIFF(month, 0, convert(date, s.DateofRequest)), 0) Monthdate, " +
                "s.CustomerID, s.ServiceID, s.BytesSent, s.RequestTime, s.RequestedURL, s.RequestStatus, s.UserIPAddress from CATLogsOfService s " +
                "where exists( select d.CustomerID from CATCustomerMonthlyData d " +
                "where s.CustomerID = d.CustomerID and s.ServiceID = d.ServiceID " +
                "and DATEADD(month, DATEDIFF(month, 0, convert(date, s.DateofRequest)), 0) = d.DateOfRequest)')");

            Sql("EXEC('create view [dbo].[view_InvoiceByMonth] as " +
                "select m.ID, m.DateOfRequest, m.CustomerID, m.ServiceID, m.NumberOfRequest, m.ReceivedBytes, m.RequestedTime, c.ServiceCode, " +
                "c.ServiceName, (p.ServiceBasicPrice * m.NumberOfRequest * c.ServicePriceDiscount) BasicPriceWithoutVAT, (p.ServiceBasicPrice * m.NumberOfRequest * 0.2) VAT" +
                ", (p.ServiceBasicPrice * m.NumberOfRequest * 1.2) BasicPriceWithVAT from CATCustomerMonthlyData m, CATCustomerServices c" +
                ", CATServiceParameters p where m.CustomerID = c.FKCustomerDataID and m.ServiceID = c.FKServiceID and c.FKServiceID = p.PKServiceID')");
            Sql("EXEC('create  view [dbo].[view_InvoiceByDay] as " +
                "select m.ID, m.DateOfRequest, m.CustomerID, m.ServiceID, m.NumberOfRequest, m.ReceivedBytes, m.RequestedTime, c.ServiceCode, " +
                "c.ServiceName, (p.ServiceBasicPrice * m.NumberOfRequest * c.ServicePriceDiscount) BasicPriceWithoutVAT, (p.ServiceBasicPrice * m.NumberOfRequest * 0.2) VAT, " +
                "(p.ServiceBasicPrice * m.NumberOfRequest * 1.2) BasicPriceWithVAT from CATCustomerDailyData m, CATCustomerServices c, CATServiceParameters p " +
                "where m.CustomerID = c.FKCustomerDataID and m.ServiceID = c.FKServiceID and c.FKServiceID = p.PKServiceID')");

            AlterColumn("dbo.STLogImport", "BatchID", c => c.Int(nullable: false, defaultValue: 0));
            AlterColumn("dbo.STLogImport", "OriginalCheckSum", c => c.String(nullable: false, defaultValue: "n/a"));

            AlterColumn("dbo.STInputFileInfo", "InsertDateTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.STInputFileInfo", "LoaderBatchID", c => c.Int(nullable: true, defaultValue: -1));
            AlterColumn("dbo.STInputFileInfo", "LinesInFile", c => c.Int(nullable: true, defaultValue: -1));
            AlterColumn("dbo.STInputFileInfo", "LoadedRecord", c => c.Int(nullable: true, defaultValue: -1));

            AlterColumn("dbo.STInputFileDuplicity", "OriginalId", c => c.Int(nullable: true, defaultValue: -1));
            AlterColumn("dbo.STInputFileDuplicity", "LinesInFile", c => c.Int(nullable: true, defaultValue: -1));
            AlterColumn("dbo.STInputFileDuplicity", "InsertDateTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.STInputFileDuplicity", "LoaderBatchID", c => c.Int(nullable: true, defaultValue: -1));
            AlterColumn("dbo.STInputFileDuplicity", "LoadDateTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));

            AlterColumn("dbo.CATServiceParameters", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATServiceParameters", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATServiceParameters", "TCActive", c => c.Int(nullable: true, defaultValue: 0));

            AlterColumn("dbo.CATLogsOfService", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATLogsOfService", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATLogsOfService", "TCActive", c => c.Int(nullable: true, defaultValue: 0));

            AlterColumn("dbo.CATCustomerDailyData", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerDailyData", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerDailyData", "TCActive", c => c.Int(nullable: true, defaultValue: 0));
            AlterColumn("dbo.CATCustomerDailyData", "NumberOfRequest", c => c.Long(nullable: true, defaultValue: 0));
            AlterColumn("dbo.CATCustomerDailyData", "ReceivedBytes", c => c.Long(nullable: true, defaultValue: 0));
            AlterColumn("dbo.CATCustomerDailyData", "RequestedTime", c => c.Decimal(nullable: true, defaultValue: 0));

            AlterColumn("dbo.CATCustomerMonthlyData", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerMonthlyData", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerMonthlyData", "TCActive", c => c.Int(nullable: true, defaultValue: 0));
            AlterColumn("dbo.CATCustomerMonthlyData", "NumberOfRequest", c => c.Long(nullable: true, defaultValue: 0));
            AlterColumn("dbo.CATCustomerMonthlyData", "ReceivedBytes", c => c.Long(nullable: true, defaultValue: 0));
            AlterColumn("dbo.CATCustomerMonthlyData", "RequestedTime", c => c.Decimal(nullable: true, defaultValue: 0));

            AlterColumn("dbo.CATProcessStatus", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));

            AlterColumn("dbo.CATChangeDetect", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATChangeDetect", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));

            AlterColumn("dbo.CATCustomerData", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerData", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerData", "TCActive", c => c.Int(nullable: true, defaultValue: 0));

            AlterColumn("dbo.CATCustomerServices", "ServicePriceDiscount", c => c.Decimal(nullable: true, precision: 18, scale: 5, defaultValue: 1));
            AlterColumn("dbo.CATCustomerServices", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerServices", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATCustomerServices", "TCActive", c => c.Int(nullable: true, defaultValue: 0));

            AlterColumn("dbo.CATServicePatterns", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATServicePatterns", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATServicePatterns", "TCActive", c => c.Int(nullable: true, defaultValue: 0));

            AlterColumn("dbo.ARCHCustomerDailyData", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.ARCHCustomerDailyData", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.ARCHCustomerDailyData", "TCActive", c => c.Int(nullable: true, defaultValue: 0));

            AlterColumn("dbo.ARCHCustomerMonthlyData", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.ARCHCustomerMonthlyData", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.ARCHCustomerMonthlyData", "TCActive", c => c.Int(nullable: true, defaultValue: 0));

            AlterColumn("dbo.CATOwnerData", "TCInsertTime", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATOwnerData", "TCLastUpdate", c => c.DateTime(nullable: true, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.CATOwnerData", "TCActive", c => c.Int(nullable: true, defaultValue: 0));
        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CATServicePatterns", "CATServiceParameters_PKServiceID", "dbo.CATServiceParameters");
            DropForeignKey("dbo.CATCustomerServices", "CATServiceParameters_PKServiceID", "dbo.CATServiceParameters");
            DropForeignKey("dbo.CATCustomerServices", "CATCustomerData_PKCustomerDataID", "dbo.CATCustomerData");
            DropForeignKey("dbo.CATCustomerIdentifiers", "CATCustomerData_PKCustomerDataID", "dbo.CATCustomerData");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.CATServicePatterns", new[] { "CATServiceParameters_PKServiceID" });
            DropIndex("dbo.CATCustomerServices", new[] { "CATServiceParameters_PKServiceID" });
            DropIndex("dbo.CATCustomerServices", new[] { "CATCustomerData_PKCustomerDataID" });
            DropIndex("dbo.CATCustomerIdentifiers", new[] { "CATCustomerData_PKCustomerDataID" });
            DropTable("dbo.view_InvoiceByMonth");
            DropTable("dbo.view_InvoiceByDay");
            DropTable("dbo.view_DetailFromMonthly");
            DropTable("dbo.view_DetailFromDaily");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.STLogImport");
            DropTable("dbo.STInputFileInfo");
            DropTable("dbo.STInputFileDuplicity");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CATVRMService");
            DropTable("dbo.CATUnknownService");
            DropTable("dbo.CATServicePatterns");
            DropTable("dbo.CATServiceParameters");
            DropTable("dbo.CATProcessStatus");
            DropTable("dbo.CATOwnerData");
            DropTable("dbo.CATLogsOfService");
            DropTable("dbo.CATChangeDetect");
            DropTable("dbo.CATCustomerMonthlyData");
            DropTable("dbo.CATCustomerServices");
            DropTable("dbo.CATCustomerIdentifiers");
            DropTable("dbo.CATCustomerData");
            DropTable("dbo.CATCustomerDailyData");
            DropTable("dbo.ARCHLogsOfService");
            DropTable("dbo.ARCHCustomerMonthlyData");
            DropTable("dbo.ARCHCustomerDailyData");
        }
    }
}
