// ============================================================
// Courses.aspx.cs - Courses Page Code Behind
// ============================================================

using System;
using System.Data;

public partial class Courses : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            LoadCourses();
        }

        // Check for success message
        if (!String.IsNullOrEmpty(Request.QueryString["msg"]))
        {
            if (Request.QueryString["msg"] == "enrolled")
                SuccessMessage = "You have been successfully enrolled in the course!";
            else if (Request.QueryString["msg"] == "deleted")
                SuccessMessage = "Course deleted successfully!";
        }
    }

    private void LoadCourses()
    {
        try
        {
            DataTable dt = CourseManager.GetAllCourses();
            coursesGridView.DataSource = dt;
            coursesGridView.DataBind();
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error loading courses: " + ex.Message;
        }
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        string searchText = searchTextBox.Text.Trim();
        if (String.IsNullOrEmpty(searchText))
        {
            ErrorMessage = "Please enter a search term.";
            return;
        }

        try
        {
            DataTable dt = CourseManager.SearchCourses(searchText);
            coursesGridView.DataSource = dt;
            coursesGridView.PageIndex = 0;
            coursesGridView.DataBind();
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error searching courses: " + ex.Message;
        }
    }

    protected void ClearButton_Click(object sender, EventArgs e)
    {
        searchTextBox.Text = "";
        LoadCourses();
    }

    protected void CoursesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        coursesGridView.PageIndex = e.NewPageIndex;
        LoadCourses();
    }

    protected void CoursesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Enroll")
        {
            int courseID = Convert.ToInt32(e.CommandArgument);
            int studentID = StudentManager.GetStudentIDByUserID(Convert.ToInt32(Session["UserID"]));

            if (studentID <= 0)
            {
                ErrorMessage = "Student record not found.";
                return;
            }

            try
            {
                EnrollmentManager.EnrollStudent(studentID, courseID);
                Response.Redirect("Courses.aspx?msg=enrolled");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        else if (e.CommandName == "Delete")
        {
            int courseID = Convert.ToInt32(e.CommandArgument);
            try
            {
                CourseManager.DeleteCourse(courseID);
                Response.Redirect("Courses.aspx?msg=deleted");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error deleting course: " + ex.Message;
            }
        }
    }
}
