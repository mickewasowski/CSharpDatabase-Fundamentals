select count(c.CountryCode) as CountryCode from Countries as c
left join MountainsCountries as mc on mc.CountryCode = c.CountryCode
left join Mountains as m on m.Id = mc.MountainId
where m.Id is null