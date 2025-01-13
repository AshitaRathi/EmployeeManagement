USE EmployeeManagement;

SET IDENTITY_INSERT Jobs ON;

INSERT INTO Jobs (Id, Title)
VALUES (1, 'Admin');

SET IDENTITY_INSERT Jobs OFF;

SET IDENTITY_INSERT Teams ON;

INSERT INTO Teams (Id, Name)
VALUES (1, 'Admin');

SET IDENTITY_INSERT Teams OFF;

SET IDENTITY_INSERT Employees ON;

-- Insert data into Employees table
INSERT INTO Employees (Id, Name, Password, Gender, PhoneNumber, Email, DateOfBirth, HireDate, TotalExperience, TeamId, JobId)
VALUES 
(1, 'Admin', '123456', 'Female', '123-456-7890', 'admin@gmail.com', '1990-05-15', '2020-01-01', 5, 1, 1);

-- Disable IDENTITY_INSERT for Employees table
SET IDENTITY_INSERT Employees OFF;
