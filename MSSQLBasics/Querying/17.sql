select t.Id,
a.FirstName + ' ' + isnull(a.MiddleName + ' ','')+ a.LastName as [Full Name],
ct.Name as [From],
c.Name as [To],
case
	when t.CancelDate is null 
	then convert(varchar,DATEDIFF(DAY,t.ArrivalDate,t.ReturnDate)) + ' days'
	else 'Canceled'
end as Duration
 from Trips as t
join AccountsTrips as ac on ac.TripId = t.Id
join Accounts as a on a.Id = ac.AccountId
join Cities as ct on ct.Id = a.CityId
join Rooms as r on r.Id = t.RoomId
join Hotels as h on h.Id = r.HotelId
join Cities as c on c.Id = h.CityId
order by [Full Name],t.Id