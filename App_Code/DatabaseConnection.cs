// ============================================================
// DATABASE CONNECTION CLASS
// App_Code/DatabaseConnection.cs
// ============================================================

using System;
using System.Data;
using System.Data.SqlClient;

public class DatabaseConnection
{
    // Connection String (Update with your SQL Server instance)
    private static string connectionString = "Server=localhost;Database=StudentManagementDB;User Id=sa;Password=YourPassword;";

    /// <summary>
    /// Get database connection object
    /// </summary>
    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    /// <summary>
    /// Execute SQL Query and return DataTable
    /// </summary>
    public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    // Add parameters if provided
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Database Query Error: " + ex.Message);
        }
        return dt;
    }

    /// <summary>
    /// Execute Non-Query (Insert, Update, Delete)
    /// </summary>
    public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
    {
        int rowsAffected = 0;
        try
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    // Add parameters if provided
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Database Command Error: " + ex.Message);
        }
        return rowsAffected;
    }

    /// <summary>
    /// Execute Scalar (returns single value)
    /// </summary>
    public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
    {
        object result = null;
        try
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    // Add parameters if provided
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    conn.Open();
                    result = cmd.ExecuteScalar();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Database Scalar Error: " + ex.Message);
        }
        return result;
    }
}
