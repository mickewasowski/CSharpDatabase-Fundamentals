create function ufn_IsWordComprised(@setOfLetters varchar(max), @word varchar(max))
returns bit
begin
declare @count int = 1

while(@count <= len(@word))
begin
	declare @currentChar char(1) = substring(@word,@count,1)
	declare @charIndex int = charindex(@currentChar,@setOfLetters)

	if(@charIndex < 1)
	begin
	return 0
	end

	set @count += 1
end
	return 1
end
go
select 'ghiuerwyt' as SetOfLetters,'with' as Word,dbo.ufn_IsWordComprised('ghiuerwyt','with') as Result

--select dbo.ufn_IsWordComprised('ghiuerwyt','with')