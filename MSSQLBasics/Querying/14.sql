select t.Id,h.Name as HotelName,r.Type,
case 
when t.CancelDate is null then sum(h.BaseRate + r.Price)
else 0.00
end as Revenue
 from Trips as t
 join AccountsTrips as act on act.TripId = t.Id
join Rooms as r on r.Id = t.RoomId
join Hotels as h on h.Id = r.HotelId
group by t.Id,h.Name,r.Type,t.CancelDate
order by r.Type,t.Id
