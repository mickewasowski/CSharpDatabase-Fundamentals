SELECT DepositGroup,SUM(DepositAmount) AS [TotalSum] FROM WizzardDeposits AS e
GROUP BY e.DepositGroup
