USE [log4service]
GO

IF OBJECT_ID('dbo.CATProcessStatus', 'U') IS NOT NULL 
  DROP TABLE dbo.CATProcessStatus; 

GO

/****** Object:  Table [dbo].[CATProcessStatus]    Script Date: 14. 6. 2017 9:09:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].CATProcessStatus(
    [StepName] varchar(20),
    [BatchID] varchar(100) ,
	[BatchRecordNum] [int],
	[NumberOfService] [int] ,
	[NumberOfCustomer] [int],
	[NumberOfUnknownService] [int],
	[NumberOfPreprocessDelete] [int],
	[TCInsertTime] [datetime] 
	

) ON [PRIMARY] 
GO

ALTER TABLE [dbo].CATProcessStatus  ADD  CONSTRAINT [DF_CATProcessStatus_TCInsertTime] DEFAULT (getdate()) FOR [TCInsertTime]
GO

--insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum], [NumberOfService] ,[NumberOfCustomer],[NumberOfUnknownService], [NumberOfPreprocessDelete])
--values ('Name', 'batchlist', 'batchrecord', 'NumberOfService', 'NumberOfCustomer', 'NumberOfUnknownService', 'NumberOfPreprocessDelete');