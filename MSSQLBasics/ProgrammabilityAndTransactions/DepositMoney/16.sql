create proc usp_DepositMoney (@AccountId int, @MoneyAmount decimal(18,4)) 
as
 begin transaction
	update Accounts set Balance += @MoneyAmount
	where Id = @AccountId
	if(@MoneyAmount < 0)
	begin
	raiserror('Invalid money amount!',16,1)
	rollback
	return
	end
 commit

 exec usp_DepositMoney 1, 10.50

 select * from Accounts