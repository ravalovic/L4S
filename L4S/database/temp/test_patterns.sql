--and dbo.RegexMatch(l.RequestedURL, '.*(Folio|HouseNo|Entrance).*') is not null  )
select l.RequestedURL, l.UserIPAddress from STLogImport l where 
   l.RequestedURL like'%/Participants?%'--?&$filter=CadastralUnit/Code%'  
--or l.RequestedURL like'%/Spaces/?&$filter=Municipality/Code%20eq%select=Id%'    
--or l.RequestedURL like'%/Constructions/?&$filter=CadastralUnit/Code%20eq%select=Id%'  
--or l.RequestedURL like'%/Constructions/?&$filter=Municipality/Code%20eq%select=Id%'  





--OK
-- l.RequestedURL like'%/Participants(%)?_24select=%'  


