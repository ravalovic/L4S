delete from [CATServicePatterns]; --Foreign key
DBCC CHECKIDENT ('CATServicePatterns', RESEED, 0);

delete from [CATServiceParameters];
DBCC CHECKIDENT ('CATServiceParameters', RESEED, 0);

-- Insert service A 2.1.02
INSERT INTO [dbo].[CATServiceParameters]([ServiceCode],[ServiceDescription],[ServiceBasicPrice]) VALUES('2.1.02',N'A.2.1.2 Poskytnutie priestorovej informácie zo súboru geodetických informácií z KN',0.001)
-- Insert pattern
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/kn_wfs/MapServer/WFSServer', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/kn_wms_norm/MapServer/WMSServer', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/kn_wms_orto/MapServer/WMSServer', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/kn_wmts_norm_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/kn_wmts_norm_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/kn_wmts_orto_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/kn_wmts_orto_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/uo_wfs/MapServer/WFSServer', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/uo_wms_norm/MapServer/WMSServer', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/uo_wms_orto/MapServer/WMSServer', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/uo_wmts_norm_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/uo_wmts_norm_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/uo_wmts_orto_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1);
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID]) VALUES('/uo_wmts_orto_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1);

