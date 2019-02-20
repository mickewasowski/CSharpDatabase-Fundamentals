--Find the top 50 games ordered by start date, then by name of the game. 
--Display only games from 2011 and 2012 year. 
--Display start date in the format “yyyy-MM-dd”. 

SELECT TOP(50)[Name],FORMAT([Start],'yyyy-MM-dd') AS [Start] FROM Games
WHERE YEAR([Start]) IN(2011,2012)
ORDER BY [Start],[Name]