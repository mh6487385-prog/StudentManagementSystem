// ============================================================
// Site.Master.cs - Master Page Code Behind
// ============================================================

using System;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if user is logged in
        if (Session["UserID"] == null)
        {
            // Redirect to login if not logged in and not on login page
            if (!Request.Url.AbsolutePath.Contains("Login.aspx") && !Request.Url.AbsolutePath.Contains("Default.aspx"))
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}
