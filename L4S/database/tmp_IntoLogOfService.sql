SELECT 
       l.[BatchID]
      ,l.[RecordID]
	  ,c.[FKCustomerID]
      ,2 ServiceID
      ,l.[UserID]
      ,dateadd(hour,convert(int,substring([DateOfRequest],len([DateOfRequest])-5,4)) ,convert(datetime, substring([DateOfRequest],0,12)+' '+substring([DateOfRequest],13,8),104))  DateOfRequest
      ,l.[RequestedURL]
      ,l.[RequestStatus]
      ,l.[BytesSent]
      ,l.[RequestTime]
      ,l.[UserIPAddress]
  FROM [dbo].[STLogImport] l, [dbo].[CATCustomerIdentifiers] c
  where 
  l.UserIPAddress = c.CustomerIdentifier


  select requestedurl, userIPaddress from STLogImport 
  where LEN(userIPaddress)>15

  USE [log4service]
GO

SELECT [PKCustomerIdentifiersID]
      ,[CustomerIdentifier]
      ,[CustomerIdentifierDescription]
      ,[FKCustomerID]
  FROM [dbo].[CATCustomerIdentifiers]
GO

select * from stlogimport where UserIPAddress like'%62.197.223.6%'

SELECT CASE 
        WHEN CHARINDEX(',', UserIPAddress, 0) = 0
            THEN UserIPAddress
        ELSE LEFT(UserIPAddress, CHARINDEX(',', UserIPAddress, 0)-1)
		END AS UserIPAddress1
    ,CASE 
        WHEN CHARINDEX(',', UserIPAddress, 0) = 0
            THEN ''
        ELSE RIGHT(UserIPAddress, CHARINDEX(',', REVERSE(UserIPAddress), 0)-1)
        END AS LastName
FROM stlogimport where len(UserIPAddress) >1

select d.PKCustomerDataID, i.CustomerIdentifier  from [dbo].CATCustomerData d, [dbo].CATCustomerIdentifiers i
where d.PKCustomerDataID = i.FKCustomerID