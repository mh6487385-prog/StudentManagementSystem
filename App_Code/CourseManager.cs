// ============================================================
// COURSE MANAGEMENT CLASS (DAL)
// App_Code/CourseManager.cs
// ============================================================

using System;
using System.Data;
using System.Data.SqlClient;

public class CourseManager
{
    /// <summary>
    /// Get all courses with instructor names
    /// </summary>
    public static DataTable GetAllCourses()
    {
        string query = @"SELECT c.CourseID, c.CourseCode, c.CourseName, c.Credits, 
                       c.Semester, u.FullName as InstructorName, c.MaxStudents, c.Status
                       FROM Courses c
                       INNER JOIN Users u ON c.InstructorID = u.UserID
                       ORDER BY c.Semester, c.CourseCode";
        
        return DatabaseConnection.ExecuteQuery(query);
    }

    /// <summary>
    /// Search courses by name or code
    /// </summary>
    public static DataTable SearchCourses(string searchText)
    {
        string query = @"SELECT c.CourseID, c.CourseCode, c.CourseName, c.Credits, 
                       c.Semester, u.FullName as InstructorName, c.MaxStudents, c.Status
                       FROM Courses c
                       INNER JOIN Users u ON c.InstructorID = u.UserID
                       WHERE c.CourseCode LIKE @SearchText OR c.CourseName LIKE @SearchText
                       ORDER BY c.Semester, c.CourseCode";
        
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@SearchText", "%" + searchText + "%")
        };

        return DatabaseConnection.ExecuteQuery(query, parameters);
    }

    /// <summary>
    /// Get course by CourseID
    /// </summary>
    public static DataTable GetCourseByID(int courseID)
    {
        string query = @"SELECT c.*, u.FullName as InstructorName
                       FROM Courses c
                       INNER JOIN Users u ON c.InstructorID = u.UserID
                       WHERE c.CourseID = @CourseID";
        
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CourseID", courseID) };
        
        return DatabaseConnection.ExecuteQuery(query, parameters);
    }

    /// <summary>
    /// Get student's enrolled courses
    /// </summary>
    public static DataTable GetStudentCourses(int studentID)
    {
        string query = @"SELECT c.CourseID, c.CourseCode, c.CourseName, c.Credits, 
                       c.Semester, u.FullName as InstructorName, e.Grade, e.Marks, e.Status
                       FROM Enrollments e
                       INNER JOIN Courses c ON e.CourseID = c.CourseID
                       INNER JOIN Users u ON c.InstructorID = u.UserID
                       WHERE e.StudentID = @StudentID
                       ORDER BY c.Semester, c.CourseCode";
        
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@StudentID", studentID) };
        
        return DatabaseConnection.ExecuteQuery(query, parameters);
    }

    /// <summary>
    /// Insert new course
    /// </summary>
    public static int InsertCourse(string courseCode, string courseName, string description, 
                                   int credits, int semester, int instructorID, int maxStudents)
    {
        string query = @"INSERT INTO Courses (CourseCode, CourseName, Description, Credits, 
                       Semester, InstructorID, MaxStudents)
                       VALUES (@CourseCode, @CourseName, @Description, @Credits, @Semester, 
                       @InstructorID, @MaxStudents)";
        
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@CourseCode", courseCode),
            new SqlParameter("@CourseName", courseName),
            new SqlParameter("@Description", description),
            new SqlParameter("@Credits", credits),
            new SqlParameter("@Semester", semester),
            new SqlParameter("@InstructorID", instructorID),
            new SqlParameter("@MaxStudents", maxStudents)
        };

        return DatabaseConnection.ExecuteNonQuery(query, parameters);
    }

    /// <summary>
    /// Update course information
    /// </summary>
    public static int UpdateCourse(int courseID, string courseName, string description, 
                                   int credits, int maxStudents, string status)
    {
        string query = @"UPDATE Courses SET CourseName = @CourseName, Description = @Description, 
                       Credits = @Credits, MaxStudents = @MaxStudents, Status = @Status 
                       WHERE CourseID = @CourseID";
        
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@CourseName", courseName),
            new SqlParameter("@Description", description),
            new SqlParameter("@Credits", credits),
            new SqlParameter("@MaxStudents", maxStudents),
            new SqlParameter("@Status", status),
            new SqlParameter("@CourseID", courseID)
        };

        return DatabaseConnection.ExecuteNonQuery(query, parameters);
    }

    /// <summary>
    /// Delete course
    /// </summary>
    public static int DeleteCourse(int courseID)
    {
        string query = "DELETE FROM Courses WHERE CourseID = @CourseID";
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CourseID", courseID) };
        
        return DatabaseConnection.ExecuteNonQuery(query, parameters);
    }
}
