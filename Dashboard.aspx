<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content">
        <h1 class="page-title">Dashboard</h1>

        <!-- Dashboard Cards -->
        <div class="dashboard-grid">
            <div class="dashboard-card">
                <h3>Total Students</h3>
                <div class="dashboard-card-value"><asp:Label ID="totalStudentsLabel" runat="server" Text="0"></asp:Label></div>
                <div class="dashboard-card-label">Active student accounts</div>
            </div>

            <div class="dashboard-card">
                <h3>Total Courses</h3>
                <div class="dashboard-card-value"><asp:Label ID="totalCoursesLabel" runat="server" Text="0"></asp:Label></div>
                <div class="dashboard-card-label">Available courses</div>
            </div>

            <div class="dashboard-card">
                <h3>Total Enrollments</h3>
                <div class="dashboard-card-value"><asp:Label ID="totalEnrollmentsLabel" runat="server" Text="0"></asp:Label></div>
                <div class="dashboard-card-label">Active enrollments</div>
            </div>

            <div class="dashboard-card">
                <h3>My Courses</h3>
                <div class="dashboard-card-value"><asp:Label ID="myCoursesLabel" runat="server" Text="0"></asp:Label></div>
                <div class="dashboard-card-label">Enrolled courses</div>
            </div>
        </div>

        <!-- Quick Actions -->
        <div style="margin-top: 40px;">
            <h2 style="color: #2c3e50;">Quick Actions</h2>
            <div class="btn-group" style="margin-top: 15px;">
                <a href="Students.aspx" class="btn btn-primary">View Students</a>
                <a href="Courses.aspx" class="btn btn-primary">View Courses</a>
                <% if (Session["UserType"] != null && Session["UserType"].ToString() == "Student") { %>
                    <a href="MyEnrollments.aspx" class="btn btn-primary">My Courses</a>
                <% } %>
                <% if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin") { %>
                    <a href="AddStudent.aspx" class="btn btn-success">Add New Student</a>
                <% } %>
            </div>
        </div>

        <!-- Recent Activity -->
        <div style="margin-top: 40px;">
            <h2 style="color: #2c3e50;">Recent Enrollments</h2>
            <asp:GridView ID="recentEnrollmentsGridView" runat="server" CssClass="table" AllowPaging="true" PageSize="5"
                AutoGenerateColumns="false" OnPageIndexChanging="RecentEnrollmentsGridView_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                    <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" DataFormatString="{0:MM/dd/yyyy}" />
                </Columns>
                <EmptyDataTemplate>
                    <p>No recent enrollments found.</p>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
