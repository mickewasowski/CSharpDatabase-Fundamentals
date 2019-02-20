create proc usp_GetEmployeesFromTown (@input varchar(15))
as
select e.FirstName,e.LastName from Towns as t
join Addresses as a on a.TownID = t.TownID
join Employees as e on e.AddressID = a.AddressID
where t.Name = @input

exec usp_GetEmployeesFromTown 'Sofia'