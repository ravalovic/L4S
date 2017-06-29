USE [log4service]
GO

IF OBJECT_ID('dbo.CATChangeDetect', 'U') IS NOT NULL 
  DROP TABLE dbo.CATChangeDetect; 

GO

/****** Object:  Table [dbo].[CATChangeDetect]    Script Date: 14. 6. 2017 9:09:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].CATChangeDetect(
    [TableName] varchar(20),
	[Status] int,
    [StatusName] varchar(100) ,
	[TCInsertTime] [datetime],
    [TCUpdateTime] [datetime]	
	

) ON [PRIMARY] 
GO

ALTER TABLE [dbo].CATChangeDetect  ADD  CONSTRAINT [DF_CATChangeDetect_TCInsertTime] DEFAULT (getdate()) FOR [TCInsertTime]
GO
ALTER TABLE [dbo].CATChangeDetect  ADD  CONSTRAINT [DF_CATChangeDetect_TCUpdateTime] DEFAULT (getdate()) FOR [TCUpdateTime]
GO

--insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum], [NumberOfService] ,[NumberOfCustomer],[NumberOfUnknownService], [NumberOfPreprocessDelete])
--values ('Name', 'batchlist', 'batchrecord', 'NumberOfService', 'NumberOfCustomer', 'NumberOfUnknownService', 'NumberOfPreprocessDelete');