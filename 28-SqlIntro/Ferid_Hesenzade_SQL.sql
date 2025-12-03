
CREATE DATABASE Company;
USE Company;

CREATE TABLE Employees (
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100),
    PhoneNumber VARCHAR(20),
    HireDate DATE,
    JobTitle VARCHAR(100),
    Salary DECIMAL(10,2),
    Department VARCHAR(50)
);


INSERT INTO Employees 
VALUES
('Leyla', 'Həsənova', 'leyla@company.az', '0501112233', '2021-03-15', 'HR Specialist', 2200, 'HR'),
('Arif', 'Mammadov', 'hazrat@company.az', '0502223344', '2019-07-10', 'IT Specialist', 2600, 'IT'),
('Nigar', 'Quliyeva', 'nigar@company.az', '0503334455', '2022-01-20', 'Accountant', 1800, 'Finance'),
('Rəşad', 'Əliyev', 'resad@company.az', '0504445566', '2020-05-11', 'IT Manager', 3200, 'IT'),
('Kamran', 'Səfərov', 'kamran@code.edu.az', '0505556677', '2018-11-05', 'Sales Manager', 2400, 'Sales');


SELECT * FROM Employees;


SELECT * FROM Employees WHERE Salary > 2000;


SELECT * FROM Employees WHERE Department = 'IT';

SELECT * FROM Employees ORDER BY Salary DESC;


SELECT FirstName, Salary FROM Employees;


SELECT * FROM Employees WHERE HireDate > '2020-01-01';


SELECT * FROM Employees WHERE Email LIKE '%company.az%';


SELECT MAX(Salary) AS HighestSalary FROM Employees;


SELECT MIN(Salary) AS LowestSalary FROM Employees;


SELECT AVG(Salary) AS AverageSalary FROM Employees;


SELECT COUNT(*) AS TotalEmployees FROM Employees;


SELECT SUM(Salary) AS TotalSalary FROM Employees;


SELECT Department, COUNT(*) AS EmployeeCount FROM Employees GROUP BY Department;


SELECT Department, AVG(Salary) AS AvgSalaryc FROM Employees GROUP BY Department;


SELECT Department, MAX(Salary) AS MaxSalary FROM Employees GROUP BY Department;

UPDATE Employees SET Salary = 2800 WHERE EmployeeID = 1;


UPDATE Employees SET Salary = Salary * 1.10 WHERE Department = 'IT';

UPDATE Employees SET JobTitle = 'HR Meneceri' WHERE FirstName = 'Leyla' AND LastName = 'Həsənova';


DELETE FROM Employees WHERE EmployeeID = 5;



DELETE FROM Employees WHERE Salary < 1500;


SELECT * FROM Employees WHERE FirstName LIKE '%a%';


SELECT * FROM Employees WHERE Salary BETWEEN 2000 AND 2500;


SELECT * FROM Employees WHERE Department IN ('Finance', 'IT');
