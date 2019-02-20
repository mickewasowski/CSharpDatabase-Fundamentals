create function ufn_GetSalaryLevel(@salary DECIMAL(18,4))
returns nvarchar(10)
as
begin
  declare @salarylevel varchar(10)
  if(@salary < 30000)
  set @salarylevel = 'Low'
  else if(@salary >= 30000 and @salary <= 50000)
  set @salarylevel = 'Average'
  else
  set @salarylevel = 'High'
  return @salarylevel
end

go
select FirstName,LastName,Salary,dbo.ufn_GetSalaryLevel(Salary) as SalaryLevel from Employees