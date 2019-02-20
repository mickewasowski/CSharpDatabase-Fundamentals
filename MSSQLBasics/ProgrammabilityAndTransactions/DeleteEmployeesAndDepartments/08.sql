create proc usp_DeleteEmployeesFromDepartment(@departmentId INT) 
as
alter table Departments
alter column ManagerID int

delete from EmployeesProjects
where EmployeeID in (select EmployeeID from Employees where DepartmentID = @departmentId)

update Departments
set ManagerID = null
where DepartmentID = @departmentId

update Employees 
set ManagerID = null
where ManagerID in (select EmployeeID from Employees where DepartmentID = @departmentId)

delete from Employees
where DepartmentID = @departmentId

delete from Departments
where DepartmentID = @departmentId

select count(*)
from Employees
where DepartmentID = @departmentId


exec usp_DeleteEmployeesFromDepartment 7




