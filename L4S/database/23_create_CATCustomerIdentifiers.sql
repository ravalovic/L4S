USE [log4service]
GO
IF (OBJECT_ID('FK_CATCustomerIdentifiers_FKCustomerID', 'F') IS NOT NULL)
ALTER TABLE [dbo].[CATCustomerIdentifiers] DROP CONSTRAINT [FK_CATCustomerIdentifiers_FKCustomerID]
GO

/****** Object:  Table [dbo].[CATCustomerIdentifiers]    Script Date: 15. 6. 2017 15:51:36 ******/
IF OBJECT_ID('dbo.CATCustomerIdentifiers', 'U') IS NOT NULL 
DROP TABLE [dbo].[CATCustomerIdentifiers]
GO

/****** Object:  Table [dbo].[CATCustomerIdentifiers]    Script Date: 15. 6. 2017 15:51:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CATCustomerIdentifiers](
	[PKCustomerIdentifiersID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerIdentifier] [varchar](200) NOT NULL,
	[CustomerIdentifierDescription] [varchar](50) NULL,
	[FKCustomerID] [int] NOT NULL,
 CONSTRAINT [PK_CATCustomerIdentifiers] PRIMARY KEY CLUSTERED 
(
	[PKCustomerIdentifiersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CATCustomerIdentifiers]  WITH CHECK ADD  CONSTRAINT [FK_CATCustomerIdentifiers_FKCustomerID] FOREIGN KEY([FKCustomerID])
REFERENCES [dbo].[CATCustomerData] ([PKCustomerDataID])
GO

