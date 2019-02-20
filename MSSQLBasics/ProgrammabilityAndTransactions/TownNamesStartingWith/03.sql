create proc usp_GetTownsStartingWith(@InputChar varchar(5))
as
select [Name] from Towns
where [Name] like @InputChar + '%'

exec usp_GetTownsStartingWith b





