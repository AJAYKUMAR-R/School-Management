CREATE TABLE Students
(
Student_id int,
studentName varchar(20),
Age int,
Grade int,
)

go

--PROCEDURE FOR CREATE
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
go

exec ADDRECORD 'ajay',10,12

select * from Students

---PROCEDURE FOR UPDATE 
GO
CREATE PROCEDURE UPDATESTUDENTS(
@ID INT,
@SNAME VARCHAR(20),
@AGE INT,
@GRADE INT
)
AS 
BEGIN
update Students set studentName = @SNAME,
Grade = @GRADE,
Age = @AGE
where Student_id = @ID
END

EXEC UPDATESTUDENTS 53,'AJ',12,12

GO

--PROCEDURE FOR DELETE
CREATE PROCEDURE DELETESTUDENTS(@ID INT)
AS
BEGIN
DELETE FROM STUDENTS 
WHERE Student_id = @ID
END


EXEC DELETESTUDENTS 53

EXEC ADDRECORD 'DRAX',10,8

--SELECT * FROM STUDENTS


SELECT * FROM Students

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

DROP PROCEDURE Getstudents

--sp for Pagination
GO
CREATE PROCEDURE Getstudents(
@pagenumber INT,
@pageSize INT,
@DropdownColumn  VARCHAR(20),
@DropdownColumnValue VARCHAR(20),
@SortDirection VARCHAR(10),
@SortColumns varchar(10),
@TotalRecords int output
)
AS
BEGIN

--CONDITION CHECK FOR ALL THE INPUT VALUES PREVENTING NULL
DECLARE @SortOrder VARCHAR(MAX) = ISNULL(@SortDirection,'ASC')
DECLARE @SortColumn VARCHAR(MAX) = ISNULL(@SortColumns,'STUDENT_ID')

SELECT 'ORDER',@SortOrder
SELECT 'COLUMN',@SortColumn

---TEM TABLE CREATION
CREATE TABLE #TempTable (
	Student_id int,
	studentName varchar(20),
	Age int,
	Grade int
);

INSERT INTO #TempTable (Student_id, studentName,Age,Grade)
SELECT *
FROM Students
WHERE
		(@DropdownColumn IS NULL)
		or
        (@DropdownColumnValue IS NULL)
        OR
        (
            (@DropdownColumn = 'NAME') AND (studentName LIKE  @DropdownColumnValue + '%')
        )
        OR
        (
            (@DropdownColumn = 'STUDENT_ID') AND (Student_id LIKE  @DropdownColumnValue + '%')
        )
        OR
        (
            (@DropdownColumn = 'AGE') AND (Age LIKE  @DropdownColumnValue + '%')
        )
        OR
        (
            (@DropdownColumn = 'Grade') AND (Grade LIKE @DropdownColumnValue + '%')
        )
    
SELECT * FROM #TempTable;


--stored prcedure start
DECLARE @total INT;
--total page for previous button and next button hold
declare @totalPages int;

--ensure pagesize  is not 0 and not null
IF @pageSize = 0 or @pageSize is null  SET @pageSize = 5;

--get the Total pages
SELECT @total = COUNT(student_id),@TotalRecords = COUNT(student_id) FROM #TempTable;

--finding the total page for not braching the unkown page
set  @totalPages = CEILING(CAST( @total AS DECIMAL) / @pageSize);

-- Ensure @pagesnumber is within a valid range
IF @pagenumber < 1 SET @pagenumber = 1;
IF @pagenumber > @totalPages SET @pagenumber = @totalPages;




DECLARE @offset INT = (@pagenumber - 1) * @pageSize;

-- Query to fetch data for a specific page
SELECT 'PAGE NUMBER',@pagenumber
SELECT 'PAGE sIZE',@pageSize
SELECT 'TpAGEsIZE',@totalPages
SELECT 'OFFSET',@offset
SELECT 'TOTAL',@total
--SELECT 'sORTORDER',@SortOrder;
--SELECT 'SORTCOLUMN',@SORTCOLUMN

SELECT * FROM #TEMPTABLE
ORDER BY 
CASE WHEN @SortOrder = 'ASC' THEN
	CASE WHEN @SORTCOLUMN = 'STUDENT_ID'
			 THEN CONVERT(VARCHAR,Student_id)
			 WHEN @SortColumn = 'NAME'
			 THEN studentName
			 WHEN @SortColumn = 'AGE'
			 THEN CONVERT(VARCHAR,Age)
			 WHEN @SortColumn = 'GRADE'
			 THEN  CONVERT(VARCHAR,Grade)
	END 
END ASC,
CASE WHEN @SortOrder = 'DESC' THEN
	CASE WHEN @SortColumn = 'STUDENT_ID'
		 THEN CONVERT(VARCHAR,Student_id)
		 WHEN @SortColumn = 'NAME'
		 THEN studentName
		 WHEN @SortColumn = 'AGE'
		 THEN CONVERT(VARCHAR,Age)
		 WHEN @SortColumn = 'GRADE'
		 THEN CONVERT(VARCHAR,Grade)
	END
END DESC
OFFSET @offset ROWS
FETCH NEXT @pageSize ROWS ONLY;

IF(OBJECT_ID('tempdb..#TempTable') IS NOT NULL)
	BEGIN
		DROP TABLE #TempTable;
	END

END


go

--RIGHT WAY TO DO IT
go
--@pagenumber INT,
--@pageSize INT,
--@DropdownColumn  VARCHAR(20),
--@DropdownColumnValue VARCHAR(20),
--@SortDirection VARCHAR(10),
--@SortColumns varchar(10),
--@TotalRecords int output
go
declare @record int;
exec Getstudents 2,2,NULL,'j','ASC',NULL,@record output
select @record;

SELECT * FROM sTUDENTS
ORDER BY Student_id
OFFSET 2 ROWS
FETCH NEXT 2 ROWS ONLY