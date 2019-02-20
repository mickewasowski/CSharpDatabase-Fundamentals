create proc usp_WithdrawMoney (@AccountId int , @MoneyAmount decimal(18,4))
as
 begin transaction
	update Accounts set Balance -= @MoneyAmount
	where Id = @AccountId
	if(@MoneyAmount < 0)
	begin
	raiserror('Invalid money amount!',16,1)
	rollback
	end
 commit 

 exec usp_WithdrawMoney 1, 10.50

 select * from Accounts