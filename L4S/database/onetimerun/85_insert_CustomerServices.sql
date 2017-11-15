USE [log4service]
DBCC CHECKIDENT ('CATCustomerServices', RESEED, 0);
GO
DELETE FROM [dbo].[CATCustomerServices];
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (2,N'A.2.1.2 - Poskytnutie priestorovej informácie zo súboru geodetických informácií z KN','A.2.1.2',1.01,'',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (3,N'A.2.1.3 - Poskytnutie informácie z KN o vlastníkoch a iných oprávnených osobách','A.2.1.3',1.01,'',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (4,N'A.2.1.4 - Poskytnutie informácie z KN o nehnuteľnostiach','A.2.1.4',1.01,'',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (5,N'A.2.1.5 - Poskytnutie informácie z KN o právach k nehnuteľnostiam','A.2.1.5',1.01,'',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (6,N'A.2.1.6 - Poskytnutie informácie z KN o registri územno technických jednotiek','A.2.1.6',1.01,'',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (9,N'A.2.1.9 - Poskytnutie výpisu z listu vlastníctva z KN','A.2.1.9',1.01,'',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (15,N'A.2.1.15 - Poskytnutie informácie z KN o súpise vlastníkov','A.2.1.15',1.01,'',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (16,N'A.2.1.16 - Poskytnutie informácie z KN o súpise správcov','A.2.1.16',1.01,'',1);

INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (2,N'A.2.1.2 - Poskytnutie priestorovej informácie zo súboru geodetických informácií z KN','A.2.1.2',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (3,N'A.2.1.3 - Poskytnutie informácie z KN o vlastníkoch a iných oprávnených osobách','A.2.1.3',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (4,N'A.2.1.4 - Poskytnutie informácie z KN o nehnuteľnostiach','A.2.1.4',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (5,N'A.2.1.5 - Poskytnutie informácie z KN o právach k nehnuteľnostiam','A.2.1.5',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (6,N'A.2.1.6 - Poskytnutie informácie z KN o registri územno technických jednotiek','A.2.1.6',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (7,N'A.2.1.7 - Poskytnutie informácie z KN o číselníkoch','A.2.1.7',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (9,N'A.2.1.9 - Poskytnutie výpisu z listu vlastníctva z KN','A.2.1.9',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (13,N'A.2.1.13 - Poskytnutie informácie z KN o súpise parciel z registra C a E','A.2.1.13',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (14,N'A.2.1.14 - Poskytnutie informácie z KN o súpise stavieb','A.2.1.14',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (15,N'A.2.1.15 - Poskytnutie informácie z KN o súpise vlastníkov','A.2.1.15',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (16,N'A.2.1.16 - Poskytnutie informácie z KN o súpise správcov','A.2.1.16',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (17,N'A.2.1.17 - Poskytnutie informácie z KN o súpise nájomcov','A.2.1.17',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (18,N'A.2.1.18 - Poskytnutie informácie z KN o súpise iných oprávnených osôb','A.2.1.18',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (20,N'A.2.1.20 - Poskytnutie informácie z KN na vybrané geodetické činnosti v KN','A.2.1.20',1.0,'',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (30,N'A.2.1.15 - Poskytnutie informácie z KN o súpise vlastníkov','IOM',1.0,'',2);


INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (2,N'A.2.1.2 - Poskytnutie priestorovej informácie zo súboru geodetických informácií z KN','A.2.1.2',0.95,'',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (3,N'A.2.1.3 - Poskytnutie informácie z KN o vlastníkoch a iných oprávnených osobách','A.2.1.3',0.95,'',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (4,N'A.2.1.4 - Poskytnutie informácie z KN o nehnuteľnostiach','A.2.1.4',0.95,'',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (5,N'A.2.1.5 - Poskytnutie informácie z KN o právach k nehnuteľnostiam','A.2.1.5',0.95,'',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (6,N'A.2.1.6 - Poskytnutie informácie z KN o registri územno technických jednotiek','A.2.1.6',0.95,'',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (9,N'A.2.1.9 - Poskytnutie výpisu z listu vlastníctva z KN','A.2.1.9',0.95,'',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (15,N'A.2.1.15 - Poskytnutie informácie z KN o súpise vlastníkov','A.2.1.15',0.95,'',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (16,N'A.2.1.16 - Poskytnutie informácie z KN o súpise správcov','A.2.1.16',0.95,'',4);
