select a.Id,FirstName + ' ' + LastName as FullName,max(DATEDIFF(DAY,ArrivalDate,ReturnDate))as LongestTrip,min(DATEDIFF(DAY,ArrivalDate,ReturnDate))as ShortestTrip from Trips as t
join AccountsTrips as act on act.TripId = t.Id
join Accounts as a on a.Id = act.AccountId
where t.CancelDate is null and a.MiddleName is null
group by a.id,a.FirstName,a.LastName
order by LongestTrip desc,a.Id