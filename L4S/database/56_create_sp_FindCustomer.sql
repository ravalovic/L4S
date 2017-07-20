USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_FindCustomer]    Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_FindCustomer]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].sp_FindCustomer
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_FindCustomer]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].sp_FindCustomer
	-- Add the parameters for the stored procedure here
	@myCustomerID int = 0,
	@myInput int = 0,
	@myBatchList varchar(max),
	@myCustomerQuery varchar(max) ='' out
AS
DECLARE
       @myLike varchar(max),
	   @first int = 0,
	   @myWhere varchar(max) = ' UserIPAddress like ''',
       @myOR varchar(max) = ' or UserIPAddress like ''',
	   @myPerc varchar(max) ='%',
       @myENDLike varchar(max) = '''',
	   @myTable varchar(100)
BEGIN
     IF (@myInput = 0) SET @myTable = '[dbo].[STLogImport]';
	 IF (@myInput = 1) SET @myTable = '[dbo].[CATUnknownService]';
     SET NOCOUNT ON;
	--Get service parameters
		  SET @myCustomerQuery = 'UPDATE '+@myTable+' 
									SET 
									 [CustomerID] = '+cast(@myCustomerID as varchar)+'
								  WHERE BatchID IN'+@myBatchList+' AND (';
			DECLARE myCursor CURSOR FOR SELECT  CustomerIdentifier FROM [dbo].CATCustomerIdentifiers WHERE  FKCustomerID = @myCustomerID and TCActive<>99;
		    OPEN myCursor
		    FETCH NEXT FROM myCursor INTO @myLike
		    WHILE @@FETCH_STATUS = 0   
		    BEGIN  
			       IF @first = 0  
				    BEGIN
				        SET @myCustomerQuery = @myCustomerQuery + @myWhere  + @myPerc + @myLike+  @myPerc+ @myENDLike;
						SET @first =1 ;
					END
				   ELSE SET @myCustomerQuery = @myCustomerQuery + @myOR  + @myPerc + @myLike+  @myPerc + @myENDLike;
				   FETCH NEXT FROM myCursor INTO @myLike
		    END   
		    CLOSE myCursor   
		    DEALLOCATE myCursor
			SET @myCustomerQuery =@myCustomerQuery + ' )';
    
END
	 
