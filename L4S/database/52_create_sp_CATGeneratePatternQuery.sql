-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_CATLoadLogsOfServices 
	-- Add the parameters for the stored procedure here
	@ServiceID int = 0
AS
DECLARE 
       @myLike varchar(2000),
       @mySelect varchar(8000),
       @myWhere varchar(100) = 'where l.RequestedURL like ''',
       @myOR varchar(100) = ' or l.RequestedURL like ''',
       @myENDLike varchar(4) = '''',
       @i int = 0
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @mySelect = 'SELECT l.DateOfRequest, l.RequestedURL, l.UserIPAddress from STLogImport l ';
    declare like_cursor CURSOR FOR select p.PatternLike from CATServicePatterns p where p.FKServiceID = @ServiceID;
    OPEN like_cursor
    FETCH NEXT FROM like_cursor INTO @myLike
    WHILE @@FETCH_STATUS = 0   
    BEGIN   
           IF (@i=0)
    	     SET @mySelect = @mySelect + @myWhere + @myLike+  @myENDLike;
    	   ELSE 
    	     SET @mySelect = @mySelect + @myOR + @myLike + @myENDLike;
    	   --print @myLike;
    	   --print @mySelect;
           FETCH NEXT FROM like_cursor INTO @myLike   
    	   SET @i = @i+1;
    END   
    CLOSE like_cursor   
    DEALLOCATE like_cursor

print @mySelect;
--exec(@mySelect);
END
GO


