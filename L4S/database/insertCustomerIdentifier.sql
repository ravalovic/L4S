USE [log4service]
GO

INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('10.231.22.22, 85.248.49.200'
           ,'Viac IP'
           ,1)
GO



INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('62.197.223.6:62741, 62.197.192.36'
           ,'Viac IP Port'
           ,1)
GO

INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('178.162.209.232'
           ,'Jedna IP'
           ,1)
GO