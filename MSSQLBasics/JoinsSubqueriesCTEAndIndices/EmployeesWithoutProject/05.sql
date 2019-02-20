select top(3) e.EmployeeID,e.FirstName from Employees as e
left join EmployeesProjects as ep on e.EmployeeID = ep.EmployeeID
where ep.ProjectID is null
order by e.EmployeeID