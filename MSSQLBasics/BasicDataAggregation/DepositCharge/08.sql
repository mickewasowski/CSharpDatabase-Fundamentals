SELECT DepositGroup,MagicWandCreator,MIN(DepositCharge) AS [MinDepositCharge] FROM WizzardDeposits AS e
GROUP BY e.DepositGroup,e.MagicWandCreator
ORDER BY MagicWandCreator,DepositGroup