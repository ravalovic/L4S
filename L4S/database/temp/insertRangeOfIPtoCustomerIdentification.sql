DECLARE 
 @NIP varchar(100),
 @Desc varchar(100),

 @IP varchar(100)='192.168.1.',
 @first int = 1,
 @last int = 254,
 @CustID int = 5,
 @step int = 1

 -- pomocny select pre vyber PKCustomerDataID nastavit do @CustID
 -- SELECT [PKCustomerDataID], [CustomerType], [CompanyName], [CompanyType], [CompanyID], [IndividualFirstName], [IndividualLastName], [IndividualID] FROM [dbo].[CATCustomerData]

begin
 WHILE (@first <= @last)
    BEGIN
	    
        SET @NIP = @IP + cast(@first+@step-1 as varchar);
		SET @Desc = 'IPADD_'+cast(@first+@step-1 as varchar);
        SET @first = @first + @step;
		PRINT @NIP;
		PRINT @Desc;

		INSERT INTO [dbo].[CATCustomerIdentifiers]
           ([CustomerIdentifier]
           ,[CustomerIdentifierDescription]
           ,[FKCustomerID]
           )
     VALUES
           (@NIP
           ,@Desc
           ,@CustID
           ) 
    END
end  