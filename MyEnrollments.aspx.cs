// ============================================================
// MyEnrollments.aspx.cs - My Enrollments Page Code Behind
// ============================================================

using System;
using System.Data;

public partial class MyEnrollments : System.Web.UI.Page
{
    public string SuccessMessage { get; set; }
    public string ErrorMessage { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check authentication
        if (Session["UserID"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        // Check if student
        if (Session["UserType"].ToString() != "Student")
        {
            Response.Redirect("Default.aspx");
        }

        if (!IsPostBack)
        {
            LoadMyEnrollments();
        }

        if (!String.IsNullOrEmpty(Request.QueryString["msg"]))
        {
            if (Request.QueryString["msg"] == "dropped")
                SuccessMessage = "Course dropped successfully!";
        }
    }

    private void LoadMyEnrollments()
    {
        try
        {
            int studentID = StudentManager.GetStudentIDByUserID(Convert.ToInt32(Session["UserID"]));
            if (studentID > 0)
            {
                DataTable dt = CourseManager.GetStudentCourses(studentID);
                enrollmentsGridView.DataSource = dt;
                enrollmentsGridView.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error loading enrollments: " + ex.Message;
        }
    }

    protected void EnrollmentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        enrollmentsGridView.PageIndex = e.NewPageIndex;
        LoadMyEnrollments();
    }

    protected void EnrollmentsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Drop")
        {
            int courseID = Convert.ToInt32(e.CommandArgument);
            int studentID = StudentManager.GetStudentIDByUserID(Convert.ToInt32(Session["UserID"]));

            try
            {
                // Find enrollment and drop it
                string query = "SELECT EnrollmentID FROM Enrollments WHERE StudentID = @StudentID AND CourseID = @CourseID";
                System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@StudentID", studentID),
                    new System.Data.SqlClient.SqlParameter("@CourseID", courseID)
                };
                
                object enrollmentID = DatabaseConnection.ExecuteScalar(query, param);
                if (enrollmentID != null)
                {
                    EnrollmentManager.DropEnrollment(Convert.ToInt32(enrollmentID));
                    Response.Redirect("MyEnrollments.aspx?msg=dropped");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error dropping course: " + ex.Message;
            }
        }
    }
}
