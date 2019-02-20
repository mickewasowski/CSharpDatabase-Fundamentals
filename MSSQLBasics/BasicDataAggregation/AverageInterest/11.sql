SELECT DepositGroup,IsDepositExpired,AVG(DepositInterest) AS [AverageInterest]  FROM WizzardDeposits
WHERE YEAR(DepositStartDate) >= 1985
--WHERE DepositStartDate > '01/01/1985'
GROUP BY DepositGroup,IsDepositExpired
ORDER BY DepositGroup DESC,IsDepositExpired