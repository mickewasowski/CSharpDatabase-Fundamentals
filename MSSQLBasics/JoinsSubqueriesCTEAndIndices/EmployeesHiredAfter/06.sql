select e.FirstName,e.LastName,e.HireDate,d.Name from Departments as d
join Employees as e on e.DepartmentID = d.DepartmentID
where e.HireDate > '1/1/1999' and d.Name in ('Sales','Finance')
order by e.HireDate