// ============================================================
// ViewStudent.aspx.cs - View Student Details Page Code Behind
// ============================================================

using System;
using System.Data;

public partial class ViewStudent : System.Web.UI.Page
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
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int studentID = Convert.ToInt32(Request.QueryString["id"]);
                LoadStudentDetails(studentID);
                LoadStudentCourses(studentID);
            }
        }
    }

    private void LoadStudentDetails(int studentID)
    {
        try
        {
            DataTable dt = StudentManager.GetStudentByID(studentID);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                studentNumberLabel.Text = row["StudentNumber"].ToString();
                fullNameLabel.Text = row["FullName"].ToString();
                emailLabel.Text = row["Email"].ToString();
                dobLabel.Text = Convert.ToDateTime(row["DateOfBirth"]).ToString("MM/dd/yyyy");
                phoneLabel.Text = row["PhoneNumber"].ToString();
                cityLabel.Text = row["City"].ToString();
            }
        }
        catch (Exception ex)
        {
            // Handle error
        }
    }

    private void LoadStudentCourses(int studentID)
    {
        try
        {
            DataTable dt = CourseManager.GetStudentCourses(studentID);
            coursesGridView.DataSource = dt;
            coursesGridView.DataBind();
        }
        catch (Exception ex)
        {
            // Handle error
        }
    }
}
