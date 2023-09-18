SELECT max(c.company_code),max(c.founder),count(distinct e.lead_manager_code), count(distinct e.senior_manager_code), count(distinct e.manager_code),count(distinct e.employee_code)
FROM Company c, Employee e
WHERE c.company_code = e.company_code
GROUP BY e.lead_manager_code
ORDER BY max(c.company_code);


SELECT 
COUNT(distinct E.lead_manager_code),
COUNT(distinct E.senior_manager_code),
COUNT(distinct E.manager_code),
COUNT(DISTINCT E.employee_code)
FROM
COMPANY C,EMPLOYEE E
WHERE C.company_code = E.company_code
GROUP BY e.lead_manager_code

---KIND OF JOIN THIS IS
SELECT * FROM
COMPANY,EMPLOYEE
WHERE COMPANY.COMPANY_CODE = EMPLOYEE.COMPANY_CODE

SELECT * FROM 
COMPANY,EMPLOYEE

--SIMPLE WAY
SELECT
Company.company_code,
Company.founder,
T1.LEADMANAGER,
T2.SENIORMANAGER,
T3.MANAGERCOUNT,
T4.EMPLOYEECOUNT
FROM
Company
LEFT JOIN
(SELECT company_code,COUNT(DISTINCT lead_manager_code) AS LEADMANAGER FROM Lead_Manager
GROUP BY company_code) AS T1
ON Company.company_code = T1.company_code
LEFT JOIN 
(SELECT company_code,COUNT(DISTINCT senior_manager_code) AS SENIORMANAGER FROM Senior_Manager
GROUP BY company_code) AS T2
ON T1.company_code = T2.company_code
LEFT JOIN
(SELECT company_code,COUNT(DISTINCT manager_code) AS MANAGERCOUNT FROM Manager
GROUP BY company_code) AS T3
ON T1.company_code = T3.company_code
LEFT JOIN
(SELECT company_code,COUNT(DISTINCT employee_code) AS EMPLOYEECOUNT FROM Employee
GROUP BY company_code) AS T4
ON T1.company_code = T4.company_code
ORDER BY T1.company_code

SELECT * FROM Company ORDER BY company_code;

UPDATE Company [dbo].[Lead_Manager]
SET company_code = 'C_10'
WHERE company_code = 'C_3'


SELECT 
    C.company_code,
    C.founder,
    COALESCE(LM.lead_managers_count, 0) AS total_lead_managers,
    COALESCE(SM.senior_managers_count, 0) AS total_senior_managers,
    COALESCE(M.manager_count, 0) AS total_managers,
    COALESCE(E.employee_count, 0) AS total_employees
FROM Company AS C
LEFT JOIN (
    SELECT company_code, COUNT(lead_manager_code) AS lead_managers_count
    FROM Lead_Manager
    GROUP BY company_code
) AS LM ON C.company_code = LM.company_code
LEFT JOIN (
    SELECT company_code, COUNT(senior_manager_code) AS senior_managers_count
    FROM Senior_Manager
    GROUP BY company_code
) AS SM ON C.company_code = SM.company_code
LEFT JOIN (
    SELECT company_code, COUNT(manager_code) AS manager_count
    FROM Manager
    GROUP BY company_code
) AS M ON C.company_code = M.company_code
LEFT JOIN (
    SELECT company_code, COUNT(employee_code) AS employee_count
    FROM Employee
    GROUP BY company_code
) AS E ON C.company_code = E.company_code
ORDER BY C.company_code;


SELECT [company_code],COUNT(DISTINCT [lead_manager_code]) FROM  [dbo].[Senior_Manager]
GROUP BY  [company_code]

SELECT * FROM  [dbo].[Lead_Manager]
SELECT * FROM [dbo].[Senior_Manager]

SELECT * FROM GRADES
SELECT * FROM STUDENTS
SELECT
ID,
NAME,
MARK,
 CASE  WHEN STUDENTS.MARKS < GRADES.MARK THEN GRADE
 ELSE 0 END AS GRADE
FROM
STUDENTS,GRADES



--CREATE TABLE Grades (
--    Grade INT,
--    Min_Mark INT,
--	Max_Mark INT
--);

--INSERT INTO Grades 
--VALUES
--    (10,90,100),
--    (9,80,89),
--    (8,70,79),
--    (7,60,69),
--    (6,50,59),
--    (5,40,49),
--    (4,30,39),
--    (3,20,29),
--    (2,10,19),
--    (1,0,9)


SELECT 
ID,
NAME,
MARKS,
CASE WHEN STUDENTS.MARKS BETWEEN MIN_MARK AND MAX_MARK THEN GRADE
ELSE 0 END AS GRADE
FROM
STUDENTS,GRADES

--FIRST WHERE CONDITION IS WORKING
--WHERE GRADE <> 0
 select * from Students

 SELECT
 ID,
CASE WHEN GRADE < 8 THEN NULL
ELSE [NAME] END AS NAMES,
MARKS,
CASE WHEN STUDENTS.MARKS BETWEEN MIN_MARK AND MAX_MARK THEN GRADE
ELSE 0 END AS GRADE
FROM
STUDENTS
LEFT JOIN
GRADES
ON STUDENTS.MARKS BETWEEN MIN_MARK AND MAX_MARK
ORDER BY 
CASE 
WHEN (CASE WHEN (GRADE BETWEEN 8 AND 10) THEN 'NAME'
     WHEN (GRADE BETWEEN 1 AND 7) THEN 'MARKS'
	 END) = 'NAME' THEN [NAMES]
ELSE MARKS END
--ORDER BY GRADE DESC


--SELECT CAN ABLE TO ACCESS THE GRADE AND MARKS
--GRADE IS DYNAMICALLY GENERATED BASED IN MARKS THOSUG IT ACCESSIBLE IN OTHER COLUMNS
 SELECT --STEP 2
 ID,
CASE WHEN GRADE < 8 THEN NULL
ELSE [NAME] END AS NAMES,
MARKS,
CASE WHEN STUDENTS.MARKS BETWEEN MIN_MARK AND MAX_MARK THEN GRADE
ELSE 0 END AS GRADE
FROM
STUDENTS
LEFT JOIN
GRADES
ON STUDENTS.MARKS BETWEEN MIN_MARK AND MAX_MARK --STEP 1
ORDER BY GRADE DESC,NAMES ASC,STUDENTS.MARKS ASC --STEP 3


BEGIN  TRAN

-- Create the "Orders" table
CREATE TABLE Orders (
    order_number INT PRIMARY KEY,
    customer_number INT
);

-- Insert values into the "Orders" table
INSERT INTO Orders (order_number, customer_number)
VALUES
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 3);

SELECT customer_number
(SELECT customer_number ,COUNT(customer_number) AS TOTAL  FROM 
Orders)





ROLLBACK TRAN



BEGIN TRAN 
-- Create the "Employees" table
CREATE TABLE Employees (
    emp_id INT,
    event_day DATE,
    in_time INT,
    out_time INT
);

-- Insert values into the "Employees" table
INSERT INTO Employees (emp_id, event_day, in_time, out_time)
VALUES
    (1, '2020-11-28', 4, 32),
    (1, '2020-11-28', 55, 200),
    (1, '2020-12-03', 1, 42),
    (2, '2020-11-28', 3, 33),
    (2, '2020-12-09', 47, 74);


SELECT event_day as day,emp_id,sum(out_time-in_time) total_time  FROM Employees
GROUP by emp_id,event_day
ROLLBACK TRAN


GO
BEGIN TRAN
-- Create the "Stocks" table
CREATE TABLE Stocks (
    stock_name VARCHAR(100),
    operation VARCHAR(4),
    operation_day INT,
    price DECIMAL(10, 2)
);

-- Insert values into the "Stocks" table
INSERT INTO Stocks (stock_name, operation, operation_day, price)
VALUES
    ('Leetcode', 'Buy', 1, 1000.00),
    ('Corona Masks', 'Buy', 2, 10.00),
    ('Leetcode', 'Sell', 5, 9000.00),
    ('Handbags', 'Buy', 17, 30000.00),
    ('Corona Masks', 'Sell', 3, 1010.00),
    ('Corona Masks', 'Buy', 4, 1000.00),
    ('Corona Masks', 'Sell', 5, 500.00),
    ('Corona Masks', 'Buy', 6, 1000.00),
    ('Handbags', 'Sell', 29, 7000.00),
    ('Corona Masks', 'Sell', 10, 10000.00);

SELECT
stock_name,
SUM(case when operation = 'sell' then price else -price end) as capital_gain_loss  --OVER(PARTITION BY stock_name,operation ORDER BY stock_name) AS capital_gain_loss 
FROM
Stocks
GROUP BY stock_name
ROLLBACK TRAN

---
--CREATE DATABASE STUDENTS
--CREATE TABLE Students (
--    student_id INT PRIMARY KEY,
--    name VARCHAR(100),
--    age INT,
--    grade INT
--);

--INSERT INTO Students (student_id, name, age, grade)
--VALUES
--    (1, 'John Doe', 18, 12),
--    (2, 'Jane Smith', 17, 11),
--    (3, 'Michael Johnson', 16, 10),
--    (4, 'Emily Brown', 18, 12),
--    (5, 'William Lee', 17, 11),
--    (6, 'Olivia Davis', 16, 10),
--    (7, 'James Wilson', 18, 12),
--    (8, 'Sophia Martin', 17, 11),
--    (9, 'Alexander Anderson', 16, 10),
--    (10, 'Isabella Thompson', 18, 12);

Select * From Students;

GO
create PROCEDURE ADDRECORD(
@NAME VARCHAR(50),
@AGE INT,
@GRADE INT)
AS
BEGIN
DECLARE @SID INT;

SELECT 
TOP 1
@SID = Students.student_id 
FROM 
Students 
ORDER BY student_id DESC


INSERT INTO Students VALUES (@SID + 1,@NAME,@AGE,@GRADE)

END

DROP TABLE STUDENTS



SELECT * FROM sys.procedures WHERE name = 'ADDRECORD'


BEGIN TRAN

EXEC [DBO].[ADDRECORD] 'Jeevan',21,8

SELECT * FROM Students;
delete FROM Students
where student_id = 12;

ROLLBACK TRAN

--use PRAC

select * from Students

begin tran

select * into StudentsDuo from [dbo].[Students]

select * from StudentsDuo
rollback tran

begin tran

COPY  [dbo].[Students] FROM MyServer.MyDatabase.Customers

rollback tran

alter table Students 
add Subjects varchar(25);

select * from Students

begin tran
update Students 
set Subjects = 'English'
where Id%2 = 0;

update Students 
set Subjects = 'Maths'
where Id%2 <> 0;

commit

declare @pagesnumber int = 3;
declare @pageSize int = 5;
declare @total int = 50;

print(3|5)



SELECT *
FROM Students
ORDER BY student_id
OFFSET 5 ROWS
FETCH NEXT 1 ROWS ONLY;





insert into Students values(1,'Nick jas',12,9)
rollback tran

exec sp_helptext 'ADDRECORD'

CREATE PROCEDURE ADDRECORD( @NAME VARCHAR(50), @AGE INT, @GRADE INT)
AS 
BEGIN 
DECLARE @SID INT;  
SELECT  TOP 1 @SID = Students.student_id  
FROM  Students  ORDER BY student_id DESC   
INSERT INTO Students VALUES (@SID + 1,@NAME,@AGE,@GRADE)  
END


go


DECLARE @pagesnumber INT = 3;
DECLARE @pageSize INT = 5;
DECLARE @total INT = 50;

-- Calculate the offset
DECLARE @offset INT = (@pagesnumber - 1) * @pageSize;

-- Query to fetch data for a specific page
SELECT *
FROM Students
ORDER BY student_id -- Replace with the column you want to order by
OFFSET @offset ROWS
FETCH NEXT @pageSize ROWS ONLY;

truncate table Students

rollback
begin tran

select * from Students;

INSERT INTO Students (Student_id,studentName,Age,Grade)
VALUES
(1, 'John Doe', 20, 10),
(2, 'Jane Doe', 19, 11),
(3, 'Peter Smith', 18, 12),
(4, 'Susan Jones', 17, 13),
(5, 'David Brown', 16, 14),
(6, 'Elizabeth Green', 15, 15),
(7, 'Michael Williams', 14, 16),
(8, 'Sarah Johnson', 13, 17),
(9, 'William Thomas', 12, 18),
(10, 'Jennifer Anderson', 11, 19),
(11, 'James Taylor', 10, 20),
(12, 'Emily White', 9, 21),
(13, 'Benjamin Brown', 8, 22),
(14, 'Olivia Black', 7, 23),
(15, 'Lucas Green', 6, 24),
(16, 'Ava Smith', 5, 25),
(17, 'Noah Williams', 4, 26),
(18, 'Chloe Johnson', 3, 27),
(19, 'Oliver Thomas', 2, 28),
(20, 'Isabella Anderson', 1, 29),
(21, 'Jacob Taylor', 100, 30),
(22, 'Sophia White', 99, 31),
(23, 'Henry Black', 98, 32),
(24, 'Amelia Green', 97, 33),
(25, 'William Smith', 96, 34),
(26, 'Ethan Williams', 95, 35),
(27, 'Avery Johnson', 94, 36),
(28, 'Mia Thomas', 93, 37),
(29, 'Alexander Anderson', 92, 38),
(30, 'Lily Taylor', 91, 39),
(31, 'Benjamin White', 90, 40),
(32, 'Olivia Black', 89, 41),
(33, 'Lucas Green', 88, 42),
(34, 'Ava Smith', 87, 43),
(35, 'Noah Williams', 86, 44),
(36, 'Chloe Johnson', 85, 45),
(37, 'Oliver Thomas', 84, 46),
(38, 'Isabella Anderson', 83, 47),
(39, 'Jacob Taylor', 82, 48),
(40, 'Sophia White', 81, 49),
(41, 'Henry Black', 80, 50),
(42, 'Amelia Green', 79, 51),
(43, 'William Smith', 78, 52),
(44, 'Ethan Williams', 77, 53),
(45, 'Avery Johnson', 76, 54),
(46, 'Mia Thomas', 75, 55),
(47, 'Alexander Anderson', 74, 56),
(48, 'Lily Taylor', 73, 57),
(49, 'Benjamin White', 72, 58),
(50, 'Olivia Black', 71, 59),
(51, 'Lucas Green', 70, 60)

select * from Students;
GO
CREATE PROCEDURE Getstudents(@pagenumber INT,@pageSize INT)
AS
BEGIN

--stored prcedure start
DECLARE @total INT;
--total page for previous button and next button hold
declare @totalPages int;

--get the Total pages
SELECT @total = COUNT(student_id) FROM STUDENTS;

--finding the total page for not braching the unkown page
set  @totalPages = CEILING(CAST( @total AS DECIMAL) / @pageSize);

-- Ensure @pagesnumber is within a valid range
IF @pagenumber < 1 SET @pagenumber = 1;
IF @pagenumber > @totalPages SET @pagenumber = @totalPages;


DECLARE @offset INT = (@pagenumber - 1) * @pageSize;

-- Query to fetch data for a specific page
SELECT *
FROM Students
ORDER BY student_id -- Replace with the column you want to order by
OFFSET @offset ROWS
FETCH NEXT @pageSize ROWS ONLY;

END

EXEC Getstudents 1,100;

--select CEILING(CAST(53 AS DECIMAL) / 10)

ALTER TABLE STUDENTS
COLUMN 

exec sp_helptext '';
go

--RIGHT WAY TO DO IT

DECLARE @SORTCOLUMN VARCHAR(MAX)='NAME';
DECLARE @SORTORDER VARCHAR(10) = 'DESC';
SELECT 
*
FROM Students
ORDER BY 
CASE WHEN @SORTORDER = 'ASC' THEN
	CASE WHEN @SORTCOLUMN = 'STUDENT_ID'
			 THEN CONVERT(VARCHAR,Student_id)
			 WHEN @SORTCOLUMN = 'NAME'
			 THEN studentName
			 WHEN @SORTCOLUMN = 'AGE'
			 THEN CONVERT(VARCHAR,Age)
			 WHEN @SORTCOLUMN = 'GRADE'
			 THEN  CONVERT(VARCHAR,Grade)
	END 
END ASC,
CASE WHEN @SORTORDER = 'DESC' THEN
	CASE WHEN @SORTCOLUMN = 'STUDENT_ID'
		 THEN CONVERT(VARCHAR,Student_id)
		 WHEN @SORTCOLUMN = 'NAME'
		 THEN studentName
		 WHEN @SORTCOLUMN = 'AGE'
		 THEN CONVERT(VARCHAR,Age)
		 WHEN @SORTCOLUMN = 'GRADE'
		 THEN CONVERT(VARCHAR,Grade)
	END
END DESC
OFFSET 10 ROWS
FETCH NEXT 10 ROWS ONLY




GO

SELECT * FROM STUDENTS

GO

SELECT TOP 1 MigrationId
FROM __EFMigrationsHistory
ORDER BY MigrationId DESC;

create database CRUD

