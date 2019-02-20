create table Logs(
LogId int identity,
AccountId int foreign key references Accounts(Id),
OldSum decimal(15,2),
NewSum decimal(15,2)
)

go

create trigger tr_LogEntryInfo on Accounts for update
as
	declare @oldSum decimal(15,2) = (select Balance from deleted)
	declare @newSum decimal(15,2) = (select Balance from inserted)
	declare @accountId int = (select Id from inserted)

	insert into Logs(AccountId,OldSum,NewSum)
	values (@accountId,@oldSum,@newSum)	


update Accounts
set Balance += 10
where Id = 1

select * from Logs

