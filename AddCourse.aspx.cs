// ============================================================
// AddCourse.aspx.cs - Add/Edit Course Page Code Behind
// ============================================================

using System;
using System.Data;

public partial class AddCourse : System.Web.UI.Page
{
    public string ErrorMessage { get; set; }
    private int courseID = 0;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check authentication and authorization
        if (Session["UserID"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (Session["UserType"].ToString() != "Admin")
        {
            Response.Redirect("Default.aspx");
        }

        if (!IsPostBack)
        {
            LoadInstructors();

            // Check if editing existing course
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                courseID = Convert.ToInt32(Request.QueryString["id"]);
                LoadCourseData(courseID);
                pageTitle.Text = "Edit Course";
            }
        }
    }

    private void LoadInstructors()
    {
        try
        {
            string query = "SELECT UserID, FullName FROM Users WHERE UserType = 'Instructor' ORDER BY FullName";
            DataTable dt = DatabaseConnection.ExecuteQuery(query);

            instructorDropDown.DataSource = dt;
            instructorDropDown.DataTextField = "FullName";
            instructorDropDown.DataValueField = "UserID";
            instructorDropDown.DataBind();
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error loading instructors: " + ex.Message;
        }
    }

    private void LoadCourseData(int id)
    {
        try
        {
            DataTable dt = CourseManager.GetCourseByID(id);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                courseCodeTextBox.Text = row["CourseCode"].ToString();
                courseNameTextBox.Text = row["CourseName"].ToString();
                descriptionTextBox.Text = row["Description"].ToString();
                creditsTextBox.Text = row["Credits"].ToString();
                semesterTextBox.Text = row["Semester"].ToString();
                instructorDropDown.SelectedValue = row["InstructorID"].ToString();
                maxStudentsTextBox.Text = row["MaxStudents"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error loading course data: " + ex.Message;
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            // Validate inputs
            if (String.IsNullOrEmpty(courseCodeTextBox.Text) || String.IsNullOrEmpty(courseNameTextBox.Text))
            {
                ErrorMessage = "Please fill in all required fields.";
                return;
            }

            if (courseID > 0)
            {
                // Update existing course
                int result = CourseManager.UpdateCourse(
                    courseID,
                    courseNameTextBox.Text,
                    descriptionTextBox.Text,
                    Convert.ToInt32(creditsTextBox.Text),
                    Convert.ToInt32(maxStudentsTextBox.Text),
                    "Active"
                );

                if (result > 0)
                {
                    Response.Redirect("Courses.aspx?msg=updated");
                }
            }
            else
            {
                // Insert new course
                int result = CourseManager.InsertCourse(
                    courseCodeTextBox.Text,
                    courseNameTextBox.Text,
                    descriptionTextBox.Text,
                    Convert.ToInt32(creditsTextBox.Text),
                    Convert.ToInt32(semesterTextBox.Text),
                    Convert.ToInt32(instructorDropDown.SelectedValue),
                    Convert.ToInt32(maxStudentsTextBox.Text)
                );

                if (result > 0)
                {
                    Response.Redirect("Courses.aspx?msg=added");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error saving course: " + ex.Message;
        }
    }
}
