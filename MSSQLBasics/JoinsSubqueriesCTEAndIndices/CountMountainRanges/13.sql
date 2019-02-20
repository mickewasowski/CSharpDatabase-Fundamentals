select c.CountryCode,count(m.MountainRange) as MountainRanges from Countries as c
join MountainsCountries as mc on mc.CountryCode = c.CountryCode
join Mountains as m on mc.MountainId = m.Id
where c.CountryCode in ('BG','RU','US')
group by c.CountryCode