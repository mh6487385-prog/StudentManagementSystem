<%@ Page Title="My Enrollments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyEnrollments.aspx.cs" Inherits="MyEnrollments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-content">
        <h1 class="page-title">My Courses</h1>

        <!-- Messages -->
        <% if (!String.IsNullOrEmpty(SuccessMessage)) { %>
            <div class="alert alert-success">
                <%=SuccessMessage %>
            </div>
        <% } %>

        <!-- Enrollments GridView -->
        <asp:GridView ID="enrollmentsGridView" runat="server" CssClass="table" AllowPaging="true" PageSize="10"
            AutoGenerateColumns="false" OnPageIndexChanging="EnrollmentsGridView_PageIndexChanging"
            OnRowCommand="EnrollmentsGridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="CourseCode" HeaderText="Code" />
                <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
                <asp:BoundField DataField="Credits" HeaderText="Credits" />
                <asp:BoundField DataField="Semester" HeaderText="Semester" />
                <asp:BoundField DataField="InstructorName" HeaderText="Instructor" />
                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                <asp:BoundField DataField="Marks" HeaderText="Marks" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="action-column">
                    <ItemTemplate>
                        <asp:LinkButton ID="detailsButton" runat="server" CommandName="Details" CommandArgument='<%#Eval("CourseID")%>'
                            CssClass="btn btn-primary" style="font-size: 12px;">Details</asp:LinkButton>
                        <% if (Eval("Status").ToString() == "Enrolled") { %>
                            <asp:LinkButton ID="dropButton" runat="server" CommandName="Drop" CommandArgument='<%#Eval("CourseID")%>'
                                CssClass="btn btn-danger" style="font-size: 12px;"
                                OnClientClick="return confirm('Are you sure you want to drop this course?');">Drop Course</asp:LinkButton>
                        <% } %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <p style="padding: 20px;">You are not enrolled in any courses.</p>
            </EmptyDataTemplate>
        </asp:GridView>

        <div style="margin-top: 20px;">
            <a href="Courses.aspx" class="btn btn-primary">Browse More Courses</a>
        </div>
    </div>
</asp:Content>
