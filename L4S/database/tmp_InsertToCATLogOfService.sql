INSERT INTO [dbo].[CATLogsOfService]
           ([BatchID]
           ,[RecordID]
           ,[CustomerID]
           ,[ServiceID]
           ,[UserID]
           ,[DateOfRequest]
           ,[RequestedURL]
           ,[RequestStatus]
           ,[BytesSent]
           ,[RequestTime]
           
           ,[UserIPAddress]
           )
    SELECT [BatchID]
      ,[RecordID]
      ,1
	  ,1
      ,[UserID]
      ,dateadd(hour,convert(int,substring(l.DateOfRequest,len(l.DateOfRequest)-5,4)) ,convert(datetime, substring(l.DateOfRequest,0,12)+' '+substring(l.DateOfRequest,13,8),104))
      ,[RequestedURL]
      ,[RequestStatus]
      ,[BytesSent]
      ,[RequestTime]
      ,[UserIPAddress]
  FROM [dbo].[STLogImport] l where 
  BatchID = 6 and RecordID <= 168021
           
