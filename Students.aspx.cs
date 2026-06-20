// ============================================================
// Students.aspx.cs - Students List Page Code Behind
// ============================================================

using System;
using System.Data;

public partial class Students : System.Web.UI.Page
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
            LoadStudents();
        }

        // Check for success message
        if (!String.IsNullOrEmpty(Request.QueryString["msg"]))
        {
            if (Request.QueryString["msg"] == "deleted")
                SuccessMessage = "Student deleted successfully!";
            else if (Request.QueryString["msg"] == "updated")
                SuccessMessage = "Student updated successfully!";
            else if (Request.QueryString["msg"] == "added")
                SuccessMessage = "Student added successfully!";
        }
    }

    private void LoadStudents()
    {
        try
        {
            DataTable dt = StudentManager.GetAllStudents();
            studentsGridView.DataSource = dt;
            studentsGridView.DataBind();
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error loading students: " + ex.Message;
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
            DataTable dt = StudentManager.SearchStudents(searchText);
            studentsGridView.DataSource = dt;
            studentsGridView.PageIndex = 0;
            studentsGridView.DataBind();
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error searching students: " + ex.Message;
        }
    }

    protected void ClearButton_Click(object sender, EventArgs e)
    {
        searchTextBox.Text = "";
        LoadStudents();
    }

    protected void StudentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        studentsGridView.PageIndex = e.NewPageIndex;
        LoadStudents();
    }

    protected void StudentsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int studentID = Convert.ToInt32(e.CommandArgument);
            try
            {
                StudentManager.DeleteStudent(studentID);
                Response.Redirect("Students.aspx?msg=deleted");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error deleting student: " + ex.Message;
            }
        }
    }
}
