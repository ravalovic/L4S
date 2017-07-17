USE [log4service]
GO

/****** Object:  View [dbo].[view_InvoiceByDay]    Script Date: 3. 7. 2017 16:05:46 ******/
DROP VIEW [dbo].view_InvoiceByDay
GO

/****** Object:  View [dbo].[view_InvoiceByDay]    Script Date: 3. 7. 2017 16:05:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create view [dbo].view_InvoiceByDay as
select m.ID, m.DateOfRequest, m.CustomerID, m.ServiceID, m.NumberOfRequest,
 Case 
     when g.ParamValue = UPPER('BYTE')  then m.ReceivedBytes
	 when g.ParamValue = UPPER('MBYTE')  then m.ReceivedBytes/(1024.0*1024.0)
	 when g.ParamValue = UPPER('GBYTE')  then  m.ReceivedBytes/(1024.0*1024.0*1024.0)
	
 end as ReceivedBytes 
 ,m.RequestedTime, c.ServiceCode, c.ServiceName, (p.ServiceBasicPrice * c.ServicePriceDiscount) UnitPrice,
 Case 
     when g.ParamValue = UPPER('NUMREQ') then 'NumOfRequest'
	 when g.ParamValue = UPPER('BYTE') then 'Byte'
	 when g.ParamValue = UPPER('MBYTE') then 'MByte'
	 when g.ParamValue = UPPER('GBYTE') then 'GByte'
 end as MeasureOfUnits,
 Case 
     when g.ParamValue = UPPER('NUMREQ') then (p.ServiceBasicPrice * m.NumberOfRequest * c.ServicePriceDiscount) 
	 when g.ParamValue = UPPER('BYTE') then (p.ServiceBasicPrice * m.ReceivedBytes * c.ServicePriceDiscount)
	 when g.ParamValue = UPPER('MBYTE') then (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0) * c.ServicePriceDiscount)
	 when g.ParamValue = UPPER('GBYTE') then (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0*1024.0) * c.ServicePriceDiscount)
 end as BasicPriceWithoutVAT 
 ,Case 
     when g.ParamValue = UPPER('NUMREQ') then  (p.ServiceBasicPrice * m.NumberOfRequest * 0.2) 
	 when g.ParamValue = UPPER('BYTE')   then (p.ServiceBasicPrice * m.ReceivedBytes * 0.2)
	 when g.ParamValue = UPPER('MBYTE')  then (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0) * 0.2)
	 when g.ParamValue = UPPER('GBYTE')  then  (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0*1024.0) * 0.2)
  end as VAT
 ,Case 
     when g.ParamValue = UPPER('NUMREQ') then (p.ServiceBasicPrice * m.NumberOfRequest * 1.2)  
     when g.ParamValue = UPPER('BYTE')   then (p.ServiceBasicPrice * m.ReceivedBytes * 1.2)
	 when g.ParamValue = UPPER('MBYTE')  then (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0) * 1.2)
	 when g.ParamValue = UPPER('GBYTE')  then (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0*1024.0) * 1.2) 
 end as BasicPriceWithVAT
 from CATCustomerDailyData m, CATCustomerServices c, CATServiceParameters p , CONFGeneralSettings g
where m.CustomerID = c.FKCustomerDataID and m.ServiceID = c.FKServiceID and c.FKServiceID = p.PKServiceID and g.ParamName = UPPER('METRICUNIT')
GO


