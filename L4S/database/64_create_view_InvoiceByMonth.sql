USE [log4service]
GO

/****** Object:  View [dbo].[[view_InvoiceByMonth]]    Script Date: 3. 7. 2017 16:05:46 ******/
DROP VIEW [dbo].[view_InvoiceByMonth]
GO

/****** Object:  View [dbo].[[view_InvoiceByMonth]]    Script Date: 3. 7. 2017 16:05:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create view [dbo].[view_InvoiceByMonth] as
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
     when g.ParamValue = UPPER('NUMREQ') then  (p.ServiceBasicPrice * m.NumberOfRequest * convert(decimal(18,5), d.ParamValue)) 
	 when g.ParamValue = UPPER('BYTE')   then (p.ServiceBasicPrice * m.ReceivedBytes * convert(decimal(18,5), d.ParamValue))
	 when g.ParamValue = UPPER('MBYTE')  then (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0) * convert(decimal(18,5), d.ParamValue))
	 when g.ParamValue = UPPER('GBYTE')  then  (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0*1024.0) * convert(decimal(18,5), d.ParamValue))
  end as VAT
 ,Case 
     when g.ParamValue = UPPER('NUMREQ') then (p.ServiceBasicPrice * m.NumberOfRequest * (1 + convert(decimal(18,5), d.ParamValue)))  
     when g.ParamValue = UPPER('BYTE')   then (p.ServiceBasicPrice * m.ReceivedBytes * (1 + convert(decimal(18,5), d.ParamValue)))
	 when g.ParamValue = UPPER('MBYTE')  then (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0) * (1 + convert(decimal(18,5), d.ParamValue)))
	 when g.ParamValue = UPPER('GBYTE')  then (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0*1024.0) * (1 + convert(decimal(18,5), d.ParamValue))) 
 end as BasicPriceWithVAT
 ,m.TCActive
 from CATCustomerMonthlyData m, CATCustomerServices c, CATServiceParameters p , CONFGeneralSettings g, CONFGeneralSettings d
where m.CustomerID = c.FKCustomerDataID and m.ServiceID = c.FKServiceID and c.FKServiceID = p.PKServiceID and g.ParamName = UPPER('METRICUNIT') and d.ParamName = UPPER('DPH')
GO


