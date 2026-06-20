// ============================================================
// AddStudent.aspx.cs - Add/Edit Student Page Code Behind
// ============================================================

using System;
using System.Data;

public partial class AddStudent : System.Web.UI.Page
{
    public string ErrorMessage { get; set; }
    private int studentID = 0;

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
            // Check if editing existing student
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                studentID = Convert.ToInt32(Request.QueryString["id"]);
                LoadStudentData(studentID);
                pageTitle.Text = "Edit Student";
            }
        }
    }

    private void LoadStudentData(int id)
    {
        try
        {
            DataTable dt = StudentManager.GetStudentByID(id);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                studentNumberTextBox.Text = row["StudentNumber"].ToString();
                fullNameTextBox.Text = row["FullName"].ToString();
                emailTextBox.Text = row["Email"].ToString();
                dateOfBirthTextBox.Text = Convert.ToDateTime(row["DateOfBirth"]).ToString("yyyy-MM-dd");
                phoneNumberTextBox.Text = row["PhoneNumber"].ToString();
                addressTextBox.Text = row["Address"].ToString();
                cityTextBox.Text = row["City"].ToString();
                statusDropDown.SelectedValue = row["Status"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error loading student data: " + ex.Message;
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            // Validate inputs
            if (String.IsNullOrEmpty(studentNumberTextBox.Text) || String.IsNullOrEmpty(fullNameTextBox.Text))
            {
                ErrorMessage = "Please fill in all required fields.";
                return;
            }

            if (studentID > 0)
            {
                // Update existing student
                int result = StudentManager.UpdateStudent(
                    studentID,
                    phoneNumberTextBox.Text,
                    addressTextBox.Text,
                    cityTextBox.Text,
                    statusDropDown.SelectedValue
                );

                if (result > 0)
                {
                    Response.Redirect("Students.aspx?msg=updated");
                }
            }
            else
            {
                // Create new user first
                int userResult = UserManager.RegisterUser(
                    studentNumberTextBox.Text.ToLower(),
                    "123456", // Default password
                    emailTextBox.Text,
                    fullNameTextBox.Text,
                    "Student"
                );

                if (userResult > 0)
                {
                    // Get the new UserID
                    string query = "SELECT UserID FROM Users WHERE Email = @Email";
                    System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@Email", emailTextBox.Text)
                    };
                    object userID = DatabaseConnection.ExecuteScalar(query, param);

                    // Insert new student
                    int result = StudentManager.InsertStudent(
                        Convert.ToInt32(userID),
                        studentNumberTextBox.Text,
                        Convert.ToDateTime(dateOfBirthTextBox.Text),
                        phoneNumberTextBox.Text,
                        addressTextBox.Text,
                        cityTextBox.Text
                    );

                    if (result > 0)
                    {
                        Response.Redirect("Students.aspx?msg=added");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error saving student: " + ex.Message;
        }
    }
}
