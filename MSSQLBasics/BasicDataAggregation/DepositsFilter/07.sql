SELECT * FROM (SELECT DepositGroup,SUM(DepositAmount) AS [TotalSum] FROM WizzardDeposits AS e
WHERE MagicWandCreator = 'Ollivander family' 
GROUP BY e.DepositGroup) AS [table]
WHERE [table].TotalSum < 150000
ORDER BY [table].TotalSum DESC