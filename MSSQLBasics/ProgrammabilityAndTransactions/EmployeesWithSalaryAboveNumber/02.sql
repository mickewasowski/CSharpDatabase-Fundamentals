create proc usp_GetEmployeesSalaryAboveNumber(@Salary decimal(18,4))
as
select FirstName,LastName from Employees
where Salary >= @Salary

exec usp_GetEmployeesSalaryAboveNumber 48100