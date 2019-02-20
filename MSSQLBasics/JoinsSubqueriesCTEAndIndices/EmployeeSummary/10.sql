select top(50) emp.EmployeeID,emp.FirstName + ' '+ emp.LastName as [EmployeeName],mng.FirstName + ' ' + mng.LastName as [ManagerName],dep.Name as [DepartmentName] from Employees as emp
join Employees as mng on mng.EmployeeID = emp.ManagerID
join Departments as dep on dep.DepartmentID = emp.DepartmentID
order by emp.EmployeeID