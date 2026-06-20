<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login - Student Management Portal</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="loginForm" runat="server">
        <div class="login-container">
            <div class="login-box">
                <h2 class="login-title">📚 Student Portal</h2>

                <!-- Error Message -->
                <% if (!String.IsNullOrEmpty(ErrorMessage)) { %>
                    <div class="alert alert-danger">
                        <%=ErrorMessage %>
                    </div>
                <% } %>

                <!-- Success Message -->
                <% if (!String.IsNullOrEmpty(SuccessMessage)) { %>
                    <div class="alert alert-success">
                        <%=SuccessMessage %>
                    </div>
                <% } %>

                <!-- Login Form -->
                <div class="form-group">
                    <label for="usernameTextBox">Username:</label>
                    <asp:TextBox ID="usernameTextBox" runat="server" placeholder="Enter your username"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="passwordTextBox">Password:</label>
                    <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Button ID="loginButton" runat="server" Text="Login" CssClass="btn btn-primary" 
                        OnClick="LoginButton_Click" Style="width: 100%; cursor: pointer;" />
                </div>

                <div class="login-links">
                    <a href="Default.aspx">Back to Home</a>
                    <a href="Register.aspx">Register New Account</a>
                </div>

                <!-- Demo Credentials -->
                <div style="margin-top: 30px; padding-top: 20px; border-top: 1px solid #e0e0e0; font-size: 12px; color: #7f8c8d;">
                    <strong>Demo Credentials:</strong><br />
                    Admin: admin / 123456<br />
                    Student: student1 / 123456<br />
                    Instructor: prof_smith / 123456
                </div>
            </div>
        </div>
    </form>
</body>
</html>
