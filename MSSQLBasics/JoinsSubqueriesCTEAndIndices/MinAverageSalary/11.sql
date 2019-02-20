select min(AverageTable.AverageSalaryByDepartment) as MinAverageSalary from
(select avg(salary) as AverageSalaryByDepartment from Employees
group by DepartmentID) as AverageTable

