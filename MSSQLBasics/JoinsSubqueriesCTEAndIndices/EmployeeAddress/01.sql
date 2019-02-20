select top(5) e.EmployeeID,e.JobTitle,a.AddressID,a.AddressText from Addresses as a
join Employees as e on a.AddressID = e.AddressID
order by a.AddressID
