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


