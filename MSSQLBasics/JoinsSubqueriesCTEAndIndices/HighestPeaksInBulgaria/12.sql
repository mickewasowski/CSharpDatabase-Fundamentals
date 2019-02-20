select mc.CountryCode,m.MountainRange,p.PeakName,p.Elevation from Countries as country 
join MountainsCountries as mc on mc.CountryCode = country.CountryCode 
join Mountains as m on mc.MountainId = m.Id
join Peaks as p on p.MountainId = m.Id
where country.CountryCode = 'BG' and p.Elevation > 2835
order by p.Elevation desc