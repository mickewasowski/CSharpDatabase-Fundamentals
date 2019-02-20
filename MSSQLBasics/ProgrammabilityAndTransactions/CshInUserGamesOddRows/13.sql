create function ufn_CashInUsersGames(@gameName varchar(max))
returns table
as
return (
		select sum(k.Cash) as [SumCash] from
		(select g.Name,ug.Cash,
		ROW_NUMBER() over (partition by g.Name order by ug.Cash desc) as [Rows]
		 from Games as g
		join UsersGames as ug on ug.GameId = g.Id) as k
		where k.Rows % 2 <> 0 and k.Name = @gameName
)
go
select * from dbo.ufn_CashInUsersGames('Love in a mist')
