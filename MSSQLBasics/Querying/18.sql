alter function udf_GetAvailableRoom(@HotelId int, @Date date, @People int)
returns nvarchar(max)
as
begin 
declare @AvailableRoom varchar(max) = (
							select top(1) concat('Room ',r.Id,': ',r.Type,' (',r.Beds,' beds',') - $',(h.BaseRate + r.Price) * @People) from Hotels as h
							join Rooms as r on r.HotelId = h.Id
							join Trips as t on t.RoomId = r.Id
							where h.Id = @HotelId
							and @Date not between t.ArrivalDate and t.ReturnDate and
							r.Beds > @People and
							t.CancelDate is null
							order by (h.BaseRate + r.Price) * @People desc
							)
if(@AvailableRoom is null)
	begin
	return 'No rooms available'
	end
return @AvailableRoom
end

SELECT dbo.udf_GetAvailableRoom(112, '2011-12-17', 2)

SELECT dbo.udf_GetAvailableRoom(94, '2015-07-26', 3)

