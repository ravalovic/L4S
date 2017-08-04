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

select m.ID, m.DateOfRequest, m.CustomerID
     , n.CompanyID as CustomerIdentification, n.CompanyName as CustomerName
	 , m.ServiceID, p.ServiceCode, p.ServiceDescription
	 , m.NumberOfRequest,
 Case 
     when g.ParamValue = UPPER('BYTE')  then convert(decimal(18,5),m.ReceivedBytes)
	 when g.ParamValue = UPPER('MBYTE')  then convert(decimal(18,5),m.ReceivedBytes/(1024.0*1024.0))
	 when g.ParamValue = UPPER('GBYTE')  then  convert(decimal(18,5),m.ReceivedBytes/(1024.0*1024.0*1024.0))
	
 end as ReceivedBytes 
 ,m.RequestedTime, c.ServiceCode as CustomerServiceCode, c.ServiceName as CustomerServiceName, convert(decimal(18,5),(p.ServiceBasicPrice * c.ServicePriceDiscount)) UnitPrice,
 Case 
     when g.ParamValue = UPPER('NUMREQ') then 'NumOfRequest'
	 when g.ParamValue = UPPER('BYTE') then 'Byte'
	 when g.ParamValue = UPPER('MBYTE') then 'MByte'
	 when g.ParamValue = UPPER('GBYTE') then 'GByte'
 end as MeasureOfUnits,
 Case 
     when g.ParamValue = UPPER('NUMREQ') then convert(decimal(18,5),(p.ServiceBasicPrice * m.NumberOfRequest * c.ServicePriceDiscount))
	 when g.ParamValue = UPPER('BYTE') then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes * c.ServicePriceDiscount))
	 when g.ParamValue = UPPER('MBYTE') then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0) * c.ServicePriceDiscount))
	 when g.ParamValue = UPPER('GBYTE') then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0*1024.0) * c.ServicePriceDiscount))
 end as BasicPriceWithoutVAT 
 ,Case 
     when g.ParamValue = UPPER('NUMREQ') then  convert(decimal(18,5),(p.ServiceBasicPrice * m.NumberOfRequest * convert(decimal(18,5), d.ParamValue)) )
	 when g.ParamValue = UPPER('BYTE')   then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes * convert(decimal(18,5), d.ParamValue)))
	 when g.ParamValue = UPPER('MBYTE')  then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0) * convert(decimal(18,5), d.ParamValue)))
	 when g.ParamValue = UPPER('GBYTE')  then convert(decimal(18,5), (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0*1024.0) * convert(decimal(18,5), d.ParamValue)))
  end as VAT
 ,Case 
     when g.ParamValue = UPPER('NUMREQ') then convert(decimal(18,5),(p.ServiceBasicPrice * m.NumberOfRequest * (1 + convert(decimal(18,5), d.ParamValue))))  
     when g.ParamValue = UPPER('BYTE')   then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes * (1 + convert(decimal(18,5), d.ParamValue))))
	 when g.ParamValue = UPPER('MBYTE')  then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0) * (1 + convert(decimal(18,5), d.ParamValue))))
	 when g.ParamValue = UPPER('GBYTE')  then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0*1024.0) * (1 + convert(decimal(18,5), d.ParamValue)))) 
 end as BasicPriceWithVAT
 ,m.TCActive
 from CATCustomerMonthlyData m, CATCustomerServices c, CATServiceParameters p , CONFGeneralSettings g, CONFGeneralSettings d, CATCustomerData n
where n.CompanyID is not null
and m.CustomerID = c.FKCustomerDataID 
and m.ServiceID = c.FKServiceID 
and c.FKServiceID = p.PKServiceID 
and n.PKCustomerDataID = m.CustomerID
and g.ParamName = UPPER('METRICUNIT') and d.ParamName = UPPER('DPH')
UNION ALL
select m.ID, m.DateOfRequest, m.CustomerID
     , n.IndividualID as CustomerIdentification, n.IndividualFirstName+' '+n.IndividualLastName as CustomerName
	 , m.ServiceID, p.ServiceCode, p.ServiceDescription
	 , m.NumberOfRequest,
 Case 
     when g.ParamValue = UPPER('BYTE')  then convert(decimal(18,5),m.ReceivedBytes)
	 when g.ParamValue = UPPER('MBYTE')  then convert(decimal(18,5),m.ReceivedBytes/(1024.0*1024.0))
	 when g.ParamValue = UPPER('GBYTE')  then  convert(decimal(18,5),m.ReceivedBytes/(1024.0*1024.0*1024.0))
	
 end as ReceivedBytes 
 ,m.RequestedTime, c.ServiceCode as CustomerServiceCode, c.ServiceName as CustomerServiceName, convert(decimal(18,5),(p.ServiceBasicPrice * c.ServicePriceDiscount)) UnitPrice,
 Case 
     when g.ParamValue = UPPER('NUMREQ') then 'NumOfRequest'
	 when g.ParamValue = UPPER('BYTE') then 'Byte'
	 when g.ParamValue = UPPER('MBYTE') then 'MByte'
	 when g.ParamValue = UPPER('GBYTE') then 'GByte'
 end as MeasureOfUnits,
 Case 
     when g.ParamValue = UPPER('NUMREQ') then convert(decimal(18,5),(p.ServiceBasicPrice * m.NumberOfRequest * c.ServicePriceDiscount))
	 when g.ParamValue = UPPER('BYTE') then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes * c.ServicePriceDiscount))
	 when g.ParamValue = UPPER('MBYTE') then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0) * c.ServicePriceDiscount))
	 when g.ParamValue = UPPER('GBYTE') then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0*1024.0) * c.ServicePriceDiscount))
 end as BasicPriceWithoutVAT 
 ,Case 
     when g.ParamValue = UPPER('NUMREQ') then  convert(decimal(18,5),(p.ServiceBasicPrice * m.NumberOfRequest * convert(decimal(18,5), d.ParamValue)) )
	 when g.ParamValue = UPPER('BYTE')   then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes * convert(decimal(18,5), d.ParamValue)))
	 when g.ParamValue = UPPER('MBYTE')  then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0) * convert(decimal(18,5), d.ParamValue)))
	 when g.ParamValue = UPPER('GBYTE')  then convert(decimal(18,5), (p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0*1024.0) * convert(decimal(18,5), d.ParamValue)))
  end as VAT
 ,Case 
     when g.ParamValue = UPPER('NUMREQ') then convert(decimal(18,5),(p.ServiceBasicPrice * m.NumberOfRequest * (1 + convert(decimal(18,5), d.ParamValue))))  
     when g.ParamValue = UPPER('BYTE')   then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes * (1 + convert(decimal(18,5), d.ParamValue))))
	 when g.ParamValue = UPPER('MBYTE')  then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0) * (1 + convert(decimal(18,5), d.ParamValue))))
	 when g.ParamValue = UPPER('GBYTE')  then convert(decimal(18,5),(p.ServiceBasicPrice * m.ReceivedBytes/(1024.0*1024.0*1024.0) * (1 + convert(decimal(18,5), d.ParamValue)))) 
 end as BasicPriceWithVAT
 ,m.TCActive
 from CATCustomerMonthlyData m, CATCustomerServices c, CATServiceParameters p , CONFGeneralSettings g, CONFGeneralSettings d, CATCustomerData n
where n.IndividualID is not null
and m.CustomerID = c.FKCustomerDataID 
and m.ServiceID = c.FKServiceID 
and c.FKServiceID = p.PKServiceID 
and n.PKCustomerDataID = m.CustomerID
and g.ParamName = UPPER('METRICUNIT') and d.ParamName = UPPER('DPH')
GO


