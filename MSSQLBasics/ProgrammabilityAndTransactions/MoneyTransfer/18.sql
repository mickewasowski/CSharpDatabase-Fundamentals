create proc usp_TransferMoney(@SenderId int, @ReceiverId int, @Amount decimal(18,4))
as
 begin transaction
	exec usp_WithdrawMoney @SenderId,@Amount
	exec usp_DepositMoney @ReceiverId, @Amount
 commit 

 exec usp_TransferMoney 1,2,13.805

 select * from Accounts