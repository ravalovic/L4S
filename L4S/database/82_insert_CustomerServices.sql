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
     VALUES (4,'Sluzba 6','A6',1.01,'Sluzba poznamka',1);
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
     VALUES (2,'Sluzba 2','A2',1.01,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (3,'Sluzba 3','A3',1.01,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (4,'Sluzba 4','A4',1.01,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (4,'Sluzba 6','A6',1.01,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (5,'Sluzba 5','A5',1.01,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (6,'Sluzba 6','A6',1.01,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (9,'Sluzba 9','A9',1.01,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (15,'Sluzba 15','A15',1.01,'Sluzba poznamka',4);
INSERT INTO [dbo].[CATCustomerServices] ([FKServiceID], [ServiceName], [ServiceCode], [ServicePriceDiscount], [ServiceNote],[FKCustomerDataID])
     VALUES (16,'Sluzba 16','A16',1.01,'Sluzba poznamka',4);
