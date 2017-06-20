select l.DateOfRequest, l.RequestedURL, l.UserIPAddress from STLogImport l where 
-- A 2.1.02
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

-- A 2.1.03
select l.RequestedURL, l.UserIPAddress from STLogImport l where 
   l.RequestedURL like '%/Subjects?$select=Id&$filter=IdNo%'
or l.RequestedURL like '%/Subjects(%)%'
or l.RequestedURL like '%/Kn.Search%'

--A 2.1.04
select l.RequestedURL, l.UserIPAddress from STLogImport l where 
    l.RequestedURL like'%/Spaces(%)?_24select=%'
 or l.RequestedURL like'%/Spaces/?&$filter=CadastralUnit/Code%20eq%Construction/Folio/No%20eq%'    
 or l.RequestedURL like'%/Spaces/?&$filter=Entrance/HouseNo%20eq%CadastralUnit/Code%20eq%' 
 or l.RequestedURL like'%/Spaces/?&$filter=Entrance/HouseNo%20eq%CadastralUnit/Code%20eq%NonresidentialNo%20eq%' 
 or l.RequestedURL like'%/Spaces/?&$filter=Entrance/HouseNo%20eq%CadastralUnit/Code%20eq%FlatNo%20eq%' 
 or l.RequestedURL like'%/Spaces/?&$filter=Municipality/Code%20eq%Construction/Folio/No%20eq%' 
 or l.RequestedURL like'%/Spaces/?&$filter=Municipality/Code%20eq%Entrance/HouseNo%20eq%'   
 or l.RequestedURL like'%/Spaces/?&$filter=Municipality/Code%20eq%Entrance/HouseNo%20eq%20eq%NonresidentialNo%20eq%' 
 or l.RequestedURL like'%/Spaces/?&$filter=Municipality/Code%20eq%Entrance/HouseNo%20eq%20eq%FlatNo%20eq%' 
 or l.RequestedURL like'%/Constructions(%)?_24select=%' 
 or l.RequestedURL like'%/Constructions/?&$filter=CadastralUnit/Code%20eq%Folio/No%20eq%'
 or l.RequestedURL like'%/Constructions/?&$filter=CadastralUnit/Code%20eq%ParcelsC/any(%'
 or l.RequestedURL like'%/Constructions/?&$filter=CadastralUnit/Code%20eq%HouseNo%20eq%'
 or l.RequestedURL like'%/Constructions/?&$filter=Municipality/Code/Code%20eq%Folio/No%20eq%'
 or l.RequestedURL like'%/Constructions/?&$filter=Municipality/Code/Code%20eq%ParcelsC/any(%'
 or l.RequestedURL like'%/Constructions/?&$filter=Municipality/Code/Code%20eq%HouseNo%20eq%'
 or l.RequestedURL like'%/ParcelsC(%)?_24select=%'
 or l.RequestedURL like'%/ParcelsC/?&$filter=CadastralUnit/Code%20eq%Folio/No%20eq%'
 or l.RequestedURL like'%/ParcelsC/?&$filter=CadastralUnit/Code%20eq%No%20eq%'
 or l.RequestedURL like'%/ParcelsC/?&$filter=Municipality/Code%20eq%Folio/No%20eq%'
 or l.RequestedURL like'%/ParcelsC/?&$filter=Municipality/Code%20eq%No%20eq%'
 or l.RequestedURL like'%/ParcelsE(%)?_24select=%'
 or l.RequestedURL like'%/ParcelsE/?&$filter=CadastralUnit/Code%20eq%Folio/No%20eq%'
 or l.RequestedURL like'%/ParcelsE/?&$filter=CadastralUnit/Code%20eq%No%20eq%'
 or l.RequestedURL like'%/ParcelsE/?&$filter=Municipality/Code%20eq%Folio/No%20eq%'
 or l.RequestedURL like'%/ParcelsE/?&$filter=Municipality/Code%20eq%No%20eq%'

 --A 2.1.14
 select l.RequestedURL, l.UserIPAddress from STLogImport l where 
   l.RequestedURL like'%/Spaces/?&$filter=CadastralUnit/Code%20eq%select=Id%'  
or l.RequestedURL like'%/Spaces/?&$filter=Municipality/Code%20eq%select=Id%'    
or l.RequestedURL like'%/Constructions/?&$filter=CadastralUnit/Code%20eq%select=Id%'  
or l.RequestedURL like'%/Constructions/?&$filter=Municipality/Code%20eq%select=Id%'  


