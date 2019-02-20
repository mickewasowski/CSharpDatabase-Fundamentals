select c.Name as City,count(h.Name) as Hotels from Cities as c
left join Hotels as h on h.CityId = c.Id
group by c.Name
order by count(h.Name) desc,c.Name
