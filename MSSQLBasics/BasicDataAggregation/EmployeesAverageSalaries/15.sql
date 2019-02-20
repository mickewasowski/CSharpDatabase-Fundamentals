SELECT * INTO NewEmployeeTable
FROM Employees
WHERE Salary > 30000

DELETE FROM NewEmployeeTable
WHERE ManagerID = 42

UPDATE NewEmployeeTable
SET Salary = Salary + 5000 FROM NewEmployeeTable
WHERE DepartmentID = 1

SELECT DepartmentID,AVG(Salary) AS [AverageSalary] FROM NewEmployeeTable
GROUP BY DepartmentID