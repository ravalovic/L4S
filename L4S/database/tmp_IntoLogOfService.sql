SELECT top 20
       l.[BatchID]
      ,l.[RecordID]
	  --,c.[FKCustomerID]
      ,2
      ,l.[UserID]
      ,dateadd(hour,convert(int,substring([DateOfRequest],len([DateOfRequest])-5,4)) ,convert(datetime, substring([DateOfRequest],0,12)+' '+substring([DateOfRequest],13,8),104))  DateOfRequest
      ,l.[RequestedURL]
      ,l.[RequestStatus]
      ,l.[BytesSent]
      ,l.[RequestTime]
      ,l.[UserIPAddress]
  FROM [dbo].[STLogImport] l --, [dbo].[CATCustomerIdentifiers] c
 -- where 
  --   l.UserIPAddress = c.CustomerIdentifier


  select requestedurl, userIPaddress from STLogImport 
  where LEN(userIPaddress)>15