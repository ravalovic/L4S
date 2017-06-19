select l.RequestedURL, l.UserIPAddress from STLogImport l where 
l.RequestedURL like'%/Spaces(%)?%'
--or (l.RequestedURL like'%/Spaces__24%'and dbo.RegexMatch(l.RequestedURL, '.*(Folio|HouseNo|Entrance).*') is not null and dbo.RegexMatch(l.RequestedURL, '.*(Parcels).*') is null )
--or l.RequestedURL like'%/ParcelsC(%)__24%'
--or l.RequestedURL like'%/ParcelsC__24%'
--or l.RequestedURL like'%/ParcelsE(%)__24%'
--or l.RequestedURL like'%/ParcelsE__24%'
or l.RequestedURL like'%/Constructions(%)__24%'
or (l.RequestedURL like'%/Constructions__24%%' and dbo.RegexMatch(l.RequestedURL, '.*(Folio|HouseNo|Entrance).*') is not null  )