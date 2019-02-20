create table NotificationEmails(
Id int primary key identity,
Recipient int foreign key references Accounts(Id),
[Subject] nvarchar(100),
Body nvarchar(100) 
)
go
create trigger tr_EmailInfoLog on Logs for insert
as
	declare @accountId int = (select top(1) AccountId from inserted)
	declare @oldBalance decimal(15,2) = (select top(1) OldSum from inserted)
	declare @newBalance decimal(15,2) = (select top(1) NewSum from inserted)
	
	insert into NotificationEmails(Recipient,Subject,Body)
	 values(
	@accountId,
	'Balance change for account: ' + cast(@accountId as varchar(20)),
	'On '+ convert(varchar(30),getdate(),103) + ' your balance was changed from ' + cast	(@oldBalance as varchar(20)) + ' to ' + cast(@newBalance as varchar(20)) + '.'
	)
go

update Accounts
set Balance += 10
where Id = 1

select * from NotificationEmails