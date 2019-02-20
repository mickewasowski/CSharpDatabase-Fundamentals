select top(5) e.EmployeeID,e.FirstName,p.Name from Employees as e
join EmployeesProjects as ep on ep.EmployeeID = e.EmployeeID
join Projects as p on p.ProjectID = ep.ProjectID
where p.StartDate > '2002/08/13' and p.EndDate is null
order by e.EmployeeID


