delete from [CATServicePatterns]; --Foreign key
DBCC CHECKIDENT ('CATServicePatterns', RESEED, 0);

delete from [CATServiceParameters];
DBCC CHECKIDENT ('CATServiceParameters', RESEED, 0);

-- Insert service A 2.1.02
INSERT INTO [dbo].[CATServiceParameters]([ServiceCode],[ServiceDescription],[ServiceBasicPrice]) VALUES('2.1.02',N'A.2.1.2 Poskytnutie priestorovej informácie zo súboru geodetických informácií z KN',0.001)
-- Insert pattern
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/kn_wfs/MapServer/WFSServer', '2.1.2', 1, 'MINV', N'Katastrálna mapa WFS', N'S-JTSK');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/kn_wms_norm/MapServer/WMSServer', '2.1.2', 1, null, N'Katastrálna mapa WMS' ,N'WebMercator + S-JTSK');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/kn_wms_orto/MapServer/WMSServer', '2.1.2', 1, null, N'Katastrálna mapa WMS - inverzná' ,N'WebMercator + S-JTSK');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/kn_wmts_norm_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1, null, N'Katastrálna mapa WMTS' ,N'S-JTSK');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/kn_wmts_norm_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1, null, N'Katastrálna mapa WMTS' ,N'WebMercator');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/kn_wmts_orto_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1, null, N'Katastrálna mapa WMTS - inverzná' ,N'S-JTSK');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/kn_wmts_orto_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1, null, N'Katastrálna mapa WMTS - inverzná' ,N'WebMercator');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/uo_wfs/MapServer/WFSServer', '2.1.2', 1, 'MINV', N'Mapa určeného operátu WFS' ,N'S-JTSK');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/uo_wms_norm/MapServer/WMSServer', '2.1.2', 1, null, N'Mapa určeného operátu WMS' ,N'WebMercator + S-JTSK');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/uo_wms_orto/MapServer/WMSServer', '2.1.2', 1, null, N'Mapa určeného operátu WMS - inverzná' ,N'WebMercator + S-JTSK');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/uo_wmts_norm_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1, null, N'Mapa určeného operátu WMTS' ,N'S-JTSK');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/uo_wmts_norm_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1, null, N'Mapa určeného operátu WMTS' ,N'WebMercator');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/uo_wmts_orto_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1, null, N'Mapa určeného operátu WMTS - inverzná' ,N'S-JTSK');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/uo_wmts_orto_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml', '2.1.2', 1, null, N'Mapa určeného operátu WMTS - inverzná' ,N'WebMercator');
-- Insert service A 2.1.03
INSERT INTO [dbo].[CATServiceParameters]([ServiceCode],[ServiceDescription],[ServiceBasicPrice]) VALUES('2.1.03',N'A.2.1.03 Poskytnutie informácie z KN o vlastníkoch a iných oprávnených osobách',0.001);
-- Insert pattern
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/Subjects?$select=Id&$filter=IdNo','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'IČO právnickej osoby');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('/Subjects(%)%','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Interné ID FOPO');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Interné ID FOPO');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Meno + priezvisko + rodné číslo fyzickej osoby');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Meno + priezvisko + rodné číslo fyzickej osoby');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3','MINV',N'Fyzická / Právnická osoba (FOPO)',N'Meno + priezvisko+ rodPriezvisko');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3','MINV',N'Fyzická / Právnická osoba (FOPO)',N'Meno + priezvisko+ rodPriezvisko+ datNarod');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Názov k.ú. + meno + priezvisko');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Názov k.ú. + meno + priezvisko ');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Názov k.ú. + názov');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Názov k.ú. + názov');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Názov obce + meno + priezvisko');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Názov obce + meno + priezvisko');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Názov obce + názov');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3','MINV',N'Fyzická / Právnická osoba (FOPO)',N'Rodné číslo');
INSERT INTO [dbo].[CATServicePatterns]([PatternCode],[PatternDescription],[FKServiceID],[Entity],[Explanation],[DatSelectMethod]) VALUES('','2.1.3',null,N'Fyzická / Právnická osoba (FOPO)',N'Názov obce + názov');