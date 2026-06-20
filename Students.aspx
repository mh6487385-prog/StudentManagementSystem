<%@ Page Title="Students" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="Students" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content">
        <h1 class="page-title">Student Management</h1>

        <!-- Search Box -->
        <div class="search-box">
            <asp:TextBox ID="searchTextBox" runat="server" placeholder="Search by student number or name..."></asp:TextBox>
            <asp:Button ID="searchButton" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="SearchButton_Click" />
            <asp:Button ID="clearButton" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="ClearButton_Click" />
            <% if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin") { %>
                <a href="AddStudent.aspx" class="btn btn-success">+ Add New Student</a>
            <% } %>
        </div>

        <!-- Messages -->
        <% if (!String.IsNullOrEmpty(SuccessMessage)) { %>
            <div class="alert alert-success">
                <%=SuccessMessage %>
            </div>
        <% } %>

        <% if (!String.IsNullOrEmpty(ErrorMessage)) { %>
            <div class="alert alert-danger">
                <%=ErrorMessage %>
            </div>
        <% } %>

        <!-- Students GridView -->
        <asp:GridView ID="studentsGridView" runat="server" CssClass="table" AllowPaging="true" PageSize="10"
            AutoGenerateColumns="false" OnPageIndexChanging="StudentsGridView_PageIndexChanging"
            OnRowCommand="StudentsGridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="StudentNumber" HeaderText="Student #" />
                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                <asp:BoundField DataField="DateOfBirth" HeaderText="Date of Birth" DataFormatString="{0:MM/dd/yyyy}" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone" />
                <asp:BoundField DataField="City" HeaderText="City" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" DataFormatString="{0:MM/dd/yyyy}" />
                <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="action-column">
                    <ItemTemplate>
                        <a href="ViewStudent.aspx?id=<%#Eval("StudentID")%>" class="btn btn-primary" style="font-size: 12px;">View</a>
                        <% if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin") { %>
                            <a href="AddStudent.aspx?id=<%#Eval("StudentID")%>" class="btn btn-warning" style="font-size: 12px;">Edit</a>
                            <asp:LinkButton ID="deleteButton" runat="server" CommandName="Delete" CommandArgument='<%#Eval("StudentID")%>'
                                CssClass="btn btn-danger" style="font-size: 12px;" 
                                OnClientClick="return confirm('Are you sure you want to delete this student?');">Delete</asp:LinkButton>
                        <% } %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <p style="padding: 20px;">No students found.</p>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
