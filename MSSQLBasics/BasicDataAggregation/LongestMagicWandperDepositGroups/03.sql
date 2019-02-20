SELECT DepositGroup,
MAX(MagicWandSize) AS [LongestMagicWand]
 FROM WizzardDeposits AS e
 GROUP BY e.DepositGroup