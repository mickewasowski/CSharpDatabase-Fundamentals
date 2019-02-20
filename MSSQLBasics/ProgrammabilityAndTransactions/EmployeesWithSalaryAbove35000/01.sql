create proc usp_EmployeesWithSalaryAbove35000
as
select FirstName,LastName from Employees
where Salary > 35000

exec usp_EmployeesWithSalaryAbove35000