USE [log4service]
DBCC CHECKIDENT ('CATCustomerServices', RESEED, 0);
GO
DELETE FROM [dbo].[CATCustomerServices];

INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (2,'Sluzba 2','A2',1.01,'Sluzba poznamka',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (3,'Sluzba 3','A3',1.01,'Sluzba poznamka',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (4,'Sluzba 4','A4',1.01,'Sluzba poznamka',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (5,'Sluzba 5','A5',1.01,'Sluzba poznamka',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (6,'Sluzba 6','A6',1.01,'Sluzba poznamka',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (9,'Sluzba 9','A9',1.01,'Sluzba poznamka',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (15,'Sluzba 15','A15',1.01,'Sluzba poznamka',1);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (16,'Sluzba 16','A16',1.01,'Sluzba poznamka',1);

INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (2,'Sluzba 2','A2',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (3,'Sluzba 3','A3',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (4,'Sluzba 4','A4',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (5,'Sluzba 5','A5',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (6,'Sluzba 6','A6',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (7,'Sluzba 7','A7',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (9,'Sluzba 9','A9',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (13,'Sluzba 13','A13',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (14,'Sluzba 14','A14',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (15,'Sluzba 15','A15',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (16,'Sluzba 16','A16',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (17,'Sluzba 17','A17',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (18,'Sluzba 18','A18',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (20,'Sluzba 20','A20',1.0,'Sluzba poznamka',2);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (30,'Sluzba IOM','IOM',1.0,'Sluzba poznamka',2);


INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (2,'Sluzba 2','A2',0.95,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (3,'Sluzba 3','A3',0.95,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (4,'Sluzba 4','A4',0.95,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (5,'Sluzba 5','A5',0.95,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (6,'Sluzba 6','A6',0.95,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (9,'Sluzba 9','A9',0.95,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (15,'Sluzba 15','A15',0.95,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (16,'Sluzba 16','A16',0.95,'Sluzba poznamka',4);
