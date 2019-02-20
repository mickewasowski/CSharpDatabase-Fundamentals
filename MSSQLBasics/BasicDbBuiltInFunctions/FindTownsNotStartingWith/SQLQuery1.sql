SELECT TownID,[Name] FROM Towns
--WHERE [Name] NOT LIKE '[rbd]%'
WHERE [Name] LIKE '[^rbd]%'
ORDER BY [Name]