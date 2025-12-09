<<<<<<< HEAD
IF DB_ID('CompanyMM') IS NOT NULL
BEGIN
    ALTER DATABASE CompanyMM SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CompanyMM;
END

CREATE DATABASE CompanyMM;
USE CompanyMM;

CREATE TABLE Employees
(
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    BirthDate DATE NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    CONSTRAINT CHK_Employees_BirthDate CHECK (BirthDate < GETDATE())
);

CREATE TABLE Projects
(
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectName NVARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    CONSTRAINT CHK_Projects_Dates CHECK (EndDate IS NULL OR EndDate >= StartDate)
);

CREATE TABLE EmployeeProjects
(
    EmployeeID INT NOT NULL,
    ProjectID INT NOT NULL,
    AssignedDate DATE NOT NULL DEFAULT (GETDATE()),
    PRIMARY KEY (EmployeeID, ProjectID),
    CONSTRAINT FK_EmployeeProjects_Employee FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID) ON DELETE CASCADE,
    CONSTRAINT FK_EmployeeProjects_Project FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE,
    CONSTRAINT CHK_EmployeeProjects_AssignedDate CHECK (AssignedDate <= GETDATE())
);

INSERT INTO Employees (FirstName, LastName, BirthDate, Email) VALUES
('Elmir', 'Aliyev', '1990-03-12', 'elmir.aliyev@example.com'),
('Aysel', 'Mammadova', '1988-07-20', 'aysel.mammadova@example.com'),
('Tural', 'Huseynov', '1995-01-05', 'tural.huseynov@example.com'),
('Leyla', 'Qasimova', '1992-11-30', 'leyla.qasimova@example.com'),
('Ramil', 'Hasanov', '1985-05-14', 'ramil.hasanov@example.com');

INSERT INTO Projects (ProjectName, StartDate, EndDate) VALUES
('Website Redesign', '2024-09-01', '2024-12-31'),
('Mobile App Development', '2024-10-15', '2025-04-30'),
('Data Warehouse', '2024-08-01', NULL);

INSERT INTO EmployeeProjects (EmployeeID, ProjectID, AssignedDate) VALUES
(1, 1, '2024-09-05'),
(1, 2, '2024-10-20'),
(1, 3, '2024-08-15'),
(2, 1, '2024-09-10'),
(3, 2, '2024-11-01'),
(4, 3, '2024-08-10'),
(5, 2, '2024-12-01');

SELECT * FROM Employees;
SELECT * FROM Projects;

SELECT e.EmployeeID, e.FirstName, e.LastName, p.ProjectID, p.ProjectName, ep.AssignedDate
FROM EmployeeProjects ep
JOIN Employees e ON ep.EmployeeID = e.EmployeeID
JOIN Projects p ON ep.ProjectID = p.ProjectID;

SELECT p.ProjectID, p.ProjectName, COUNT(ep.EmployeeID) AS AssignedEmployeeCount
FROM Projects p
LEFT JOIN EmployeeProjects ep ON p.ProjectID = ep.ProjectID
GROUP BY p.ProjectID, p.ProjectName;

SELECT e.EmployeeID, e.FirstName, e.LastName, COUNT(ep.ProjectID) AS ProjectCount
FROM Employees e
JOIN EmployeeProjects ep ON e.EmployeeID = ep.EmployeeID
GROUP BY e.EmployeeID, e.FirstName, e.LastName
HAVING COUNT(ep.ProjectID) > 2;

IF OBJECT_ID('EmployeeProjectView', 'V') IS NOT NULL
DROP VIEW EmployeeProjectView;

CREATE VIEW EmployeeProjectView AS
SELECT
    e.EmployeeID,
    CONCAT(e.FirstName, ' ', e.LastName) AS FullName,
    p.ProjectID,
    p.ProjectName,
    ep.AssignedDate
FROM EmployeeProjects ep
JOIN Employees e ON ep.EmployeeID = e.EmployeeID
JOIN Projects p ON ep.ProjectID = p.ProjectID;

SELECT * FROM EmployeeProjectView WHERE EmployeeID = 1;

IF OBJECT_ID('sp_AssignEmployeeToProject', 'P') IS NOT NULL
DROP PROCEDURE sp_AssignEmployeeToProject;

CREATE PROCEDURE sp_AssignEmployeeToProject
    @empId INT,
    @projId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeID = @empId)
    BEGIN
        RAISERROR('Employee does not exist.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Projects WHERE ProjectID = @projId)
    BEGIN
        RAISERROR('Project does not exist.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM EmployeeProjects WHERE EmployeeID = @empId AND ProjectID = @projId)
    RETURN;

    INSERT INTO EmployeeProjects (EmployeeID, ProjectID, AssignedDate)
    VALUES (@empId, @projId, CAST(GETDATE() AS DATE));
END;

IF OBJECT_ID('fn_GetProjectCount', 'FN') IS NOT NULL
DROP FUNCTION fn_GetProjectCount;

CREATE FUNCTION fn_GetProjectCount(@empId INT)
RETURNS INT
AS
BEGIN
    DECLARE @cnt INT;
    SELECT @cnt = COUNT(*) FROM EmployeeProjects WHERE EmployeeID = @empId;
    RETURN ISNULL(@cnt, 0);
END;

SELECT dbo.fn_GetProjectCount(1) AS ProjectCount;

EXEC sp_AssignEmployeeToProject @empId = 2, @projId = 3;

SELECT * FROM EmployeeProjectView WHERE EmployeeID = 2;

DELETE FROM EmployeeProjects WHERE EmployeeID = 3;

SELECT * FROM EmployeeProjectView WHERE EmployeeID = 3;
=======
IF DB_ID('CompanyMM') IS NOT NULL
BEGIN
    ALTER DATABASE CompanyMM SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CompanyMM;
END

CREATE DATABASE CompanyMM;
USE CompanyMM;

CREATE TABLE Employees
(
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    BirthDate DATE NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    CONSTRAINT CHK_Employees_BirthDate CHECK (BirthDate < GETDATE())
);

CREATE TABLE Projects
(
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectName NVARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    CONSTRAINT CHK_Projects_Dates CHECK (EndDate IS NULL OR EndDate >= StartDate)
);

CREATE TABLE EmployeeProjects
(
    EmployeeID INT NOT NULL,
    ProjectID INT NOT NULL,
    AssignedDate DATE NOT NULL DEFAULT (GETDATE()),
    PRIMARY KEY (EmployeeID, ProjectID),
    CONSTRAINT FK_EmployeeProjects_Employee FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID) ON DELETE CASCADE,
    CONSTRAINT FK_EmployeeProjects_Project FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID) ON DELETE CASCADE,
    CONSTRAINT CHK_EmployeeProjects_AssignedDate CHECK (AssignedDate <= GETDATE())
);

INSERT INTO Employees (FirstName, LastName, BirthDate, Email) VALUES
('Elmir', 'Aliyev', '1990-03-12', 'elmir.aliyev@example.com'),
('Aysel', 'Mammadova', '1988-07-20', 'aysel.mammadova@example.com'),
('Tural', 'Huseynov', '1995-01-05', 'tural.huseynov@example.com'),
('Leyla', 'Qasimova', '1992-11-30', 'leyla.qasimova@example.com'),
('Ramil', 'Hasanov', '1985-05-14', 'ramil.hasanov@example.com');

INSERT INTO Projects (ProjectName, StartDate, EndDate) VALUES
('Website Redesign', '2024-09-01', '2024-12-31'),
('Mobile App Development', '2024-10-15', '2025-04-30'),
('Data Warehouse', '2024-08-01', NULL);

INSERT INTO EmployeeProjects (EmployeeID, ProjectID, AssignedDate) VALUES
(1, 1, '2024-09-05'),
(1, 2, '2024-10-20'),
(1, 3, '2024-08-15'),
(2, 1, '2024-09-10'),
(3, 2, '2024-11-01'),
(4, 3, '2024-08-10'),
(5, 2, '2024-12-01');

SELECT * FROM Employees;
SELECT * FROM Projects;

SELECT e.EmployeeID, e.FirstName, e.LastName, p.ProjectID, p.ProjectName, ep.AssignedDate
FROM EmployeeProjects ep
JOIN Employees e ON ep.EmployeeID = e.EmployeeID
JOIN Projects p ON ep.ProjectID = p.ProjectID;

SELECT p.ProjectID, p.ProjectName, COUNT(ep.EmployeeID) AS AssignedEmployeeCount
FROM Projects p
LEFT JOIN EmployeeProjects ep ON p.ProjectID = ep.ProjectID
GROUP BY p.ProjectID, p.ProjectName;

SELECT e.EmployeeID, e.FirstName, e.LastName, COUNT(ep.ProjectID) AS ProjectCount
FROM Employees e
JOIN EmployeeProjects ep ON e.EmployeeID = ep.EmployeeID
GROUP BY e.EmployeeID, e.FirstName, e.LastName
HAVING COUNT(ep.ProjectID) > 2;

IF OBJECT_ID('EmployeeProjectView', 'V') IS NOT NULL
DROP VIEW EmployeeProjectView;

CREATE VIEW EmployeeProjectView AS
SELECT
    e.EmployeeID,
    CONCAT(e.FirstName, ' ', e.LastName) AS FullName,
    p.ProjectID,
    p.ProjectName,
    ep.AssignedDate
FROM EmployeeProjects ep
JOIN Employees e ON ep.EmployeeID = e.EmployeeID
JOIN Projects p ON ep.ProjectID = p.ProjectID;

SELECT * FROM EmployeeProjectView WHERE EmployeeID = 1;

IF OBJECT_ID('sp_AssignEmployeeToProject', 'P') IS NOT NULL
DROP PROCEDURE sp_AssignEmployeeToProject;

CREATE PROCEDURE sp_AssignEmployeeToProject
    @empId INT,
    @projId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeID = @empId)
    BEGIN
        RAISERROR('Employee does not exist.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Projects WHERE ProjectID = @projId)
    BEGIN
        RAISERROR('Project does not exist.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM EmployeeProjects WHERE EmployeeID = @empId AND ProjectID = @projId)
    RETURN;

    INSERT INTO EmployeeProjects (EmployeeID, ProjectID, AssignedDate)
    VALUES (@empId, @projId, CAST(GETDATE() AS DATE));
END;

IF OBJECT_ID('fn_GetProjectCount', 'FN') IS NOT NULL
DROP FUNCTION fn_GetProjectCount;

CREATE FUNCTION fn_GetProjectCount(@empId INT)
RETURNS INT
AS
BEGIN
    DECLARE @cnt INT;
    SELECT @cnt = COUNT(*) FROM EmployeeProjects WHERE EmployeeID = @empId;
    RETURN ISNULL(@cnt, 0);
END;

SELECT dbo.fn_GetProjectCount(1) AS ProjectCount;

EXEC sp_AssignEmployeeToProject @empId = 2, @projId = 3;

SELECT * FROM EmployeeProjectView WHERE EmployeeID = 2;

DELETE FROM EmployeeProjects WHERE EmployeeID = 3;

SELECT * FROM EmployeeProjectView WHERE EmployeeID = 3;
>>>>>>> c14c9742bc1e765dbec0ffc6e8d063c69789b72c
