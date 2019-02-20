create proc usp_GetHoldersWithBalanceHigherThan(@input decimal (15,2))
as
select ah.FirstName,ah.LastName from AccountHolders as ah
join Accounts as a on a.AccountHolderId = ah.Id
group by ah.FirstName,ah.LastName,ah.Id
having sum(a.Balance) > @input
order by ah.FirstName,ah.LastName

exec usp_GetHoldersWithBalanceHigherThan 10000
