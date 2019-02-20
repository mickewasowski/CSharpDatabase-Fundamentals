select top(5) * from
(select c.CountryName,max(p.Elevation) as HighestPeakElevation,max(r.Length) as LongestRiverLength from Countries as c
join MountainsCountries as mc on mc.CountryCode = c.CountryCode
join Mountains as m on m.Id = mc.MountainId
join Peaks as p on p.MountainId = m.Id
join CountriesRivers as cr on cr.CountryCode = c.CountryCode
join Rivers as r on r.Id = cr.RiverId
group by c.CountryName) as k
order by k.HighestPeakElevation desc,k.LongestRiverLength desc, k.CountryName


