select k.Id,k.Email,k.CountryCode,k.Trips from
(
select a.Id,a.Email,c.CountryCode,COUNT(t.Id) as Trips,
DENSE_RANK() over (partition by c.CountryCode order by count(t.Id) desc,a.Id) as TripsRank
 from Accounts as a
join AccountsTrips as act on act.AccountId = a.Id
join Trips as t on t.Id = act.TripId
join Rooms as r on r.Id = t.RoomId
join Hotels as h on h.Id = r.HotelId
join Cities as c on c.Id = h.CityId 
group by a.Id,a.Email,c.CountryCode
) as k
where k.TripsRank = 1
order by k.Trips desc,k.Id
--DENSE_RANK() over (partition by c.CountryCode order by count(t.Id) desc,a.Id 
-- in dense_rank when we have 2 equal value the have the same rank this is why we need to add in the ordering part another constraint which secures that we'll have only one value


