USE [log4service]
GO

IF OBJECT_ID('[dbo].[CATServicePAtternsChange]', 'TR') IS NOT NULL 
  DROP TRIGGER [dbo].[CATServicePAtternsChange]; 
  
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[CATServicePAtternsChange]
   ON  [dbo].[CATServicePAtterns]
   AFTER INSERT,UPDATE
AS 
DECLARE @action as char(1);
BEGIN
    SET NOCOUNT ON;
    --
    -- Check if this is an INSERT, UPDATE or DELETE Action.
    -- 
    SET @action = 'I'; -- Set Action to Insert by default.
    IF EXISTS(SELECT * FROM DELETED)
    BEGIN
        SET @action = 
            CASE
                WHEN EXISTS(SELECT * FROM INSERTED) THEN 'U' -- Set Action to Updated.
                ELSE 'D' -- Set Action to Deleted.       
            END
    END
    ELSE 
        IF NOT EXISTS(SELECT * FROM INSERTED) RETURN; -- Nothing updated or inserted.
        

INSERT INTO [dbo].[CATChangeDetect]
           ([TableName]
           ,[Status]
		   ,[StatusName]
           )
     VALUES
           ('CATServicePAtternsChange'
           ,0
		   ,@action
           )

END