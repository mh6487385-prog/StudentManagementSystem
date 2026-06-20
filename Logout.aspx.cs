// ============================================================
// Logout.aspx.cs - Logout Page Code Behind
// ============================================================

using System;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Clear all session variables
        Session.Clear();
        Session.Abandon();
        
        // Redirect to login after 2 seconds
        Response.AddHeader("Refresh", "3; url=Default.aspx");
    }
}
