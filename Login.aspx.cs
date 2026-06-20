// ============================================================
// Login.aspx.cs - Login Page Code Behind
// ============================================================

using System;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    public string ErrorMessage { get; set; }
    public string SuccessMessage { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        // If already logged in, redirect to dashboard
        if (Session["UserID"] != null)
        {
            Response.Redirect("Dashboard.aspx");
        }

        // Show success message if coming from registration
        if (!String.IsNullOrEmpty(Request.QueryString["msg"]))
        {
            SuccessMessage = "Registration successful! Please login with your credentials.";
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string username = usernameTextBox.Text.Trim();
        string password = passwordTextBox.Text.Trim();

        if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
        {
            ErrorMessage = "Please enter both username and password.";
            return;
        }

        try
        {
            // Call UserManager to authenticate
            DataTable userTable = UserManager.AuthenticateUser(username, password);

            if (userTable.Rows.Count > 0)
            {
                // Login successful - set session variables
                DataRow user = userTable.Rows[0];
                Session["UserID"] = Convert.ToInt32(user["UserID"]);
                Session["Username"] = user["Username"].ToString();
                Session["FullName"] = user["FullName"].ToString();
                Session["Email"] = user["Email"].ToString();
                Session["UserType"] = user["UserType"].ToString();

                // Update last login time
                UserManager.UpdateLastLogin(Convert.ToInt32(user["UserID"]));

                // Redirect to dashboard
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                ErrorMessage = "Invalid username or password. Please try again.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Error during login: " + ex.Message;
        }
    }
}
