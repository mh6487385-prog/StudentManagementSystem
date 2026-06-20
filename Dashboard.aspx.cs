// ============================================================
// Dashboard.aspx.cs - Dashboard Page Code Behind
// ============================================================

using System;
using System.Data;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check authentication
        if (Session["UserID"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            LoadDashboardData();
        }
    }

    private void LoadDashboardData()
    {
        try
        {
            // Get total students
            string query1 = "SELECT COUNT(*) FROM Students";
            object result1 = DatabaseConnection.ExecuteScalar(query1);
            totalStudentsLabel.Text = result1.ToString();

            // Get total courses
            string query2 = "SELECT COUNT(*) FROM Courses";
            object result2 = DatabaseConnection.ExecuteScalar(query2);
            totalCoursesLabel.Text = result2.ToString();

            // Get total enrollments
            string query3 = "SELECT COUNT(*) FROM Enrollments";
            object result3 = DatabaseConnection.ExecuteScalar(query3);
            totalEnrollmentsLabel.Text = result3.ToString();

            // Get student's enrolled courses (if student)
            if (Session["UserType"].ToString() == "Student")
            {
                int studentID = StudentManager.GetStudentIDByUserID(Convert.ToInt32(Session["UserID"]));
                if (studentID > 0)
                {
                    string query4 = "SELECT COUNT(*) FROM Enrollments WHERE StudentID = @StudentID";
                    System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@StudentID", studentID)
                    };
                    object result4 = DatabaseConnection.ExecuteScalar(query4, param);
                    myCoursesLabel.Text = result4.ToString();
                }
            }

            // Load recent enrollments
            LoadRecentEnrollments();
        }
        catch (Exception ex)
        {
            // Handle error
        }
    }

    private void LoadRecentEnrollments()
    {
        try
        {
            string query = @"SELECT TOP 5 u.FullName as StudentName, c.CourseName, e.Status, e.EnrollmentDate
                           FROM Enrollments e
                           INNER JOIN Students s ON e.StudentID = s.StudentID
                           INNER JOIN Users u ON s.UserID = u.UserID
                           INNER JOIN Courses c ON e.CourseID = c.CourseID
                           ORDER BY e.EnrollmentDate DESC";

            DataTable dt = DatabaseConnection.ExecuteQuery(query);
            recentEnrollmentsGridView.DataSource = dt;
            recentEnrollmentsGridView.DataBind();
        }
        catch (Exception ex)
        {
            // Handle error
        }
    }

    protected void RecentEnrollmentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        recentEnrollmentsGridView.PageIndex = e.NewPageIndex;
        LoadRecentEnrollments();
    }
}
