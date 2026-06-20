// ============================================================
// USER MANAGEMENT CLASS (DAL)
// App_Code/UserManager.cs
// ============================================================

using System;
using System.Data;
using System.Data.SqlClient;

public class UserManager
{
    /// <summary>
    /// Authenticate user (Login)
    /// </summary>
    public static DataTable AuthenticateUser(string username, string password)
    {
        string query = "SELECT UserID, Username, FullName, Email, UserType FROM Users WHERE Username = @Username AND Password = @Password AND IsActive = 1";
        
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@Username", username),
            new SqlParameter("@Password", password)
        };

        return DatabaseConnection.ExecuteQuery(query, parameters);
    }

    /// <summary>
    /// Register new user
    /// </summary>
    public static int RegisterUser(string username, string password, string email, string fullName, string userType)
    {
        string query = @"INSERT INTO Users (Username, Password, Email, FullName, UserType) 
                       VALUES (@Username, @Password, @Email, @FullName, @UserType)";
        
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@Username", username),
            new SqlParameter("@Password", password),
            new SqlParameter("@Email", email),
            new SqlParameter("@FullName", fullName),
            new SqlParameter("@UserType", userType)
        };

        return DatabaseConnection.ExecuteNonQuery(query, parameters);
    }

    /// <summary>
    /// Check if username exists
    /// </summary>
    public static bool UsernameExists(string username)
    {
        string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Username", username) };
        
        object result = DatabaseConnection.ExecuteScalar(query, parameters);
        return Convert.ToInt32(result) > 0;
    }

    /// <summary>
    /// Get user by UserID
    /// </summary>
    public static DataTable GetUserByID(int userID)
    {
        string query = "SELECT * FROM Users WHERE UserID = @UserID";
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", userID) };
        
        return DatabaseConnection.ExecuteQuery(query, parameters);
    }

    /// <summary>
    /// Update last login time
    /// </summary>
    public static void UpdateLastLogin(int userID)
    {
        string query = "UPDATE Users SET LastLogin = GETDATE() WHERE UserID = @UserID";
        SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserID", userID) };
        
        DatabaseConnection.ExecuteNonQuery(query, parameters);
    }
}
