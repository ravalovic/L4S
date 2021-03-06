/****** Script for SelectTopNRows command from SSMS  ******/
use log4service

update CATServiceParameters
set ServiceCode = 'A.'+ ServiceCode
--SUBSTRING(servicecode,3,len(servicecode));

update CATCustomerServices 
set ServiceCode = (select p.ServiceCode from CATServiceParameters p where p.PKServiceID = CATCustomerServices.FKServiceID);

update CATInvoiceByDay 
set ServiceCode = (select p.ServiceCode from CATServiceParameters p where p.PKServiceID = CATInvoiceByDay.ServiceID);

update CATInvoiceByMonth 
set ServiceCode = (select p.ServiceCode from CATServiceParameters p where p.PKServiceID = CATInvoiceByMonth.ServiceID);

update CATCustomerServiceDetailInvoice 
set ServiceCode = (select p.ServiceCode from CATServiceParameters p where p.PKServiceID = CATCustomerServiceDetailInvoice.ServiceID);

update CATServicePatterns 
set PatternDescription = (select p.ServiceCode from CATServiceParameters p where p.PKServiceID = CATServicePatterns.FKServiceID);



select SUBSTRING(servicecode,3,len(servicecode)) from CATServiceParameters



