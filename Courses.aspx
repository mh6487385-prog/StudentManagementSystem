<%@ Page Title="Courses" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Courses.aspx.cs" Inherits="Courses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content">
        <h1 class="page-title">Course Management</h1>

        <!-- Search Box -->
        <div class="search-box">
            <asp:TextBox ID="searchTextBox" runat="server" placeholder="Search by course code or name..."></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="SearchButton_Click" />
            <asp:Button ID="clearButton" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="ClearButton_Click" />
            <% if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin") { %>
                <a href="AddCourse.aspx" class="btn btn-success">+ Add New Course</a>
            <% } %>
        </div>

        <!-- Messages -->
        <% if (!String.IsNullOrEmpty(SuccessMessage)) { %>
            <div class="alert alert-success">
                <%=SuccessMessage %>
            </div>
        <% } %>

        <!-- Courses GridView -->
        <asp:GridView ID="coursesGridView" runat="server" CssClass="table" AllowPaging="true" PageSize="10"
            AutoGenerateColumns="false" OnPageIndexChanging="CoursesGridView_PageIndexChanging"
            OnRowCommand="CoursesGridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="CourseCode" HeaderText="Code" />
                <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                <asp:BoundField DataField="Credits" HeaderText="Credits" />
                <asp:BoundField DataField="Semester" HeaderText="Semester" />
                <asp:BoundField DataField="InstructorName" HeaderText="Instructor" />
                <asp:BoundField DataField="MaxStudents" HeaderText="Max Students" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="action-column">
                    <ItemTemplate>
                        <% if (Session["UserType"].ToString() == "Student") { %>
                            <asp:LinkButton ID="enrollButton" runat="server" CommandName="Enroll" CommandArgument='<%#Eval("CourseID")%>'
                                CssClass="btn btn-success" style="font-size: 12px;">Enroll</asp:LinkButton>
                        <% } %>
                        <% if (Session["UserType"].ToString() == "Admin") { %>
                            <a href="AddCourse.aspx?id=<%#Eval("CourseID")%>" class="btn btn-warning" style="font-size: 12px;">Edit</a>
                            <asp:LinkButton ID="deleteButton" runat="server" CommandName="Delete" CommandArgument='<%#Eval("CourseID")%>'
                                CssClass="btn btn-danger" style="font-size: 12px;" 
                                OnClientClick="return confirm('Are you sure you want to delete this course?');">Delete</asp:LinkButton>
                        <% } %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <p style="padding: 20px;">No courses found.</p>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
