USE [log4service]
GO
DBCC CHECKIDENT ('CATCustomerData', RESEED,0);

DELETE FROM [CATCustomerIdentifiers]
GO

DELETE FROM [CATCustomerData]
GO




INSERT INTO [dbo].[CATCustomerData]
           ([CustomerType]
           ,[CompanyName]
           ,[CompanyType]
           ,[CompanyID]
           ,[AddressStreet]
           ,[AddressBuildingNumber]
           ,[AddressCity]
           ,[AddressZipCode]
           ,[AddressCountry]
           )
     VALUES
           ('PO'
           ,'TestComp'
           ,'s.r.o.'
           ,'48478269'
           ,'Hlavna'
           ,'45/15'
           ,'Vrbove' 
           ,'92203'
           ,'Slovensko'
        )
GO

INSERT INTO [dbo].[CATCustomerData]
           ([CustomerType]
           ,[IndividualTitle]
           ,[IndividualFirstName]
           ,[IndividualLastName]
           ,[IndividualID]
           ,[AddressStreet]
           ,[AddressBuildingNumber]
           ,[AddressCity]
           ,[AddressZipCode]
           ,[AddressCountry]
           ,[ContactEmail]
           )
     VALUES
           ('FO'
           ,'Ing.'
           ,'Beda'
           ,'Travnicek'
           ,'735698123'
           ,'Vedlajsia'
           ,'48/236' 
           ,'Piestany'
           ,'92101',
		   'Slovensko',
		   'beda@travnicek.com'
        )
GO
USE [log4service]
GO

INSERT INTO [dbo].[CATCustomerData]
           ([CustomerType]
           ,[CompanyName]
           ,[CompanyType]
           ,[CompanyID]
           ,[AddressStreet]
           ,[AddressBuildingNumber]
           ,[AddressCity]
           ,[AddressZipCode]
           ,[AddressCountry]
           )
     VALUES
           ('PO'
           ,'TestComp2'
           ,'s.r.o.'
           ,'48478269'
           ,'Benovskeho'
           ,'145/15'
           ,'Vrbove' 
           ,'92203'
           ,'Slovensko'
        )
GO

INSERT INTO [dbo].[CATCustomerData]
           ([CustomerType]
           ,[IndividualTitle]
           ,[IndividualFirstName]
           ,[IndividualLastName]
           ,[IndividualID]
           ,[AddressStreet]
           ,[AddressBuildingNumber]
           ,[AddressCity]
           ,[AddressZipCode]
           ,[AddressCountry]
           ,[ContactEmail]
           )
     VALUES
           ('FO'
           ,'Ing.'
           ,'Lada'
           ,'Travnicek'
           ,'735698122'
           ,'Vedlajsia'
           ,'48/236' 
           ,'Piestany'
           ,'92101',
		   'Slovensko',
		   'lada@travnicek.com'
        )
GO

USE [log4service]
GO
DELETE FROM [CATCustomerIdentifiers]
GO

INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('10.231.22.22'
           ,'1 IP'
           ,1)
GO

INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('62.197.223.6 '
           ,'2 IP'
           ,1)
GO

INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('178.162.209.232'
           ,'3 IP'
           ,1)
GO
INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('85.248.49.200'
           ,'3 IP'
           ,2)
GO
INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('62.197.192.36'
           ,'4 IP'
           ,2)
GO 
 
INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('90.182.212.193'
           ,'1 IP'
           ,2)
GO  
INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('213.81.206.132'
           ,'2 IP'
           ,2)
GO  
INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('194.1.227.251'
           ,'4 IP'
           ,3)
GO 

INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('62.152.232.34'
           ,'4 IP'
           ,3)
GO 

INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('46.150.193.174'
           ,'5 IP'
           ,3)
GO 

INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('81.171.75.157'
           ,'6 IP'
           ,3)
GO 
INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('195.28.70.59'
           ,'7 IP'
           ,4)
GO 
INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('81.171.75.157'
           ,'8 IP'
           ,4)
GO 

INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('178.18.75.51'
           ,'9 IP'
           ,4)
GO 
INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID])
     VALUES
           ('178.40.76.7'
           ,'10 IP'
           ,4)
GO 