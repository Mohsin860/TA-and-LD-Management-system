select * from AssignTaskLD;
select * from FeedbackLD ;
select * from FeedbackTA ;

select * from AssignTaskTA ;
select * from LDs ;
select * from AssignTaskLD ;
select * from TAs ;
select * from faculty ;
select * from Courses ;
select * from admin ;
drop table if exists Courses 

CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
	  CourseName VARCHAR(255) NOT NULL,
	  CourseCode VARCHAR(10) NOT NULL,
  
  
    InstructorID VARCHAR(50) ,
	FOREIGN KEY (InstructorID) REFERENCES faculty(username)
    
);
create OR alter trigger InsertedTrigger
ON Courses
after insert
as
begin
    print 'Details in the Course table has updated.';
end;


drop table if exists AssignTaskTA 

CREATE TABLE AssignTaskTA (
     facultyusername varchar(50),
    TaskID INT PRIMARY KEY identity(1,1),
    TaskName VARCHAR(255) NOT NULL,
    Description TEXT,
    Deadline DATE,
    Status VARCHAR(50) DEFAULT 'Pending',
    UserID varchar(50) ,
	courseID int,
	 FOREIGN KEY (courseID) REFERENCES Courses(CourseID),
		

    FOREIGN KEY (UserID) REFERENCES TAs(username)
);

create OR alter trigger InsertedTrigger3
ON AssignTaskTA
after insert
as
begin
    print 'Details in the AssignTaskTA table has updated.';
end;



drop table if exists AssignTaskLD 

CREATE TABLE AssignTaskLD (
     facultyusername varchar(50),
	-- FOREIGN KEY (facultyusername) REFERENCES faculty(username),
    TaskID INT PRIMARY KEY identity(1,1),
    TaskName VARCHAR(255) NOT NULL,
    Description TEXT,
    Deadline DATE,
    Status VARCHAR(50) DEFAULT 'Pending',
    UserID varchar(50) ,
	courseID int,
	 FOREIGN KEY (courseID) REFERENCES Courses(CourseID),
		

    FOREIGN KEY (UserID) REFERENCES LDs(username)
);
create OR alter trigger InsertedTrigger2
ON AssignTaskLD
after insert
as
begin
    print 'Details in the AssignTaskLD table has updated.';
end;
drop table if exists FeedbackTA 

CREATE TABLE FeedbackTA (
    FeedbackID INT PRIMARY KEY identity(1,1),
	TaskID INT,FOREIGN KEY (TaskID) REFERENCES AssignTaskTA(TaskID),
	courseID int,
	 FOREIGN KEY (courseID) REFERENCES Courses(CourseID),
	 FeedbackText TEXT,
	 Rating INT CHECK (Rating >= 1 AND Rating <= 5),
    facultyusername varchar(50),
   
    
DateTimeProvided DATETIME DEFAULT GETDATE()  
   
);

create OR alter trigger InsertedTrigger1
ON FeedbackTA
after insert
as
begin
    print 'Details in the FeedbackTA table has updated.';
end;



drop table if exists FeedbackLD 

CREATE TABLE FeedbackLD (
    FeedbackID INT PRIMARY KEY identity(1,1),
	TaskID INT,FOREIGN KEY (TaskID) REFERENCES AssignTaskTA(TaskID),
	courseID int,
	FOREIGN KEY (courseID) REFERENCES Courses(CourseID),
	 FeedbackText TEXT,
	 Rating INT CHECK (Rating >= 1 AND Rating <= 5),
    facultyusername varchar(50),
   
    
DateTimeProvided DATETIME DEFAULT GETDATE()  
   
);


create OR alter trigger InsertedTrigger4
ON FeedbackLD
after insert
as
begin
    print 'Details in the FeedbackLD table has updated.';
end;


drop table if exists TAs 

create table TAs
(
	FName varchar(50),
	LName varchar(50),
	username varchar(50) primary key not null,
	password varchar(50),
	Contact varchar(50)

);


create OR alter trigger InsertedTrigger5
ON TAs
after insert
as
begin
    print 'Details in the TAs table has updated.';
end;

drop table if exists faculty 

create table faculty
(
	FName varchar(50),
	LName varchar(50),
	username varchar(50) primary key not null,
	password varchar(50),
	Contact varchar(50)

);


create OR alter trigger InsertedTrigger6
ON faculty
after insert
as
begin
    print 'Details in the faculty table has updated.';
end;

drop table if exists LDs 

create table LDs
(
	FName varchar(50),
	LName varchar(50),
	username varchar(50) primary key not null,
	password varchar(50),
	Contact varchar(50)

);

create OR alter trigger InsertedTrigger7
ON LDs
after insert
as
begin
    print 'Details in the LDs table has updated.';
end;

create table admin
(
	FName varchar(50),
	LName varchar(50),
	username varchar(50) primary key not null,
	password varchar(50)
	

);


create OR alter trigger InsertedTrigger8
ON admin
after insert
as
begin
    print 'Details in the admin table has updated.';
end;
















SELECT A.TaskID, A.TaskName, A.Description, A.Deadline, A.Status, C.CourseName
FROM AssignTaskTA A
JOIN Courses C ON A.courseID = C.CourseID;

SELECT A.TaskID, A.TaskName, A.Description, A.Deadline, A.Status,  C.CourseName, F.Username AS Faculty_ID, concat(F.FName ,' ',F.LName)AS Faculty_Name
FROM AssignTaskTA A
JOIN TAs T ON A.UserID = T.Username
JOIN Courses C ON A.courseID = C.CourseID
JOIN faculty F ON A.facultyusername = F.Username
where A.UserID='ahmad';

SELECT   A.TaskName,FT.FeedbackText, FT.Rating, FT.DateTimeProvided,  C.CourseName, F.Username AS Faculty_Username,  concat(F.FName ,' ',F.LName)AS Faculty_Name
FROM FeedbackTA FT
JOIN AssignTaskTA A ON FT.TaskID = A.TaskID
JOIN Courses C ON A.courseID = C.CourseID
JOIN faculty F ON A.facultyusername = F.Username
where A.UserID='haris44';

SELECT FT.TaskID, FT.FeedbackText, FT.Rating, FT.DateTimeProvided, A.TaskName, C.CourseName
FROM FeedbackTA FT
JOIN AssignTaskTA A ON FT.TaskID = A.TaskID
JOIN Courses C ON A.courseID = C.CourseID;
SELECT  FT.TaskID, FT.FeedbackText, FT.Rating, FT.DateTimeProvided, A.TaskName, C.CourseName
FROM FeedbackTA FT
JOIN AssignTaskTA A ON FT.TaskID = A.TaskID
JOIN Courses C ON A.courseID = C.CourseID