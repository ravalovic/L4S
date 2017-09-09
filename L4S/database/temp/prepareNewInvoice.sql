use log4service;
delete from CATInvoiceByDay;
delete from CATInvoiceByMonth;
delete from CATCustomerServiceDetailInvoice;
update CATCustomerDailyData
set TCActive = 0; 
update CATCustomerMonthlyData
set TCActive = 0;

update [CONFGeneralSettings]
set ParamValue = ''
where ParamName='LastInvoiceGenerate';

update [CONFGeneralSettings]
set ParamValue = DAY(getdate())
where ParamName='InvoiceCreationDay';