use log4service;
delete from CATCustomerDailyData;
print('Delete CATCustomerDailyData');

delete from CATCustomerMonthlyData;
print('Delete CATCustomerMonthlyData');

delete from CATInvoiceByDay;
print('Delete CATInvoiceByDay');

delete from CATInvoiceByMonth;
print('Delete CATInvoiceByMonth');

delete from CATCustomerServiceDetailInvoice;
print('Delete CATCustomerServiceDetailInvoice');

update CATLogsOfService SET TCActive = 0;
print('Update CATLogsOfService');

update CATCustomerDailyData
set TCActive = 0; 
print('Update CATCustomerDailyData');

update CATCustomerMonthlyData
set TCActive = 0;
print('Update CATCustomerMonthlyData');

update [CONFGeneralSettings]
set ParamValue = ''
where ParamName='LastInvoiceGenerate';
print('Update [CONFGeneralSettings] LastInvoiceGenerate');

update [CONFGeneralSettings]
set ParamValue = DAY(getdate())
where ParamName='InvoiceCreationDay';
print('Update [CONFGeneralSettings] InvoiceCreationDay');
