select e.EmployeeID,e.FirstName,
case
	when year(p.StartDate) >= 2005 then NULL
	else p.Name
end as [ProjectName]
 from Employees as e
join EmployeesProjects as ep on e.EmployeeID = ep.EmployeeID
join Projects as p on p.ProjectID = ep.ProjectID
where ep.EmployeeID = 24

