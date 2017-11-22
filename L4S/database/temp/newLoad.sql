use log4service;
delete  FROM STInputFileInfo;  
print('Delete STInputFileInfo');

delete from  STInputFileDuplicity;
print('Delete STInputFileDuplicity');
truncate table STLogImport;
delete from STLogImport;
print('Delete STLogImport');

truncate table CATUnknownService;
delete from CATUnknownService;
print('Delete CATUnknownService');

truncate table CATLogsOfService;
delete from CATLogsOfService;
print('Delete CATLogsOfService');

delete from CATProcessStatus;
print('Delete CATProcessStatus');

delete from CATCustomerMonthlyData;
print('Delete CATCustomerMonthlyData');

delete from CATCustomerDailyData;
print('Delete CATCustomerDailyData');

delete from CATChangeDetect;
print('Delete CATChangeDetect');

delete from CATBillingInfo;
print('Delete CATBillingInfo');

delete from CATInvoiceByDay;
print('Delete CATInvoiceByDay');

delete from CATInvoiceByMonth;
print('Delete CATInvoiceByMonth');

delete from CATCustomerServiceDetailInvoice;
print('Delete CATCustomerServiceDetailInvoice');

delete from CATSummaryInvoice;
print('Delete CATSummaryInvoice');

delete from GAPAnalyze;
print('Delete GAPAnalyze');

