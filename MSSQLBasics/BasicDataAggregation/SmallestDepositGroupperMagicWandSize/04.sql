SELECT TOP(2) DepositGroup FROM WizzardDeposits AS e
GROUP BY e.DepositGroup
ORDER BY AVG(e.MagicWandSize)
