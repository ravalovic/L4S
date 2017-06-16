select l.DateOfRequest, l.RequestedURL, l.UserIPAddress from STLogImport l where 
-- A 2.1.02
select l.DateOfRequest, l.RequestedURL, l.UserIPAddress from STLogImport l where 
l.RequestedURL like'%/kn_wfs/MapServer/WFSServer%'
or l.RequestedURL like'%/kn_wms_norm/MapServer/WMSServer%'
or l.RequestedURL like'%/kn_wms_orto/MapServer/WMSServer%'
or l.RequestedURL like'%/kn_wmts_norm_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml%'
or l.RequestedURL like'%/kn_wmts_norm_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml%'
or l.RequestedURL like'%/kn_wmts_orto_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml%'
or l.RequestedURL like'%/kn_wmts_orto_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml%'
or l.RequestedURL like'%/uo_wfs/MapServer/WFSServer%'
or l.RequestedURL like'%/uo_wms_norm/MapServer/WMSServer%'
or l.RequestedURL like'%/uo_wms_orto/MapServer/WMSServer%'
or l.RequestedURL like'%/uo_wmts_norm_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml%'
or l.RequestedURL like'%/uo_wmts_norm_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml%'
or l.RequestedURL like'%/uo_wmts_orto_sjtsk/MapServer/WMTS/1.0.0/WMTSCapabilities.xml%'
or l.RequestedURL like'%/uo_wmts_orto_wm/MapServer/WMTS/1.0.0/WMTSCapabilities.xml%'

