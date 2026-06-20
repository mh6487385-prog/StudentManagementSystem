// ============================================================
// ENROLLMENT MANAGEMENT CLASS (DAL)
// App_Code/EnrollmentManager.cs
// ============================================================

using System;
using System.Data;
using System.Data.SqlClient;

public class EnrollmentManager
{
    /// <summary>
    /// Get all enrollments with student and course details
    /// </summary>
    public static DataTable GetAllEnrollments()
    {
        string query = @"SELECT e.EnrollmentID, s.StudentNumber, u.FullName as StudentName, 
                       c.CourseCode, c.CourseName, e.Grade, e.Marks, e.Status, e.EnrollmentDate
                       FROM Enrollments e
                       INNER JOIN Students s ON e.StudentID = s.StudentID
                       INNER JOIN Users u ON s.UserID = u.UserID
                       INNER JOIN Courses c ON e.CourseID = c.CourseID
                       ORDER BY c.CourseCode, s.StudentNumber";
        
        return DatabaseConnection.ExecuteQuery(query);
    }

    /// <summary>
    /// Get enrollment by EnrollmentID
    /// </summary>
    public static DataTable GetEnrollmentByID(int enrollmentID)
    {
        string query = @"SELECT * FROM Enrollments WHERE EnrollmentID = @EnrollmentID";
        
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@EnrollmentID", enrollmentID) };
        
        return DatabaseConnection.ExecuteQuery(query, parameters);
    }

    /// <summary>
    /// Enroll student in course
    /// </summary>
    public static int EnrollStudent(int studentID, int courseID)
    {
        // Check if already enrolled
        string checkQuery = "SELECT COUNT(*) FROM Enrollments WHERE StudentID = @StudentID AND CourseID = @CourseID";
        SqlParameter[] checkParams = new SqlParameter[]
        {
            new SqlParameter("@StudentID", studentID),
            new SqlParameter("@CourseID", courseID)
        };
        
        object result = DatabaseConnection.ExecuteScalar(checkQuery, checkParams);
        if (Convert.ToInt32(result) > 0)
            throw new Exception("Student is already enrolled in this course.");

        // Insert enrollment
        string query = @"INSERT INTO Enrollments (StudentID, CourseID, Status) 
                       VALUES (@StudentID, @CourseID, 'Enrolled')";
        
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@StudentID", studentID),
            new SqlParameter("@CourseID", courseID)
        };

        return DatabaseConnection.ExecuteNonQuery(query, parameters);
    }

    /// <summary>
    /// Update enrollment grade and marks
    /// </summary>
    public static int UpdateEnrollmentGrade(int enrollmentID, string grade, decimal marks)
    {
        string query = @"UPDATE Enrollments SET Grade = @Grade, Marks = @Marks, Status = 'Completed' 
                       WHERE EnrollmentID = @EnrollmentID";
        
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@Grade", grade),
            new SqlParameter("@Marks", marks),
            new SqlParameter("@EnrollmentID", enrollmentID)
        };

        return DatabaseConnection.ExecuteNonQuery(query, parameters);
    }

    /// <summary>
    /// Drop enrollment
    /// </summary>
    public static int DropEnrollment(int enrollmentID)
    {
        string query = @"UPDATE Enrollments SET Status = 'Dropped' WHERE EnrollmentID = @EnrollmentID";
        
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@EnrollmentID", enrollmentID) };
        
        return DatabaseConnection.ExecuteNonQuery(query, parameters);
    }

    /// <summary>
    /// Delete enrollment
    /// </summary>
    public static int DeleteEnrollment(int enrollmentID)
    {
        string query = "DELETE FROM Enrollments WHERE EnrollmentID = @EnrollmentID";
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@EnrollmentID", enrollmentID) };
        
        return DatabaseConnection.ExecuteNonQuery(query, parameters);
    }

    /// <summary>
    /// Get enrollment statistics for a course
    /// </summary>
    public static DataTable GetCourseEnrollmentStats(int courseID)
    {
        string query = @"SELECT COUNT(*) as TotalEnrollments, 
                       SUM(CASE WHEN Status = 'Enrolled' THEN 1 ELSE 0 END) as ActiveStudents,
                       AVG(CAST(Marks AS FLOAT)) as AverageMarks
                       FROM Enrollments WHERE CourseID = @CourseID";
        
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@CourseID", courseID) };
        
        return DatabaseConnection.ExecuteQuery(query, parameters);
    }
}
