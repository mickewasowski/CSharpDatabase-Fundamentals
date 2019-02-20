select 
case
	when MiddleName is null
	then FirstName + ' ' + LastName
	else FirstName + ' ' + MiddleName + ' ' + LastName
end as [Full Name]
,year(BirthDate) from Accounts
where year(BirthDate) > 1991
order by year(BirthDate) desc, FirstName