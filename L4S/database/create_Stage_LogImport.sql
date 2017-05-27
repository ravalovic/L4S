USE [log4service]
GO

/****** Object:  Table [dbo].[Stage_TestTable]    Script Date: 27.05.2017 20:06:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
if OBJECT_ID('log4service..Stage_LogImport') is not null
begin 
 drop table log4service..Stage_LogImport
end;

CREATE TABLE [dbo].[Stage_LogImport](
	batchID bigint Not null,
	[fileName] [nvarchar](50) not null ,
	[col1] [nvarchar](50) NULL,
	[col2] [nvarchar](50) NULL,
	[col3] [nvarchar](50) NULL,
	[col4] [nvarchar](50) NULL,
	[col5] [nvarchar](50) NULL,
	[col6] [nvarchar](50) NULL,
	[col7] [nvarchar](50) NULL,
	[col8] [nvarchar](50) NULL,
	[col9] [nvarchar](50) NULL,
	[insertDateTime] DateTime not null default GETDATE(),
) ON [PRIMARY]
GO


