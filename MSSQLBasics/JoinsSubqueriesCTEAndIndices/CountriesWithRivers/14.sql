select top(5) c.CountryName,r.RiverName from Countries as c
left join CountriesRivers as cr on cr.CountryCode = c.CountryCode
left join Rivers as r on r.Id = cr.RiverId
where c.ContinentCode = 'AF'
order by c.CountryName asc

