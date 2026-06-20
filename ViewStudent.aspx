<%@ Page Title="View Student Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewStudent.aspx.cs" Inherits="ViewStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content">
        <h1 class="page-title">Student Details</h1>

        <div style="margin-bottom: 20px;">
            <a href="Students.aspx" class="btn btn-secondary">Back to Students</a>
        </div>

        <!-- Student Information -->
        <div style="background-color: #f9f9f9; padding: 20px; border-radius: 5px; margin-bottom: 20px;">
            <h3 style="color: #2c3e50;">Personal Information</h3>
            <div class="form-row">
                <div>
                    <p><strong>Student Number:</strong> <asp:Label ID="studentNumberLabel" runat="server"></asp:Label></p>
                    <p><strong>Full Name:</strong> <asp:Label ID="fullNameLabel" runat="server"></asp:Label></p>
                    <p><strong>Email:</strong> <asp:Label ID="emailLabel" runat="server"></asp:Label></p>
                </div>
                <div>
                    <p><strong>Date of Birth:</strong> <asp:Label ID="dobLabel" runat="server"></asp:Label></p>
                    <p><strong>Phone Number:</strong> <asp:Label ID="phoneLabel" runat="server"></asp:Label></p>
                    <p><strong>City:</strong> <asp:Label ID="cityLabel" runat="server"></asp:Label></p>
                </div>
            </div>
        </div>

        <!-- Student's Courses -->
        <div>
            <h3 style="color: #2c3e50;">Enrolled Courses</h3>
            <asp:GridView ID="coursesGridView" runat="server" CssClass="table" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="CourseCode" HeaderText="Course Code" />
                    <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                    <asp:BoundField DataField="Credits" HeaderText="Credits" />
                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
                    <asp:BoundField DataField="Marks" HeaderText="Marks" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                </Columns>
                <EmptyDataTemplate>
                    <p>No courses found.</p>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
