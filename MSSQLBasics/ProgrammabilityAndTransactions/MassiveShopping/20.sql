select u.FirstName,g.Name as [Game Names],i.Name as [Item Names] from Users as u
join UsersGames as ug on u.Id = ug.UserId
join Games as g on g.Id = ug.GameId
join UserGameItems ugi on ugi.UserGameId = ug.GameId
join Items as i on i.Id = ugi.ItemId

select * from items
where MinLevel in (11,12) or MinLevel in (19,20,21)

