select a.Id,a.Email,c.Name as City,count(act.AccountId) as Trips from Accounts as a
join AccountsTrips as act on act.AccountId=a.Id
join Trips as t on t.Id = act.TripId
join Rooms as r on r.Id = t.RoomId
join Hotels as h on h.Id = r.HotelId
join Cities as c on h.CityId = c.Id
where a.CityId = h.CityId and act.AccountId = a.Id
group by a.Id,a.Email,c.Name
order by Trips desc,a.Id