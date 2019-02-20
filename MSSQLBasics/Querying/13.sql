select top(10) c.Id,c.Name,sum(r.Price + h.BaseRate) as [Total Revenue],count(t.Id) as Trips from Cities as c
join Hotels as h on c.Id = h.CityId
join Rooms as r on r.HotelId = h.Id
join Trips as t on t.RoomId = r.Id
where year(t.BookDate) = 2016
group by c.Name,c.Id
order by [Total Revenue] desc,Trips desc