create or alter proc usp_GetHoldersFullName
as
select CONCAT(FirstName,' ',LastName) as [Full Name] from AccountHolders
--select FirstName + ' ' + LastName as [Full Name] from AccountHolders

exec usp_GetHoldersFullName
