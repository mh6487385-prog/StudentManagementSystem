<%@ Page Title="Add/Edit Student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="AddStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content">
        <h1 class="page-title"><asp:Label ID="pageTitle" runat="server" Text="Add New Student"></asp:Label></h1>

        <!-- Messages -->
        <% if (!String.IsNullOrEmpty(ErrorMessage)) { %>
            <div class="alert alert-danger">
                <%=ErrorMessage %>
            </div>
        <% } %>

        <!-- Student Form -->
        <form id="studentForm" runat="server">
            <div class="form-row">
                <div class="form-group">
                    <label for="studentNumberTextBox">Student Number:</label>
                    <asp:TextBox ID="studentNumberTextBox" runat="server" required="required"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="fullNameTextBox">Full Name:</label>
                    <asp:TextBox ID="fullNameTextBox" runat="server" required="required"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label for="emailTextBox">Email:</label>
                    <asp:TextBox ID="emailTextBox" runat="server" TextMode="Email" required="required"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="dateOfBirthTextBox">Date of Birth:</label>
                    <asp:TextBox ID="dateOfBirthTextBox" runat="server" TextMode="Date" required="required"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label for="phoneNumberTextBox">Phone Number:</label>
                    <asp:TextBox ID="phoneNumberTextBox" runat="server" required="required"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="cityTextBox">City:</label>
                    <asp:TextBox ID="cityTextBox" runat="server" required="required"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label for="addressTextBox">Address:</label>
                <asp:TextBox ID="addressTextBox" runat="server" TextMode="MultiLine" Rows="3" required="required"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="statusDropDown">Status:</label>
                <asp:DropDownList ID="statusDropDown" runat="server">
                    <asp:ListItem Value="Active">Active</asp:ListItem>
                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                    <asp:ListItem Value="Graduated">Graduated</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="btn-group">
                <asp:Button ID="saveButton" runat="server" Text="Save Student" CssClass="btn btn-success" OnClick="SaveButton_Click" />
                <a href="Students.aspx" class="btn btn-secondary">Cancel</a>
            </div>
        </form>
    </div>
</asp:Content>
