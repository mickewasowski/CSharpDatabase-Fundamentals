create or alter proc usp_CalculateFutureValueForAccount(@AccountId int,@interestRate decimal(18,4))
as
select a.Id,
	   ah.FirstName,
	   ah.LastName,
	   a.Balance, 
	   dbo.ufn_CalculateFutureValue(a.Balance,@interestRate,5) as [Balance in 5 years]
from AccountHolders as ah
join Accounts as a on ah.Id = a.AccountHolderId
where a.Id in (select a.Id from Accounts where a.Id = @AccountId)
group by a.Id,
	   ah.FirstName,
	   ah.LastName,
	   a.Balance

exec usp_CalculateFutureValueForAccount

--create function ufn_CalculateFutureValue(@sum decimal(18,4),@yearlyInterestRate float,@numberOfYears int)
--returns decimal(18,4)
--begin 
--	return @sum * power((1 + @yearlyInterestRate),@numberOfYears)
--end
--go
--select dbo.ufn_CalculateFutureValue(1000,0.1,5)
