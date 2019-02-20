SELECT DepositGroup,SUM(DepositAmount) AS [TotalSum] FROM WizzardDeposits AS e
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY e.DepositGroup