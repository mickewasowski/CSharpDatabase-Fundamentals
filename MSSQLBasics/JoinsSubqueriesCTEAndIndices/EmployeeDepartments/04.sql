select top(5) e.EmployeeID,e.FirstName,e.Salary,d.Name as [DepartmentName] from Departments as d
join Employees as e on e.DepartmentID = d.DepartmentID
where e.Salary > 15000
order by d.DepartmentID