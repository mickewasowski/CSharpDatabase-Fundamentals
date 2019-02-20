create proc usp_EmployeesBySalaryLevel(@input varchar(10))
as
select FirstName,LastName from Employees
where dbo.ufn_GetSalaryLevel(Salary) = @input

exec usp_EmployeesBySalaryLevel 'High'