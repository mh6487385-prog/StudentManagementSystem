<%@ Page Title="Register" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Register - Student Management Portal</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="registerForm" runat="server">
        <div class="login-container">
            <div class="login-box" style="max-width: 500px;">
                <h2 class="login-title">Create Account</h2>

                <!-- Error Message -->
                <% if (!String.IsNullOrEmpty(ErrorMessage)) { %>
                    <div class="alert alert-danger">
                        <%=ErrorMessage %>
                    </div>
                <% } %>

                <!-- Registration Form -->
                <div class="form-group">
                    <label for="usernameTextBox">Username:</label>
                    <asp:TextBox ID="usernameTextBox" runat="server" placeholder="Choose a username"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="fullNameTextBox">Full Name:</label>
                    <asp:TextBox ID="fullNameTextBox" runat="server" placeholder="Enter your full name"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="emailTextBox">Email:</label>
                    <asp:TextBox ID="emailTextBox" runat="server" TextMode="Email" placeholder="Enter your email"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="passwordTextBox">Password:</label>
                    <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password" placeholder="Choose a password"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="confirmPasswordTextBox">Confirm Password:</label>
                    <asp:TextBox ID="confirmPasswordTextBox" runat="server" TextMode="Password" placeholder="Confirm your password"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Button ID="registerButton" runat="server" Text="Create Account" CssClass="btn btn-success" 
                        OnClick="RegisterButton_Click" Style="width: 100%; cursor: pointer;" />
                </div>

                <div class="login-links" style="text-align: center;">
                    <a href="Login.aspx">Already have an account? Login</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
