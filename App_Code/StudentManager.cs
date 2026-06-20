// ============================================================
// STUDENT MANAGEMENT CLASS (DAL)
// App_Code/StudentManager.cs
// ============================================================

using System;
using System.Data;
using System.Data.SqlClient;

public class StudentManager
{
    /// <summary>
    /// Get all students
    /// </summary>
    public static DataTable GetAllStudents()
    {
        string query = @"SELECT s.StudentID, s.StudentNumber, u.FullName, s.DateOfBirth, 
                       s.PhoneNumber, s.City, s.Status, s.EnrollmentDate
                       FROM Students s
                       INNER JOIN Users u ON s.UserID = u.UserID
                       ORDER BY s.StudentNumber";
        
        return DatabaseConnection.ExecuteQuery(query);
    }

    /// <summary>
    /// Search students by name or student number
    /// </summary>
    public static DataTable SearchStudents(string searchText)
    {
        string query = @"SELECT s.StudentID, s.StudentNumber, u.FullName, s.DateOfBirth, 
                       s.PhoneNumber, s.City, s.Status, s.EnrollmentDate
                       FROM Students s
                       INNER JOIN Users u ON s.UserID = u.UserID
                       WHERE s.StudentNumber LIKE @SearchText OR u.FullName LIKE @SearchText
                       ORDER BY s.StudentNumber";
        
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@SearchText", "%" + searchText + "%")
        };

        return DatabaseConnection.ExecuteQuery(query, parameters);
    }

    /// <summary>
    /// Get student details by StudentID
    /// </summary>
    public static DataTable GetStudentByID(int studentID)
    {
        string query = @"SELECT s.StudentID, s.UserID, s.StudentNumber, u.FullName, s.DateOfBirth, 
                       s.PhoneNumber, s.Address, s.City, s.Status, s.EnrollmentDate, u.Email
                       FROM Students s
                       INNER JOIN Users u ON s.UserID = u.UserID
                       WHERE s.StudentID = @StudentID";
        
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@StudentID", studentID)
        };

        return DatabaseConnection.ExecuteQuery(query, parameters);
    }

    /// <summary>
    /// Insert new student
    /// </summary>
    public static int InsertStudent(int userID, string studentNumber, DateTime dateOfBirth, 
                                    string phoneNumber, string address, string city)
    {
        string query = @"INSERT INTO Students (UserID, StudentNumber, DateOfBirth, PhoneNumber, Address, City) 
                       VALUES (@UserID, @StudentNumber, @DateOfBirth, @PhoneNumber, @Address, @City)";
        
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@UserID", userID),
            new SqlParameter("@StudentNumber", studentNumber),
            new SqlParameter("@DateOfBirth", dateOfBirth),
            new SqlParameter("@PhoneNumber", phoneNumber),
            new SqlParameter("@Address", address),
            new SqlParameter("@City", city)
        };

        return DatabaseConnection.ExecuteNonQuery(query, parameters);
    }

    /// <summary>
    /// Update student information
    /// </summary>
    public static int UpdateStudent(int studentID, string phoneNumber, string address, string city, string status)
    {
        string query = @"UPDATE Students SET PhoneNumber = @PhoneNumber, Address = @Address, 
                       City = @City, Status = @Status WHERE StudentID = @StudentID";
        
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@PhoneNumber", phoneNumber),
            new SqlParameter("@Address", address),
            new SqlParameter("@City", city),
            new SqlParameter("@Status", status),
            new SqlParameter("@StudentID", studentID)
        };

        return DatabaseConnection.ExecuteNonQuery(query, parameters);
    }

    /// <summary>
    /// Delete student
    /// </summary>
    public static int DeleteStudent(int studentID)
    {
        string query = "DELETE FROM Students WHERE StudentID = @StudentID";
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@StudentID", studentID) };
        
        return DatabaseConnection.ExecuteNonQuery(query, parameters);
    }

    /// <summary>
    /// Get student by UserID
    /// </summary>
    public static int GetStudentIDByUserID(int userID)
    {
        string query = "SELECT StudentID FROM Students WHERE UserID = @UserID";
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", userID) };
        
        object result = DatabaseConnection.ExecuteScalar(query, parameters);
        return result != null ? Convert.ToInt32(result) : 0;
    }
}
