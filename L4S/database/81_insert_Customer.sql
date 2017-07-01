USE [log4service]
GO

DELETE FROM [CATCustomerIdentifiers]
GO

DELETE FROM [CATCustomerData]
GO

DBCC CHECKIDENT ('CATCustomerData', RESEED, 0);


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
           ,'735698123'
           ,'Vedlajsia'
           ,'48/236' 
           ,'Piestany'
           ,'92101',
		   'Slovensko',
		   'lada@travnicek.com'
        )
GO
