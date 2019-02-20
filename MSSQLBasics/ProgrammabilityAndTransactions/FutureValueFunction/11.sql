create function ufn_CalculateFutureValue(@sum decimal(18,4),@yearlyInterestRate float,@numberOfYears int)
returns decimal(18,4)
begin 
	return @sum * power((1 + @yearlyInterestRate),@numberOfYears)
end
go
select dbo.ufn_CalculateFutureValue(1000,0.1,5)
