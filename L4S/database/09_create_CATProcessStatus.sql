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
    [BatchID] [int] not null,
	[BatchRecordNum] [int] not null,
	[ActualStepName] varchar(20),
	[ActualStepID] [int] not null,
	[ActualStepStatus] [int] not null,
	[TCInsertTime] [datetime] NULL,
	[TCLastUpdate] [datetime] NULL,
	[TCActive] [int] NULL,

) ON [PRIMARY] 
GO

ALTER TABLE [dbo].CATProcessStatus  ADD  CONSTRAINT [DF_CATProcessStatus_TCInsertTime] DEFAULT (getdate()) FOR [TCInsertTime]
GO

ALTER TABLE [dbo].CATProcessStatus ADD  CONSTRAINT [DF_CATProcessStatus_TCLastUpdate] DEFAULT (getdate()) FOR [TCLastUpdate]
GO

ALTER TABLE [dbo].CATProcessStatus ADD  CONSTRAINT [DF_CATProcessStatus_TCActive]  DEFAULT ((0)) FOR [TCActive]
GO

