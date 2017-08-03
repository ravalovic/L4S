USE [log4service]
GO
delete from [dbo].[CATOwnerData]
go

INSERT INTO [dbo].[CATOwnerData]
           ([OwnerCompanyName]
           ,[OwnerCompanyType]
           ,[OwnerCompanyID]
           ,[OwnerCompanyTAXID]
           ,[OwnerCompanyVATID]
           ,[OwnerBankAccountIban]
           ,[OwnerAddressStreet]
           ,[OwnerAddressBuildingNumber]
           ,[OwnerAddressCity]
           ,[OwnerAddressZipCode]
           ,[OwnerAddressCountry]
           ,[OwnerResponsibleFirstName]
           ,[OwnerResponsiblelastName]
           ,[OwnerContactEmail]
           ,[OwnerContactMobile]
           ,[OwnerContactPhone]
           ,[OwnerContactWeb])
           
     VALUES
           ('Výskumný ústav geodézie a kartografie'
           ,'Príspevková organizácia'
           ,'00166251'
           ,'2020857080'
           ,'SK2020857080'
           ,'SK4581800000007000061016'
           ,'Chlumeckého'
           ,'4'
           ,'Bratislava'
           ,'826 62'
           ,'Slovenská republika'
           ,'Vladimír'
           ,'Raškovič'
           ,'vugk@skgeodesy.sk'
           ,''
           ,'+421 2 2081 6180'
           ,'http://www.vugk.sk')
          
GO