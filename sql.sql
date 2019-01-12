Create Database [Sample1]


Alter Database [Sample1] Modify Name = [Sample2]
sp_renameDB 'Sample2', 'Sample3'


Drop Database [Sample3]

Alter Database [Sample3] Set SINGLE_USER With Rollback Immediate --If database in use
Drop Database [Sample3]


Use [Sample3]

Create Table Person
(
	[ID] int NOT NULL Identity Primary Key, --'Identity' implicitly creates the primary key
	[Name] nvarchar(50) NOT NULL,
    [Email] nvarchar(50) NOT NULL,
	[Age] int NULL,
    [GenderId] int NULL 
)

Create Table Gender
(
	[ID] int NOT NULL Primary Key,
	[Gender] nvarchar(50) NOT NULL
)

Insert Into [Person] ([Name], [Email], [GenderId], [Age]) Values ('Rich', 'r@r.com', 1, 21)

Select * FROM [Person]
Select Distinct City From [Person]
Select * From [Person] Where City = 'London'
Select * From [Person] Where City != 'London'
Select * From [Person] Where Age In (20, 23, 29)
Select * From [Person] Where Age Between 20 And 25
Select * From [Person] Where City Like 'L%'
Select * From [Person] Where City Not Like 'L%'
Select * From [Person] Where Email Like '%@%.___'--_ single character
Select * From [Person] Where Name Like '[MST]' -- names start with M, S or T
Select * From [Person] Where Name Like '[^MST]' -- names not starts with M, S or T
Select * From [Person] Order By [Name] Asc, [Age] Asc --sort output
Select Top 10 * From Person
Select Top 1 Percent * From Person
Select Sum(Salary) From Employee --single value
Select City, Avg(Salary) As [Average Salery] From Employee group By City
Select Count(*) From Employee
Select Count(ID) From Employee --better performance
Select Top 1 Format(LONG_W, 'N4') From Station Where LAT_N > 38.778 order by LAT_N
Select [Name] From Employee Where DepartmentId Is Null

Select 
Format(
Sqrt(
Power([Max LAT] - [Min LAT], 2) + 
Power([Max LONG] - [Min LONG], 2)), 
'N4') 
From (Select Min(LAT_N) as [Min LAT], Min(LONG_W) as [Min LONG], Max(LAT_N) as [Max LAT], Max(LONG_W) as [Max LONG] FROM Station) 
as Station;

Select [Name], [DepartmentName] From Employee Inner Join Department On Employee.DepartmentId = Department.Id --inner join
Select [Name], [DepartmentName] From Employee Left Join Department On Employee.DepartmentId = Department.Id --left join (incl. null values of employee)
Select [Name], [DepartmentName] From Employee Right Join Department On Employee.DepartmentId = Department.Id --right join (incl. null values of department)
Select [Name], [DepartmentName] From Employee Full Join Department On Employee.DepartmentId = Department.Id --full join (incl. null values of employee and department)
Select [Name], [DepartmentName] From Employee Cross Join Department --cross join (employee.length x department.length)

--where can be used with Select, Insert, Update
--having can be used with Select only

--where cannot not use aggregate functions
--having can use aggregate functions

--where clause before order by (possibly better performance)
--having clause after order by

Delete From Gender Where ID = 2 --if no constraint exists


Alter Table [Person] Add Constraint FK_Person_GenderID Foreign Key (GenderId) references [Gender](ID) --GenderId of Table Person must be available as Id of Table Gender
Alter Table [Person] Add Constraint DF_Person_GenderID Default 3 For GenderId --default value for GenderId
Alter Table [Person] Add Constraint CK_Person_Age Check ((Age > 0) AND (Age < 150)) --range constraint
Alter Table [Person] Add Constraint UQ_Person_Email Unique(Email)--unique key column

Alter Table [Person] Drop Constraint FK_Person_GenderID --delete constraint



Set Identity_Insert Person On --'Identity' value must be given explicitly

Dbcc Checkident(Person, Reseed, 0) --Reset 'Identity' value generation

Select SCOPE_IDENTITY() --last generated identity value in current session and current scope
Select @@IDENTITY --last generated identity value in current session and any scope
Select Ident_Current('Person') --last generated identity value in any session and any scope


Create Trigger trForInsert on Person for Insert --action on one table can initiate action on another table
as
Begin
	Insert Into Person2 ([Name], [Email]) Values ('Name', 'hs@hans.com');
End


-- Table can have multiple unique key columns but only one primary key column
-- unique key columns allow null, primary key column doesn't allow null


Select IsNull(Department.Name, 'No Department') --replaces null values
Select Coalesce(Department.Name, 'No Department')
Case When Department.Name Is Null Then 'No Department' Else Department.Name End

Select Coalesce(FirstName, MiddleName, LastName) as Name From Employee --use first value which is not null


-- UNION - same rows will not be merged
-- UNION ALL - same rows will be merged, slower because of sort and distinct to remove duplicate rows

-- 'Live-Abfragestatistik einschließen' to anlyse query


Create Procedure spGetEmployeesByGender
@Gender nvarchar(20)
As
Begin
	Select * From Employee Where Gender = @Gender;
End

Alter Procedure spGetEmployeesByGender
@Gender nvarchar(20)
As
Begin
	Select * From Employee Where Gender = @Gender Order By name;
End

Execute spGetEmployeesByGender 'Male'; --call stored procedure

Drop Procedure spGetEmployeesByGender;


Create Procedure spGetEmployeesByGender
@Gender nvarchar(20)
With Encryption
As
Begin
	Select * From Employee Where Gender = @Gender;
End

sp_helptext spGetEmployeesByGender;--show code of stored procedure (if not encrypted)
sp_help anyDatabaseObject --show useful information
sp_depends anyDatabaseObject --show dependencies


Create Procedure spGetEmployeeCountByGender
@Gender nvarchar(20),
@EmployeeCount int Output
As
Begin
	Select @EmployeeCount = Count(ID) From Employee Where Gender = @Gender;
End

Declare @EmployeeCount int;
Execute spGetEmployeeCountByGender 'Male', @EmployeeCount Output;
Print @EmployeeCount;


Create Procedure spGetEmployeeCountByGender
@Gender nvarchar(20)
As
Begin
	return (Select Count(ID) From Employee Where Gender = @Gender);
End

Declare @EmployeeCount int;
Execute @EmployeeCount = spGetEmployeeCountByGender 'Male'
Print @EmployeeCount;


-- Return:				-- Output:
-- only int				-- any datatype
-- only one				-- multiple
-- show success/failure -- any other function result

-- Stored Procedure:
-- reusable
-- less network traffic
-- avoid sql injection


-- Function

Create Function fn_CalculateAge(@DateOfBirth Date)
Returns int
As
Begin
	Declare @Age int;
	
	--do calculations based on parameters
	Set @Age = 30

	return @Age
End

Alter Function fn_CalculateAge(@DateOfBirth Date)
Returns int
As
Begin
	Declare @Age int;
	
	--do calculations based on parameters
	Set @Age = 31

	return @Age
End

Select dbo.fn_CalculateAge('02.09.2011')

Drop Function dbo.fn_CalculateAge;

Create Function fn_EmployeesByGender(@Gender nvarchar(10))--better performance, can be used in an update
Returns Table
As
	return (Select * From Employee Where Gender = @Gender)

Select * From dbo.fn_EmployeesByGender('Male')

Create Function fn_EmployeesByGender2(@Gender nvarchar(10))--worse performance, canot be used in an update
Returns @Table Table (ID int, Name nvarchar(20), Salary int, Gender nvarchar(20))
As
Begin
	Insert Into @Table
	Select * From Employee Where Gender = @Gender

	Return
End

Select * From dbo.fn_EmployeesByGender2('Male')

Create Function fn_EmployeesByGender(@Gender nvarchar(10))
Returns Table
With Schemabinding--Drop Table Employee not possible
As
	return (Select ID, Name, Salary, Gender From dbo.Employee Where Gender = @Gender)

-- Function vs Stored procedure:
-- functions can be used inside a select/where
-- stored procedures cannot be used inside a select/where

-- (fucntioncannot return text, ntext, image, cursor and timestamp)


Select Ascii('A') --65
Select Char(65) --A

--Print alphabet in a loop
Declare @Start int;
Set @Start = 65;

While(@Start <= 90)
Begin
	Print Char(@Start);
	Set @Start = @Start + 1;
End


Select Ltrim('    Hello') --removes leading spaces
Select Rtrim('Hello    ') --removes trailing spaces

Select Upper('Hello')--'HELLO'
Select Lower('Hello')--'hello'

Select Reverse('olleH')--'Hello'

Select Len('Hello')--5

Select Left('Hello1Hello2', 6);--Hello1
Select Right('Hello1Hello2', 6);--Hello2

Select Charindex('2', '123', 1);--2 (1-based index)
Select Patindex('%@aaa.com', '123@aaa.com');--4

Select Substring('12345', 3, 2);--34

Select Replicate('Hello', 3);--HelloHelloHello

Select Space(5);--'     '

Select Replace('hi@hello.com', '.com', '.net');--hi@hello.net

Select Stuff('hi@hello.com', 2, 1, '***');--replace 'i' by '***'


-- Date and time
Select Getdate();
Select Current_Timestamp;
Select Sysdatetime();
Select Sysdatetimeoffset();
Select Getutcdate();

Select Isdate('Hello')--0
Select Isdate(Getdate())--1

Select Day(Getdate());--current day
Select Month(Getdate());--current month
Select Year(Getdate());--current year

Select Datename(Month, Getdate());--current month as string
Select Datename(Weekday, Getdate());--current weekday as string

Select Datepart(Month, Getdate());--current month
Select Datepart(Weekday, Getdate());--current weekday

Select Dateadd(Day, 1, Getdate());--tomorrow
Select Dateadd(Day, -1, Getdate());--yesterday

Select Datediff(Day, '11.05.2019', '20.09.2019')--132 days
Select Datediff(Month, '11.05.2019', '20.09.2019')--4 month


-- Cast vs Convert
Select Cast(Getdate() as nvarchar);--no style paramter, standardized function,
Select Convert(nvarchar, Getdate(), 103);--optional styple parameter, microsoft specific

Select ROUND(850.556, 2);--850.560 (round second decimal place)
Select ROUND(850.556, 2, 1);--850.550 (cut after second decimal place)
Select ROUND(850.556, -2);--900.000


-- local temporary table:
-- only available in this session/context
-- will be deleted when connection is closed
-- should not be returned from stored procedures! (other context/session)
Create Table #PersonDetails
(
	ID int, 
	Name varchar(20)
)

Insert Into #PersonDetails Values(1, 'Mike')
Insert Into #PersonDetails Values(2, 'John')

Select * From #PersonDetails


-- global temporary table:
-- available in other session/context
-- will be deleted when last connection is closed
-- can be returned from stored procedures!
Create Table ##PersonDetails
(
	ID int, 
	Name varchar(20)
)

Insert Into ##PersonDetails Values(1, 'Mike')
Insert Into ##PersonDetails Values(2, 'John')

Select * From ##PersonDetails
