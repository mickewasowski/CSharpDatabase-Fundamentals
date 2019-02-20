select e.EmployeeID,e.FirstName,e.LastName,d.Name as [DepartmentName] from Departments as d
join Employees as e on e.DepartmentID = d.DepartmentID
where d.Name = 'Sales'
order by e.EmployeeID