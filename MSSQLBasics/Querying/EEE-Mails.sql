select a.FirstName,a.LastName,format(a.BirthDate,'MM-dd-yyyy') as BirthDate,c.Name as Hometown,a.Email from Accounts as a
join Cities as c on c.Id = a.CityId
where Email like 'e%'
order by c.Name desc