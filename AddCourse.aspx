<%@ Page Title="Add/Edit Course" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="AddCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content">
        <h1 class="page-title"><asp:Label ID="pageTitle" runat="server" Text="Add New Course"></asp:Label></h1>

        <!-- Messages -->
        <% if (!String.IsNullOrEmpty(ErrorMessage)) { %>
            <div class="alert alert-danger">
                <%=ErrorMessage %>
            </div>
        <% } %>

        <!-- Course Form -->
        <form id="courseForm" runat="server">
            <div class="form-row">
                <div class="form-group">
                    <label for="courseCodeTextBox">Course Code:</label>
                    <asp:TextBox ID="courseCodeTextBox" runat="server" required="required"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="courseNameTextBox">Course Name:</label>
                    <asp:TextBox ID="courseNameTextBox" runat="server" required="required"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label for="descriptionTextBox">Description:</label>
                <asp:TextBox ID="descriptionTextBox" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label for="creditsTextBox">Credits:</label>
                    <asp:TextBox ID="creditsTextBox" runat="server" TextMode="Number" required="required"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="semesterTextBox">Semester:</label>
                    <asp:TextBox ID="semesterTextBox" runat="server" TextMode="Number" required="required"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label for="instructorDropDown">Instructor:</label>
                    <asp:DropDownList ID="instructorDropDown" runat="server" required="required"></asp:DropDownList>
                </div>

                <div class="form-group">
                    <label for="maxStudentsTextBox">Max Students:</label>
                    <asp:TextBox ID="maxStudentsTextBox" runat="server" TextMode="Number" required="required"></asp:TextBox>
                </div>
            </div>

            <div class="btn-group">
                <asp:Button ID="saveButton" runat="server" Text="Save Course" CssClass="btn btn-success" OnClick="SaveButton_Click" />
                <a href="Courses.aspx" class="btn btn-secondary">Cancel</a>
            </div>
        </form>
    </div>
</asp:Content>
