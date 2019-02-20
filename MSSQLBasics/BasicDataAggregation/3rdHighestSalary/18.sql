--SELECT DISTINCT k.DepartmentID,k.Salary FROM
--(SELECT DepartmentID,Salary,DENSE_RANK() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) AS [ThirdHighestSalary] FROM Employees) AS k
--WHERE k.ThirdHighestSalary = 3

SELECT k.DepartmentID,k.Salary FROM
(SELECT DepartmentID,Salary,DENSE_RANK() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) AS [ThirdHighestSalary] FROM Employees) AS k
WHERE k.ThirdHighestSalary = 3
GROUP BY k.DepartmentID,k.Salary 