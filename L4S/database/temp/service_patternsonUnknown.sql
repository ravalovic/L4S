select count(*) from CATUnknownService l where 
-- A 2.1.2
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
--478440
go
-- A 2.1.3
select  count(*) from CATUnknownService l where 
   l.RequestedURL like '%/Subjects?$select=Id&$filter=IdNo%'
or l.RequestedURL like '%/Subjects(%)%'
or l.RequestedURL like '%/Kn.Search%'
--8345
go
--A 2.1.4
select  count(*) from CATUnknownService l where 
    l.RequestedURL like'%/Spaces(%)?$select=%'
 or l.RequestedURL like'%/Spaces/?&$filter=CadastralUnit/Code eq%Construction/Folio/No eq%'    
 or l.RequestedURL like'%/Spaces/?&$filter=Entrance/HouseNo eq%CadastralUnit/Code eq%' 
 or l.RequestedURL like'%/Spaces/?&$filter=Entrance/HouseNo eq%CadastralUnit/Code eq%NonresidentialNo eq%' 
 or l.RequestedURL like'%/Spaces/?&$filter=Entrance/HouseNo eq%CadastralUnit/Code eq%FlatNo eq%' 
 or l.RequestedURL like'%/Spaces/?&$filter=Municipality/Code eq%Construction/Folio/No eq%' 
 or l.RequestedURL like'%/Spaces/?&$filter=Municipality/Code eq%Entrance/HouseNo eq%'   
 or l.RequestedURL like'%/Spaces/?&$filter=Municipality/Code eq%Entrance/HouseNo eq eq%NonresidentialNo eq%' 
 or l.RequestedURL like'%/Spaces/?&$filter=Municipality/Code eq%Entrance/HouseNo eq eq%FlatNo eq%' 
 or l.RequestedURL like'%/Constructions(%)?$select=%' 
 or l.RequestedURL like'%/Constructions/?&$filter=CadastralUnit/Code eq%Folio/No eq%'
 or l.RequestedURL like'%/Constructions/?&$filter=CadastralUnit/Code eq%ParcelsC/any(%'
 or l.RequestedURL like'%/Constructions/?&$filter=CadastralUnit/Code eq%HouseNo eq%'
 or l.RequestedURL like'%/Constructions/?&$filter=Municipality/Code/Code eq%Folio/No eq%'
 or l.RequestedURL like'%/Constructions/?&$filter=Municipality/Code/Code eq%ParcelsC/any(%'
 or l.RequestedURL like'%/Constructions/?&$filter=Municipality/Code/Code eq%HouseNo eq%'
 or l.RequestedURL like'%/ParcelsC(%)?$select=%'
 or l.RequestedURL like'%/ParcelsC/?&$filter=CadastralUnit/Code eq%Folio/No eq%'
 or l.RequestedURL like'%/ParcelsC/?&$filter=CadastralUnit/Code eq%No eq%'
 or l.RequestedURL like'%/ParcelsC/?&$filter=Municipality/Code eq%Folio/No eq%'
 or l.RequestedURL like'%/ParcelsC/?&$filter=Municipality/Code eq%No eq%'
 or l.RequestedURL like'%/ParcelsE(%)?$select=%'
 or l.RequestedURL like'%/ParcelsE/?&$filter=CadastralUnit/Code eq%Folio/No eq%'
 or l.RequestedURL like'%/ParcelsE/?&$filter=CadastralUnit/Code eq%No eq%'
 or l.RequestedURL like'%/ParcelsE/?&$filter=Municipality/Code eq%Folio/No eq%'
 or l.RequestedURL like'%/ParcelsE/?&$filter=Municipality/Code eq%No eq%'
 --549587
go
 --A 2.1.5
select  count(*) from CATUnknownService l where 
    l.RequestedURL like'%/Participants(%)/?&$select=%'
 or l.RequestedURL like'%/Folios(%)/ParcelsC%'
 or l.RequestedURL like'%/Folios(%)/ParcelsE%'
 or l.RequestedURL like'%/Folios(%)/Constructions%'
 or l.RequestedURL like'%/Folios(%)/SpaceRecords%'
 or l.RequestedURL like'%/Folios(%)/LegalRightRecords%'
 or l.RequestedURL like'%/Participants/?$filter=CadastralUnit/Code eq % and OwnershipRecord/Folio/ParcelsC/any(%'
 or l.RequestedURL like'%/Participants/?$filter=CadastralUnit/Code eq % and OwnershipRecord/Folio/ParcelsE/any(%'
 or l.RequestedURL like'%/Participants/?$filter=CadastralUnit/Code eq % and Subjects/any(%'
 or l.RequestedURL like'%/Participants/?$filter=CadastralUnit/Code eq % and OwnershipRecord/Folio/Constructions/any(p: p/HouseNo eq %)&$select%'
 or l.RequestedURL like'%/Participants/?$filter=CadastralUnit/Code eq % and OwnershipRecord/Folio/Constructions/any(p: p/HouseNo eq %) and OwnershipRecord/SpaceRecords/any(%p/Space/FlatNo%'
 or l.RequestedURL like'%/Participants/?$filter=CadastralUnit/Code eq % and OwnershipRecord/Folio/Constructions/any(p: p/HouseNo eq %) and OwnershipRecord/SpaceRecords/any(%p/Space/NonresidentialNo%'
 or l.RequestedURL like'%/Participants/?$filter=CadastralUnit/Code eq % and OwnershipRecord/Folio/ParcelsE/any(%'
 or l.RequestedURL like'%/Folios(%)/?&$select=Id,ValidTo,No&$expand=CadastralUnit%'
 or l.RequestedURL like'%/OwnershipRecords(%)/LegalRightRecords%'  
 --79003
 go
 --A 2.1.6
select  count(*) from CATUnknownService l where 
   l.RequestedURL like'%/CadastralUnits?$filter=Id eq%&$select%'
or l.RequestedURL like'%/CadastralUnits?$filter=Code eq%&$select%'
or l.RequestedURL like'%/CadastralUnits?$filter=Municipality/Code eq%&$select%'
or l.RequestedURL like'%/CadastralUnits?$filter=Name eq%&$select=%'
or l.RequestedURL like'%/CadastralUnits?&$select%'
or l.RequestedURL like'%/Regions?$filter=Id eq%&$select%'
or l.RequestedURL like'%/Regions?$filter=Code eq%&$select%'
or l.RequestedURL like'%/Regions?$filter=Name eq%&$select%'
or l.RequestedURL like'%/Regions?&$select%'
or l.RequestedURL like'%/Municipalities?$filter=Id eq%&$select%'
or l.RequestedURL like'%/Municipalities?$filter=Code eq%&$select%'  
or l.RequestedURL like'%/Municipalities?$filter=District/Code eq%&$select%'
or l.RequestedURL like'%/Municipalities?$filter=Name eq%&$select%'
or l.RequestedURL like'%/Municipalities?&$select%'
or l.RequestedURL like'%/Districts?$filter=Id eq%&$select%'
or l.RequestedURL like'%/Districts?$filter=Region/Code eq%>&$select%'  
or l.RequestedURL like'%/Districts?$filter=Code eq%&$select%'
or l.RequestedURL like'%/Districts?$filter=Name eq%&$select%'
or l.RequestedURL like'%/Districts?&$select%'
or l.RequestedURL like'%/OriginalCadastralUnits?$filter=Id eq%&$select%'
or l.RequestedURL like'%/OriginalCadastralUnits?$filter=CadastralUnit/Code eq%and Id gt 0&$select%'  
or l.RequestedURL like'%/OriginalCadastralUnits?$filter=Id gt 0&$select%'
 --2412
 go
 --A 2.1.7
select  count(*) from CATUnknownService l where 
   l.RequestedURL like'%/ProtectedProperties?$filter=Id gt 0&$select%'
or l.RequestedURL like'%/NonresidentialSpaceTypes?$filter=Id gt 0&$select%'
or l.RequestedURL like'%/LandUses?$filter=Id gt 0&$select%'  
or l.RequestedURL like'%/OwnershipTypes?$filter=Id gt 0&$select%'
or l.RequestedURL like'%/SpaceTypes?$filter=Id gt 0&$select%'
or l.RequestedURL like'%/ConstructionTypes?$filter=Id gt 0&$select%'
or l.RequestedURL like'%/Affiliations?$filter=Id gt 0&$select%'
or l.RequestedURL like'%/SharedProperties?$filter=Id gt 0&$select%'
or l.RequestedURL like'%/Utilisations?$filter=Id gt 0&$select%'  
or l.RequestedURL like'%/OwnerTypes?$filter=Id gt 0&$select%'
or l.RequestedURL like'%/FolioPartTypes?$filter=Id gt 0&$select%'
or l.RequestedURL like'%/ParticipantTypes?$filter=Id gt 0&$select%'
or l.RequestedURL like'%/Localizations?$filter=Id gt 0&$select%'
or l.RequestedURL like'%/ConstructionLocalizations?$filter=Id gt 0&$select%'
--1042
go
--A.2.1.9
select  count(*) from CATUnknownService l where 
   l.RequestedURL like'%/Folios/?$filter=Id eq %&$select%'  
or l.RequestedURL like'%/Folios/?$filter=CadastralUnit/Code eq % and No eq %&$select%'
or l.RequestedURL like'%/Folios?$filter=Constructions/any(d:d/HouseNo eq %) and CadastralUnit/Code eq %&$select%'
or l.RequestedURL like'%/Folios?$filter=ParcelsC/any(d:d/No eq %) and CadastralUnit/Code eq %&$select%'
or l.RequestedURL like'%/Folios?$filter=ParcelsE/any(d:d/No eq %) and CadastralUnit/Code eq %&$select%'
or l.RequestedURL like'%/GeneratePrf?prfId=%&outputType=html%'
or l.RequestedURL like'%/GeneratePrf?prfNumber=%&cadastralUnitCode=%&outputType=html%'  
or l.RequestedURL like'%/GeneratePrf?houseNo=%&cadastralUnitCode=%&outputType=html%'
or l.RequestedURL like'%/GeneratePrf?parcelC=%&cadastralUnitCode=%&outputType=html%'
or l.RequestedURL like'%/GeneratePrf?parcelE=%&cadastralUnitCode=%&outputType=html%'
or l.RequestedURL like'%/GeneratePrf?prfId=%&outputType=pdf%'
or l.RequestedURL like'%/GeneratePrf?prfNumber=%&cadastralUnitCode=%&outputType=pdf%'  
or l.RequestedURL like'%/GeneratePrf?houseNo=%&cadastralUnitCode=%&outputType=pdf%'
or l.RequestedURL like'%/GeneratePrf?parcelC=%&cadastralUnitCode=%&outputType=pdf%'
or l.RequestedURL like'%/GeneratePrf?parcelE=%&cadastralUnitCode=%&outputType=pdf%'
--12408
go
 --A.2.1.13
select  count(*) from CATUnknownService l where 
   l.RequestedURL like'%/ParcelsC/?&$filter=CadastralUnit/Code eq % and StatusId ne 3&$select%'  
or l.RequestedURL like'%/ParcelsC/?&$filter=Municipality/Code eq % and StatusId ne 3&$select%'
or l.RequestedURL like'%/ParcelsE/?&$filter=CadastralUnit/Code eq % and StatusId ne 3&$select=%'
or l.RequestedURL like'%/ParcelsE/?&$filter=Municipality/Code eq % and StatusId ne 3&$select%'
--3732
go
 --A 2.1.14
 select  count(*) from CATUnknownService l where 
   l.RequestedURL like'%/Spaces/?&$filter=CadastralUnit/Code eq%select=Id%'  
or l.RequestedURL like'%/Spaces/?&$filter=Municipality/Code eq%select=Id%'    
or l.RequestedURL like'%/Constructions/?&$filter=CadastralUnit/Code eq%select=Id%'  
or l.RequestedURL like'%/Constructions/?&$filter=Municipality/Code eq%select=Id%'  
--3697
go

 --A 2.1.15
select  count(*) from CATUnknownService l where 
l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/CadastralUnitId eq % and p/Type/Code eq 1)&$select%'
or l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/CadastralUnit/Code eq % and p/Type/Code eq 1)&$select%'
or l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/ Municipality/Code eq % and p/Type/Code eq 1)&$select%'  
or l.RequestedURL like'%/GetSubjects?cadastralUnitCode=%&participantType=1%'
or l.RequestedURL like'%/GetSubjects?municipalityCode=%&participantType=1%'
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 1 and CadastralUnitId eq %&$select%'
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 1 and CadastralUnit/Code eq %&$select%'  
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 1 and Municipality/Code eq %&$select%'
--16663
go

 --A 2.1.16
select  count(*) from CATUnknownService l where 
   l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/CadastralUnitId eq % and p/Type/Code eq 2)&$select%'
or l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/CadastralUnit/Code eq % and p/Type/Code eq 2)&$select%'
or l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/ Municipality/Code eq % and p/Type/Code eq 2)&$select%'  
or l.RequestedURL like'%/GetSubjects?cadastralUnitCode=%&participantType=2%'
or l.RequestedURL like'%/GetSubjects?municipalityCode=%&participantType=2%'
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 2 and CadastralUnitId eq %&$select%'
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 2 and CadastralUnit/Code eq %&$select%'  
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 2 and Municipality/Code eq %&$select%'
--0
 go
 
  --A 2.1.17
select  count(*) from CATUnknownService l where 
   l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/CadastralUnitId eq % and p/Type/Code eq 3)&$select%'
or l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/CadastralUnit/Code eq % and p/Type/Code eq 3)&$select%'
or l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/ Municipality/Code eq % and p/Type/Code eq 3)&$select%'  
or l.RequestedURL like'%/GetSubjects?cadastralUnitCode=%&participantType=3%'
or l.RequestedURL like'%/GetSubjects?municipalityCode=%&participantType=3%'
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 3 and CadastralUnitId eq %&$select%'
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 3 and CadastralUnit/Code eq %&$select%'  
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 3 and Municipality/Code eq %&$select%'

 --0
 go
 
  --A 2.1.18
select  count(*) from CATUnknownService l where 
   l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/CadastralUnitId eq % and p/Type/Code eq 4)&$select%'
or l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/CadastralUnit/Code eq % and p/Type/Code eq 4)&$select%'
or l.RequestedURL like'%/Subjects?$filter=Participants/any(p: p/ Municipality/Code eq % and p/Type/Code eq 4)&$select%'  
or l.RequestedURL like'%/GetSubjects?cadastralUnitCode=%&participantType=4%'
or l.RequestedURL like'%/GetSubjects?municipalityCode=%&participantType=4%'
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 4 and CadastralUnitId eq %&$select%'
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 4 and CadastralUnit/Code eq %&$select%'  
or l.RequestedURL like'%/Participants?$filter=Type/Code eq 4 and Municipality/Code eq %&$select%'
 --0
 go
 
  --A 2.1.20
select  count(*) from CATUnknownService l where 
   l.RequestedURL like'%/sk/Vgi/Download/%'
or l.RequestedURL like'%/ParcelMaps?$filter=CadastralUnit/Code eq %&$select%'
 --52
 go
 
 --IOM
select  count(*) from CATUnknownService l where 
l.RequestedURL like'%/soap%'
--14
go
 
--1 155 395
--4 963 684