select top(50) e.FirstName,e.LastName,t.Name,a.AddressText from Addresses as a
join Employees as e on e.AddressID = a.AddressID
join Towns as t on t.TownID = a.TownID
order by e.FirstName,e.LastName
