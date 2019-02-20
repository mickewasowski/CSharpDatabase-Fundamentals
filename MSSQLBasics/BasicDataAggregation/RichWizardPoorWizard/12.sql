--SELECT SUM(k.SumDiff) AS SumDifference FROM
--(SELECT DepositAmount - LEAD(DepositAmount,1) OVER (ORDER BY Id) AS SumDiff FROM WizzardDeposits) AS k

SELECT SUM(k.Diff) FROM 
(SELECT DepositAmount - (SELECT DepositAmount FROM WizzardDeposits AS w WHERE w.Id = wd.id + 1) AS Diff FROM WizzardDeposits AS wd )AS k