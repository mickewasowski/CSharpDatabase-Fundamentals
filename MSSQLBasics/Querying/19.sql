alter proc usp_SwitchRoom(@TripId int, @TargetRoomId int)
as
begin
declare @HotelId int = (select top(1) h.Id from Hotels as h
							join Rooms as r on r.HotelId = h.Id
							join Trips as t on t.RoomId = r.Id
							where t.Id = @TripId)

declare @TargetRoomHotelId int = (select top(1) h.Id from Hotels as h
							join Rooms as r on r.HotelId = h.Id
							join Trips as t on t.RoomId = r.Id
							where r.Id = @TargetRoomId)

declare @TripsAccountsBeds int = (select count(*) from AccountsTrips as ac
							  where ac.TripId = @TripId)

if(@HotelId <> @TargetRoomHotelId)
begin
raiserror('Target room is in another hotel!',16,1)
end

if(@TripsAccountsBeds > (select r.Beds from Rooms as r
						where r.Id = @TargetRoomId))
begin
raiserror('Not enough beds in target room!',16,2)
end

update Trips
set RoomId = @TargetRoomId
where Id = @TripId
end

EXEC usp_SwitchRoom 10, 11
SELECT RoomId FROM Trips WHERE Id = 10

EXEC usp_SwitchRoom 10, 7

EXEC usp_SwitchRoom 10, 8
