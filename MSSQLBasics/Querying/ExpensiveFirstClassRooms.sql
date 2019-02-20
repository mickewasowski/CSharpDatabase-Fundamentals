select r.Id,r.Price,h.Name as Hotel,c.Name as City from Rooms as r
join Hotels as h on r.HotelId = h.Id
join Cities as c on h.CityId = c.Id
where Type = 'First Class'
order by r.Price desc,r.Id