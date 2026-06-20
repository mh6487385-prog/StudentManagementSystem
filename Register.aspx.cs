// ============================================================
// Register.aspx.cs - Registration Page Code Behind
// ============================================================

using System;

public partial class Register : System.Web.UI.Page
{
    public string ErrorMessage { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        // If already logged in, redirect to dashboard
        if (Session["UserID"] != null)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        string username = usernameTextBox.Text.Trim();
        string fullName = fullNameTextBox.Text.Trim();
        string email = emailTextBox.Text.Trim();
        string password = passwordTextBox.Text;
        string confirmPassword = confirmPasswordTextBox.Text;

        // Validate inputs
        if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(fullName) || 
            String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
        {
            ErrorMessage = "Please fill in all fields.";
            return;
        }

        if (password.Length < 6)
        {
            ErrorMessage = "Password must be at least 6 characters long.";
            return;
        }

        if (password != confirmPassword)
        {
            ErrorMessage = "Passwords do not match.";
            return;
        }

        try
        {
            // Check if username already exists
            if (UserManager.UsernameExists(username))
            {
                ErrorMessage = "Username already exists. Please choose another one.";
                return;
            }

            // Register new user
            int result = UserManager.RegisterUser(username, password, email, fullName, "Student");

            if (result > 0)
            {
                Response.Redirect("Login.aspx?msg=registered");
            }
            else
            {
                ErrorMessage = "Registration failed. Please try again.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error during registration: " + ex.Message;
        }
    }
}
