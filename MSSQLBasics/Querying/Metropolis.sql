select top(5) c.Id,c.Name as City,c.CountryCode as Country,count(a.Id) as Accounts from Cities as c
join Accounts as a on a.CityId = c.Id
group by c.Id,c.Name,c.CountryCode
order by COUNT(a.Id) desc