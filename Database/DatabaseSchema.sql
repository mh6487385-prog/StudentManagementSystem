-- ============================================================
-- STUDENT MANAGEMENT PORTAL - DATABASE SCHEMA
-- SQL Server Database Script
-- ============================================================

-- Create Database
-- CREATE DATABASE StudentManagementDB;
-- USE StudentManagementDB;

-- ============================================================
-- TABLE 1: Users (Login Authentication)
-- ============================================================
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    UserType NVARCHAR(20) NOT NULL, -- 'Admin', 'Student', 'Instructor'
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastLogin DATETIME NULL
);

-- ============================================================
-- TABLE 2: Students
-- ============================================================
CREATE TABLE Students (
    StudentID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    StudentNumber NVARCHAR(20) UNIQUE NOT NULL,
    DateOfBirth DATE NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    EnrollmentDate DATE DEFAULT GETDATE(),
    Status NVARCHAR(20) DEFAULT 'Active', -- 'Active', 'Inactive', 'Graduated'
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- ============================================================
-- TABLE 3: Courses
-- ============================================================
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseCode NVARCHAR(10) UNIQUE NOT NULL,
    CourseName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Credits INT NOT NULL,
    Semester INT NOT NULL, -- 1, 2, 3, 4, etc.
    InstructorID INT NOT NULL,
    MaxStudents INT DEFAULT 50,
    Status NVARCHAR(20) DEFAULT 'Active',
    FOREIGN KEY (InstructorID) REFERENCES Users(UserID)
);

-- ============================================================
-- TABLE 4: Enrollments (Junction Table)
-- ============================================================
CREATE TABLE Enrollments (
    EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT NOT NULL,
    CourseID INT NOT NULL,
    EnrollmentDate DATE DEFAULT GETDATE(),
    Grade NVARCHAR(2) NULL, -- 'A', 'B', 'C', 'D', 'F'
    Marks DECIMAL(5,2) NULL, -- 0-100
    Status NVARCHAR(20) DEFAULT 'Enrolled', -- 'Enrolled', 'Completed', 'Dropped'
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
    UNIQUE(StudentID, CourseID)
);

-- ============================================================
-- TABLE 5: Assignments
-- ============================================================
CREATE TABLE Assignments (
    AssignmentID INT PRIMARY KEY IDENTITY(1,1),
    CourseID INT NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    DueDate DATE NOT NULL,
    MaxMarks INT DEFAULT 100,
    CreatedDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(20) DEFAULT 'Active',
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

-- ============================================================
-- Create Indexes for Better Performance
-- ============================================================
CREATE INDEX idx_Users_Username ON Users(Username);
CREATE INDEX idx_Students_UserID ON Students(UserID);
CREATE INDEX idx_Courses_InstructorID ON Courses(InstructorID);
CREATE INDEX idx_Enrollments_StudentID ON Enrollments(StudentID);
CREATE INDEX idx_Enrollments_CourseID ON Enrollments(CourseID);
CREATE INDEX idx_Assignments_CourseID ON Assignments(CourseID);

-- ============================================================
-- SAMPLE DATA INSERTION
-- ============================================================

-- Insert Admin User
INSERT INTO Users (Username, Password, Email, FullName, UserType) 
VALUES ('admin', '123456', 'admin@college.edu', 'Admin User', 'Admin');

-- Insert Instructor Users
INSERT INTO Users (Username, Password, Email, FullName, UserType) 
VALUES 
('prof_smith', '123456', 'smith@college.edu', 'Dr. John Smith', 'Instructor'),
('prof_jones', '123456', 'jones@college.edu', 'Dr. Mary Jones', 'Instructor');

-- Insert Student Users
INSERT INTO Users (Username, Password, Email, FullName, UserType) 
VALUES 
('student1', '123456', 'student1@college.edu', 'John Doe', 'Student'),
('student2', '123456', 'student2@college.edu', 'Jane Smith', 'Student'),
('student3', '123456', 'student3@college.edu', 'Michael Brown', 'Student');

-- Insert Students
INSERT INTO Students (UserID, StudentNumber, DateOfBirth, PhoneNumber, Address, City) 
VALUES 
(4, 'STU001', '2004-05-15', '555-0101', '123 Main St', 'New York'),
(5, 'STU002', '2004-08-22', '555-0102', '456 Oak Ave', 'Los Angeles'),
(6, 'STU003', '2005-01-10', '555-0103', '789 Pine Rd', 'Chicago');

-- Insert Courses
INSERT INTO Courses (CourseCode, CourseName, Description, Credits, Semester, InstructorID) 
VALUES 
('CS101', 'Introduction to Programming', 'Basics of programming with C#', 3, 1, 2),
('CS102', 'Database Design', 'SQL Server and database concepts', 3, 1, 3),
('CS201', 'Web Development', 'ASP.NET and Web Forms', 4, 2, 2);

-- Insert Enrollments
INSERT INTO Enrollments (StudentID, CourseID, Grade, Marks, Status) 
VALUES 
(1, 1, 'A', 92, 'Completed'),
(1, 2, 'B', 85, 'Completed'),
(2, 1, 'A', 95, 'Completed'),
(2, 3, NULL, NULL, 'Enrolled'),
(3, 2, 'C', 78, 'Completed');

-- Insert Assignments
INSERT INTO Assignments (CourseID, Title, Description, DueDate, MaxMarks) 
VALUES 
(1, 'Hello World Program', 'Create a simple C# console application', '2026-06-15', 50),
(2, 'Create a Database', 'Design and create a student database', '2026-06-20', 100),
(3, 'Build a Web Form', 'Create an ASP.NET login page', '2026-06-25', 75);

-- ============================================================
-- USEFUL SQL QUERIES FOR THE APPLICATION
-- ============================================================

-- Query 1: Get all courses with instructor names
-- SELECT c.CourseID, c.CourseCode, c.CourseName, u.FullName as InstructorName 
-- FROM Courses c 
-- INNER JOIN Users u ON c.InstructorID = u.UserID;

-- Query 2: Get student enrollment details
-- SELECT s.StudentNumber, u.FullName, c.CourseName, e.Grade, e.Marks
-- FROM Enrollments e
-- INNER JOIN Students s ON e.StudentID = s.StudentID
-- INNER JOIN Users u ON s.UserID = u.UserID
-- INNER JOIN Courses c ON e.CourseID = c.CourseID;

-- Query 3: Get student's courses
-- SELECT c.CourseID, c.CourseCode, c.CourseName, c.Credits, e.Status
-- FROM Enrollments e
-- INNER JOIN Courses c ON e.CourseID = c.CourseID
-- WHERE e.StudentID = @StudentID;

-- Query 4: Search students by name
-- SELECT StudentID, UserID, StudentNumber FROM Students 
-- WHERE StudentID IN (SELECT UserID FROM Users WHERE FullName LIKE @SearchText);
