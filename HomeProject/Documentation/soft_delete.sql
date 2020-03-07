IF db_id('magord_softdelete') IS NOT NULL BEGIN
    USE master
    DROP DATABASE "magord_softdelete"
END
GO

CREATE DATABASE "magord_softdelete"
GO

USE "magord_softdelete"
GO


CREATE TABLE Person(
    PersonId INT NOT NULL,
    FirstName VARCHAR(256) NOT NULL,
    LastName VARCHAR(256) NOT NULL,
    Sex CHAR(1) NOT NULL,
    CreatedAt DATETIME2,
    DeletedAt DATETIME2,
    CONSTRAINT PK_Person PRIMARY KEY (PersonId, DeletedAt)
);

CREATE TABLE Contact(
    ContactId INT NOT NULL,
    PersonId INT NOT NULL,
    PersonDeletedAt DATETIME2,
    ContactName VARCHAR(256) NOT NULL,
    CreatedAt DATETIME2,
    DeletedAt DATETIME2,
    CONSTRAINT PK_Contact PRIMARY KEY (ContactId, DeletedAt),
    CONSTRAINT FK_Person_Contact FOREIGN KEY (PersonId, PersonDeletedAt) REFERENCES Person(PersonId, DeletedAt) ON UPDATE CASCADE
);


INSERT INTO Person(PersonId, FirstName, LastName, Sex, CreatedAt, DeletedAt) VALUES 
            (1 ,'Markus', 'Tuul', 'M', '2020-01-01', '9999-12-31'),
            (2 ,'Laura', 'Tuul', 'N', '2020-02-02', '9999-12-31'),
            (3 ,'Marek', 'Saar', 'M', '2020-01-01', '9999-12-31'),
            (4 ,'Liisa', 'Mets', 'N', '2020-01-01', '9999-12-31')

INSERT INTO Contact(ContactId, PersonId, PersonDeletedAt, ContactName, CreatedAt, DeletedAt) VALUES
            (1, 3, '9999-12-31', 'Marek.Saar@gmail.com', '2020-01-01', '9999-12-31'),
            (2, 3, '9999-12-31', 'MarekS', '2020-01-01', '9999-12-31'),
            (3, 4, '9999-12-31', '53657403', '2020-01-01', '9999-12-31'),
            (4, 4, '9999-12-31', 'LiisaMets123', '2020-01-01', '9999-12-31')


SELECT * FROM PERSON;

SELECT * FROM CONTACT


----------------------------------------------------------------------------------------------

-- CRUD with single table

--------------------------------------

-- Soft delete Person

DECLARE @Time1 DATETIME2
SELECT @Time1 = '2020-02-02'

UPDATE Person SET DeletedAt = @Time1 WHERE FirstName LIKE 'Markus'

-- Results

DECLARE @CurrentTime DATETIME2
SELECT @CurrentTime = '2020-03-03'

SELECT * FROM Person WHERE Person.CreatedAt <= @CurrentTime AND (Person.DeletedAt > @CurrentTime)

--------------------------------------

-- Soft update Person

-- STEP1: Make copy of record with changed DeletedAt column (DeletedAt = Current date)

DECLARE @TimeDeleted DATETIME2
SELECT @TimeDeleted = '2020-02-03'

INSERT INTO Person(PersonId, FirstName, LastName, Sex, CreatedAt, DeletedAt) VALUES
            (2 ,'Laura', 'Tuul', 'N', '2020-02-02', @TimeDeleted)

-- STEP2: Update old value (Sex change operation)

UPDATE Person SET Sex = 'M' WHERE FirstName LIKE 'Laura' AND DeletedAt = '9999-12-31'

-- Results

DECLARE @CurrentTime1 DATETIME2
SELECT @CurrentTime1 = '2020-03-03'

SELECT * FROM Person WHERE Person.CreatedAt <= @CurrentTime1 AND (Person.DeletedAt > @CurrentTime1)

--------------------------------------

----------------------------------------------------------------------------------------------

-- CRUD with 1:m

--------------------------------------

-- Soft update Person

-- STEP1: Make copy of record with changed DeletedAt column (DeletedAt = Current date)

DECLARE @TimeDeleted1 DATETIME2
SELECT @TimeDeleted1 = '2020-02-03'

INSERT INTO Person(PersonId, FirstName, LastName, Sex, CreatedAt, DeletedAt) VALUES
            (3, 'Marek', 'Saar', 'M', '2020-01-01', @TimeDeleted1)

-- STEP2: Update old value (Sex change operation)

UPDATE Person SET Sex = 'N' WHERE FirstName LIKE 'Marek' AND DeletedAt = '9999-12-31'

-- Results

DECLARE @CurrentTime2 DATETIME2
SELECT @CurrentTime2 = '2020-03-03'

SELECT * FROM Person JOIN Contact ON Person.PersonId = Contact.PersonId AND Person.DeletedAt = Contact.PersonDeletedAt
WHERE Person.CreatedAt <= @CurrentTime2 AND (Person.DeletedAt > @CurrentTime2) AND Contact.CreatedAt <= @CurrentTime2 AND (Contact.DeletedAt > @CurrentTime2)

--------------------------------------

-- Soft delete Contact

DECLARE @Time2 DATETIME2
SELECT @Time2 = '2020-02-02'

UPDATE Contact SET DeletedAt = @Time2 WHERE ContactName LIKE 'Marek.Saar@gmail.com'

-- Results

DECLARE @CurrentTime3 DATETIME2
SELECT @CurrentTime3 = '2020-03-03'

SELECT * FROM Person JOIN Contact ON Person.PersonId = Contact.PersonId AND Person.DeletedAt = Contact.PersonDeletedAt
WHERE Person.CreatedAt <= @CurrentTime3 AND (Person.DeletedAt > @CurrentTime3) AND Contact.CreatedAt <= @CurrentTime3 AND (Contact.DeletedAt > @CurrentTime3)

--------------------------------------

-- Soft update Contact

-- STEP1: Make copy of record with changed DeletedAt column (DeletedAt = Current date)

DECLARE @TimeDeleted2 DATETIME2
SELECT @TimeDeleted2 = '2020-02-03'

INSERT INTO Contact(ContactId, PersonId, PersonDeletedAt, ContactName, CreatedAt, DeletedAt) VALUES
            (2, 3, '9999-12-31', 'MarekS', '2020-01-01', @TimeDeleted2)

-- STEP2: Update old value

UPDATE Contact SET ContactName = 'MarekSaar123' WHERE ContactName LIKE 'MarekS' AND DeletedAt = '9999-12-31'

-- Results

DECLARE @CurrentTime4 DATETIME2
SELECT @CurrentTime4 = '2020-03-03'

SELECT * FROM Person JOIN Contact ON Person.PersonId = Contact.PersonId AND Person.DeletedAt = Contact.PersonDeletedAt
WHERE Person.CreatedAt <= @CurrentTime4 AND (Person.DeletedAt > @CurrentTime4) AND Contact.CreatedAt <= @CurrentTime4 AND (Contact.DeletedAt > @CurrentTime4)

--------------------------------------

-- Soft delete Person

DECLARE @Time3 DATETIME2
SELECT @Time3 = '2020-01-10'

DECLARE @OriginalId INT
SELECT @OriginalId = PersonId FROM Person WHERE FirstName LIKE 'Liisa'

UPDATE Contact SET DeletedAt = @Time3 WHERE PersonId = @OriginalId

UPDATE Person SET  DeletedAt = @Time3 WHERE PersonId = @OriginalId

-- Results

DECLARE @CurrentTime5 DATETIME2
SELECT @CurrentTime5 = '2020-03-03'

SELECT * FROM Person JOIN Contact ON Person.PersonId = Contact.PersonId AND Person.DeletedAt = Contact.PersonDeletedAt
WHERE Person.CreatedAt <= @CurrentTime5 AND (Person.DeletedAt > @CurrentTime5) AND Contact.CreatedAt <= @CurrentTime5 AND (Contact.DeletedAt > @CurrentTime5)

--------------------------------------

----------------------------------------------------------------------------------------------

-- CRUD with 1:0-1

--------------------------------------

CREATE TABLE Student(
    StudentId INT NOT NULL,
    FirstName VARCHAR(256) NOT NULL,
    LastName VARCHAR(256) NOT NULL,
    CreatedAt DATETIME2,
    DeletedAt DATETIME2,
    CONSTRAINT PK_Student PRIMARY KEY (StudentId, DeletedAt)
);

CREATE TABLE Address(
    AddressId INT NOT NULL,
    StudentId INT NOT NULL,
    StudentDeletedAt DATETIME2,
    Street VARCHAR (512) NOT NULL,
    Town VARCHAR(256) NOT NULL,
    PostalCode INT NOT NULL,
    CreatedAt DATETIME2,
    DeletedAt DATETIME2,
    UNIQUE(StudentId, StudentDeletedAt, DeletedAt),
    CONSTRAINT PK_Address PRIMARY KEY (AddressId, DeletedAt),
    CONSTRAINT FK_Student_Address FOREIGN KEY (StudentId, StudentDeletedAt) REFERENCES Student(StudentId, DeletedAt) ON UPDATE CASCADE
);

INSERT INTO Student(StudentId, FirstName, LastName, CreatedAt, DeletedAt) VALUES 
            (1 ,'Henry', 'Miller', '2020-01-01', '9999-12-31')

INSERT INTO Student(StudentId, FirstName, LastName, CreatedAt, DeletedAt) VALUES 
            (2 ,'Meelis', 'Kurm', '2020-01-01', '9999-12-31')

INSERT INTO Student(StudentId, FirstName, LastName, CreatedAt, DeletedAt) VALUES 
            (3 ,'Kaupo', 'Ots', '2020-01-01', '9999-12-31')

INSERT INTO Student(StudentId, FirstName, LastName, CreatedAt, DeletedAt) VALUES 
            (4 ,'Martin', 'Kits', '2020-01-01', '9999-12-31')
  




INSERT INTO Address(AddressId, StudentId, StudentDeletedAt, Street, Town, PostalCode, CreatedAt, DeletedAt) VALUES 
            (1 ,1, '9999-12-31', 'Männiku 15', 'Tallinn', '10341', '2020-01-01', '9999-12-31')

INSERT INTO Address(AddressId, StudentId, StudentDeletedAt, Street, Town, PostalCode, CreatedAt, DeletedAt) VALUES 
            (2 ,2, '9999-12-31', 'Kuldkala 1', 'Haapsalu', '10487', '2020-01-01', '9999-12-31')

INSERT INTO Address(AddressId, StudentId, StudentDeletedAt, Street, Town, PostalCode, CreatedAt, DeletedAt) VALUES 
            (3 ,3, '9999-12-31', 'Mustavee 12', 'Jõgeva', '10019', '2020-01-01', '9999-12-31')

INSERT INTO Address(AddressId, StudentId, StudentDeletedAt, Street, Town, PostalCode, CreatedAt, DeletedAt) VALUES 
            (4 ,4, '9999-12-31', 'Välja tee 20', 'Tallinn', '10371', '2020-01-01', '9999-12-31')



SELECT * FROM Student

SELECT * FROM Address

--------------------------------------

-- Soft delete Student

DECLARE @Time4 DATETIME2
SELECT @Time4 = '2020-01-20'

DECLARE @OriginalId1 INT
SELECT @OriginalId1 = StudentId FROM Student WHERE FirstName LIKE 'Henry'

UPDATE Address SET DeletedAt = @Time4 WHERE StudentId = @OriginalId1

UPDATE Student SET DeletedAt = @Time4 WHERE StudentId = @OriginalId1

-- Results

DECLARE @CurrentTime6 DATETIME2
SELECT @CurrentTime6 = '2020-03-03'

SELECT * FROM Student JOIN Address ON Student.StudentId = Address.StudentId AND Student.DeletedAt = Address.StudentDeletedAt 
WHERE Student.CreatedAt <= @CurrentTime6 AND (Student.DeletedAt > @CurrentTime6) AND Address.CreatedAt <= @CurrentTime6 AND (Address.DeletedAt > @CurrentTime6)

--------------------------------------

-- Soft update Student

-- STEP1: Make copy of record with changed DeletedAt column (DeletedAt = Current date)

DECLARE @TimeDeleted3 DATETIME2
SELECT @TimeDeleted3 = '2020-02-03'

INSERT INTO Student(StudentId, FirstName, LastName, CreatedAt, DeletedAt) VALUES 
            (3 ,'Kaupo', 'Ots', '2020-01-01', @TimeDeleted3)

-- STEP2: Update old value (Name change)

UPDATE Student SET FirstName = 'Rauno' WHERE FirstName LIKE 'Kaupo' AND DeletedAt = '9999-12-31'

-- Results

DECLARE @CurrentTime7 DATETIME2
SELECT @CurrentTime7 = '2020-03-03'

SELECT * FROM Student JOIN Address ON Student.StudentId = Address.StudentId AND Student.DeletedAt = Address.StudentDeletedAt
WHERE Student.CreatedAt <= @CurrentTime7 AND (Student.DeletedAt > @CurrentTime7) AND Address.CreatedAt <= @CurrentTime7 AND (Address.DeletedAt > @CurrentTime7)

--------------------------------------

-- Soft delete Address

DECLARE @Time5 DATETIME2
SELECT @Time5 = '2020-01-20'

UPDATE Address SET DeletedAt = @Time5 WHERE Street LIKE 'Välja tee 20'

-- Results

DECLARE @CurrentTime8 DATETIME2
SELECT @CurrentTime8 = '2020-03-03'

SELECT * FROM Student JOIN Address ON Student.StudentId = Address.StudentId AND Student.DeletedAt = Address.StudentDeletedAt WHERE Student.CreatedAt <= @CurrentTime8 AND
(Student.DeletedAt > @CurrentTime8) AND Address.CreatedAt <= @CurrentTime8 AND (Address.DeletedAt > @CurrentTime8)

--------------------------------------

-- Soft update Address

-- STEP1: Make copy of record with changed DeletedAt column (DeletedAt = Current date)

DECLARE @TimeDeleted4 DATETIME2
SELECT @TimeDeleted4 = '2020-02-03'

INSERT INTO Address(AddressId, StudentId, StudentDeletedAt, Street, Town, PostalCode, CreatedAt, DeletedAt) VALUES 
            (2 ,2, '9999-12-31', 'Kuldkala 1', 'Haapsalu', '10487', '2020-01-01', @TimeDeleted4)

-- STEP2: Update old value

UPDATE Address SET Street = 'Nõia 15', PostalCode = '10285' WHERE StudentId = 2 AND DeletedAt = '9999-12-31'

-- Results

DECLARE @CurrentTime9 DATETIME2
SELECT @CurrentTime9 = '2020-03-03'

SELECT * FROM Student JOIN Address ON Student.StudentId = Address.StudentId AND Student.DeletedAt = Address.StudentDeletedAt WHERE Student.CreatedAt <= @CurrentTime9 AND
(Student.DeletedAt > @CurrentTime9) AND Address.CreatedAt <= @CurrentTime9 AND (Address.DeletedAt > @CurrentTime9)
