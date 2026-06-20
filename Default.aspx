<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content">
        <h1 class="page-title">Welcome to Student Management Portal</h1>
        
        <% if (Session["UserID"] == null) { %>
            <div class="alert alert-info">
                <strong>Welcome!</strong> Please log in to access the portal.
            </div>
            <p style="text-align: center; margin-top: 20px;">
                <a href="Login.aspx" class="btn btn-primary">Login to Portal</a>
            </p>
        <% } else { %>
            <div class="alert alert-success">
                <strong>Welcome, <%=Session["FullName"]%>!</strong> You are logged in to the Student Management Portal.
            </div>

            <h2 style="margin-top: 30px; color: #2c3e50;">About This System</h2>
            <p>
                The Student Management Portal is a comprehensive system designed to manage student enrollments, courses, and academic progress. 
                This system allows students to view their courses and grades, while administrators can manage student records, courses, and enrollments.
            </p>

            <h3 style="margin-top: 20px; color: #2c3e50;">Features:</h3>
            <ul style="margin-left: 20px;">
                <li>Student Information Management</li>
                <li>Course Management and Enrollment</li>
                <li>Grade Tracking and Management</li>
                <li>User Authentication and Authorization</li>
                <li>Dashboard with Statistics</li>
            </ul>

            <div style="margin-top: 30px;">
                <a href="Dashboard.aspx" class="btn btn-primary">Go to Dashboard</a>
            </div>
        <% } %>
    </div>
</asp:Content>
