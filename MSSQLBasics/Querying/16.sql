select TripId,sum(Luggage) as Luggage,
case
	when sum(Luggage) > 5 then CONCAT('$',sum(Luggage) * 5)
	else '$0'
end as Fee
 from AccountsTrips
 where Luggage > 0
group by TripId
order by sum(Luggage) desc
