# QUICK START GUIDE
## Student Management Portal - Setup & Run Instructions

---

## STEP 1: DATABASE SETUP (5 minutes)

### Option A: Using SQL Server Management Studio

1. **Open SQL Server Management Studio**
   - Connect to your SQL Server instance

2. **Create Database**
   - Right-click on "Databases"
   - Select "New Database"
   - Name: `StudentManagementDB`
   - Click OK

3. **Run SQL Script**
   - Open: `Database/DatabaseSchema.sql`
   - Select all code (Ctrl+A)
   - Execute (F5)
   - This will:
     - Create 5 tables
     - Create indexes
     - Insert sample data
     - Create test users

4. **Verify Setup**
   ```sql
   -- Run this to check if everything is set up
   SELECT * FROM Users;           -- Should see 6 demo users
   SELECT * FROM Students;        -- Should see 3 students
   SELECT * FROM Courses;         -- Should see 3 courses
   SELECT * FROM Enrollments;     -- Should see 5 enrollments
   ```

### Option B: Using Command Line

```powershell
sqlcmd -S localhost -U sa -P YourPassword -i "Database\DatabaseSchema.sql"
```

---

## STEP 2: VISUAL STUDIO SETUP (5 minutes)

### Create New ASP.NET Web Forms Project

1. **Open Visual Studio 2019+**

2. **Create New Project**
   - File > New > Project
   - Select "ASP.NET Web Application (.NET Framework)"
   - Name: `StudentManagementPortal`
   - Framework: .NET Framework 4.7.2
   - Click Create

3. **Select Project Template**
   - Choose "Web Forms"
   - Authentication: "Individual User Accounts"
   - Click Create

4. **Copy Project Files**
   - Copy all files from this folder to your project
   - Replace the default files

### Update Connection String

1. **Open Web.config**

2. **Find and update**:
   ```xml
   <connectionStrings>
       <add name="StudentPortalDB" 
            connectionString="Server=YOUR_SERVER;Database=StudentManagementDB;User Id=sa;Password=YourPassword;" 
            providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

   Replace:
   - `YOUR_SERVER` with your SQL Server name (e.g., "DESKTOP-ABC123\SQLEXPRESS")
   - `YourPassword` with your SQL Server password

3. **Find your SQL Server name**:
   - Open SQL Server Management Studio
   - In "Connect to Server" dialog, your server name is displayed
   - Or run this in command line:
     ```sql
     sqlcmd -L
     ```

---

## STEP 3: BUILD & RUN (2 minutes)

1. **Build Project**
   - Press Ctrl+Shift+B
   - Or: Build > Build Solution
   - Wait for "Build succeeded" message

2. **Start Debugging**
   - Press F5
   - Visual Studio will compile and launch IIS Express
   - Browser opens to: http://localhost:XXXXX/Default.aspx

3. **Login to Portal**
   - You'll see demo credentials on the home page
   - Use: 
     - Username: `admin`
     - Password: `123456`
   - Click Login

---

## STEP 4: EXPLORE FEATURES (10 minutes)

### As Admin (username: admin)
1. Go to Dashboard - See statistics
2. Go to Students - View all students
3. Click "+ Add New Student" - Create new student
4. Go to Courses - View all courses
5. Click "+ Add New Course" - Create new course

### As Student (username: student1)
1. Logout from admin account
2. Login with student1 / 123456
3. Go to Dashboard - See your courses
4. Go to Courses - Enroll in a new course
5. Go to My Courses - See your enrollments

### Try Search Features
1. On Students page - Search for "STU" or "John"
2. On Courses page - Search for "CS" or "Programming"

---

## PROJECT STRUCTURE

```
StudentManagementPortal/
├── App_Code/
│   ├── DatabaseConnection.cs       (Database connection class)
│   ├── UserManager.cs              (User operations)
│   ├── StudentManager.cs           (Student CRUD)
│   ├── CourseManager.cs            (Course CRUD)
│   └── EnrollmentManager.cs        (Enrollment CRUD)
├── CSS/
│   └── style.css                   (Styling for all pages)
├── Database/
│   └── DatabaseSchema.sql          (Database creation script)
├── Default.aspx (+ .cs)            (Home page)
├── Login.aspx (+ .cs)              (Login page)
├── Register.aspx (+ .cs)           (Registration)
├── Logout.aspx (+ .cs)             (Logout)
├── Dashboard.aspx (+ .cs)          (Dashboard with stats)
├── Students.aspx (+ .cs)           (Student list)
├── AddStudent.aspx (+ .cs)         (Add/Edit student)
├── ViewStudent.aspx (+ .cs)        (Student details)
├── Courses.aspx (+ .cs)            (Course list)
├── AddCourse.aspx (+ .cs)          (Add/Edit course)
├── MyEnrollments.aspx (+ .cs)      (My courses)
├── Site.Master (+ .cs)             (Master page)
├── Web.config                      (Configuration)
├── PROJECT_DOCUMENTATION.md        (Full documentation)
└── QUICKSTART.md                   (This file)
```

---

## KEY DEMO ACCOUNTS

| Username | Password | Role | Can Do |
|----------|----------|------|---------|
| admin | 123456 | Admin | Everything - manage students, courses, users |
| student1 | 123456 | Student | View courses, enroll, check grades |
| student2 | 123456 | Student | View courses, enroll, check grades |
| student3 | 123456 | Student | View courses, enroll, check grades |
| prof_smith | 123456 | Instructor | View courses, manage grades |
| prof_jones | 123456 | Instructor | View courses, manage grades |

---

## TESTING SCENARIOS

### Test 1: User Authentication
```
1. Go to Login page
2. Enter: admin / 123456
3. Should see Dashboard
4. Check Session variables set correctly
5. Click Logout
6. Should redirect to Default page
```

### Test 2: Add New Student (Admin)
```
1. Login as admin
2. Go to Students > + Add New Student
3. Fill form:
   - Student Number: STU004
   - Full Name: Test Student
   - Email: test@college.edu
   - Date of Birth: 01/01/2004
   - Phone: 555-0104
   - Address: 123 Test St
   - City: Test City
4. Click Save Student
5. Should see "Student added successfully!"
6. New student appears in list
```

### Test 3: Search Functionality
```
1. Go to Students page
2. Enter search text: "STU" or "John"
3. Click Search
4. Should filter results
5. Click Clear to reset
```

### Test 4: Student Enrollment
```
1. Login as student1
2. Go to Courses
3. Click "Enroll" on a course
4. Should show "successfully enrolled"
5. Go to My Courses
6. Should see enrolled course
7. Try to Drop course
8. Confirm it's removed from My Courses
```

### Test 5: GridView Pagination
```
1. Go to Students page (if many records)
2. Should see page numbers at bottom
3. Click page 2, 3, etc.
4. Should show different students
```

---

## COMMON ERRORS & SOLUTIONS

### Error: "Connection string is not configured"
**Solution**: Check Web.config connection string
```xml
<!-- Make sure this exists in Web.config -->
<connectionStrings>
  <add name="StudentPortalDB" connectionString="Server=...;Database=StudentManagementDB;..." />
</connectionStrings>
```

### Error: "Login failed for user 'sa'"
**Solution**: Update SQL Server credentials in Web.config
- Verify your SQL Server username and password
- If using Windows Authentication, change to: `Integrated Security=true;`

### Error: "Invalid username or password"
**Solution**: Make sure demo data is inserted
```sql
SELECT COUNT(*) FROM Users;  -- Should be 6
```

### Error: "GridView shows no data"
**Solution**: 
1. Check if data exists in database
2. Verify table joins are correct
3. Check for NULL values
4. Use breakpoints to debug

### Error: "Session variables are NULL"
**Solution**: Make sure Login page is setting session
```csharp
Session["UserID"] = Convert.ToInt32(user["UserID"]);
Session["FullName"] = user["FullName"].ToString();
// etc.
```

---

## SQL SERVER QUICK COMMANDS

```sql
-- Check if database exists
SELECT * FROM sys.databases WHERE name = 'StudentManagementDB'

-- Check all users
SELECT UserID, Username, UserType, IsActive FROM Users

-- Check all students
SELECT StudentID, StudentNumber, FullName FROM Students

-- Check all courses
SELECT CourseID, CourseCode, CourseName FROM Courses

-- Check enrollments
SELECT * FROM Enrollments

-- Count records
SELECT COUNT(*) as TotalUsers FROM Users
SELECT COUNT(*) as TotalStudents FROM Students
SELECT COUNT(*) as TotalCourses FROM Courses

-- Delete all sample data (if needed)
DELETE FROM Assignments
DELETE FROM Enrollments
DELETE FROM Courses
DELETE FROM Students
DELETE FROM Users
```

---

## VISUAL STUDIO DEBUGGING

### Set Breakpoint
1. Click on line number in code
2. Red dot appears
3. When code executes, it stops there

### Debug Variables
```csharp
// Mouse over variable to see value
string username = textBox.Text;  // Hover over 'username'

// Use Debug Output
System.Diagnostics.Debug.WriteLine("Username: " + username);
```

### Check Session Values
```csharp
protected void Page_Load(object sender, EventArgs e)
{
    // Check if session is set
    if (Session["UserID"] != null)
    {
        Response.Write("UserID: " + Session["UserID"]);
    }
}
```

---

## PERFORMANCE TIPS

1. **Use Indexes**: Database schema includes indexes on frequently searched columns
2. **Pagination**: GridView limits to 10 records per page
3. **Stored Procedures**: Can be added later for complex operations
4. **Caching**: Can cache course list since it changes less frequently

---

## NEXT STEPS / ENHANCEMENTS

- [ ] Add email notifications
- [ ] Implement grades submission by instructor
- [ ] Add PDF reports
- [ ] Mobile app using ASP.NET Mobile
- [ ] Add more security (HTTPS, 2FA)
- [ ] Create REST API for mobile
- [ ] Add audit logging
- [ ] Implement payment system
- [ ] Add performance optimization

---

## SUPPORT & HELP

### Resources
- Microsoft Docs: https://docs.microsoft.com/en-us/aspnet/
- SQL Server Docs: https://docs.microsoft.com/en-us/sql/
- ASP.NET Tutorials: https://www.asp.net/tutorials

### Ask Questions
1. Check PROJECT_DOCUMENTATION.md for detailed info
2. Look at existing code comments
3. Search error message in Google
4. Debug using breakpoints

---

## SUCCESS CHECKLIST

Before submitting project, verify:

- [ ] Database created with 5 tables
- [ ] All pages load without errors
- [ ] Login works with demo credentials
- [ ] Student list displays with pagination
- [ ] Can add new student
- [ ] Can search students
- [ ] Can view student details
- [ ] Course list displays
- [ ] Can enroll in course
- [ ] My Courses shows enrollments
- [ ] Dashboard shows statistics
- [ ] GridView displays correctly
- [ ] Error messages appear on validation
- [ ] Success messages appear on operations
- [ ] Logout works and clears session
- [ ] Role-based access control works

---

## PROJECT COMPLETION

**Congratulations!** You now have a fully functional Student Management Portal with:

✓ Professional web interface
✓ Complete database design
✓ User authentication
✓ CRUD operations
✓ Search & filter
✓ Dashboard with statistics
✓ Role-based access control
✓ Responsive design

**Ready to submit your semester project!**

---

*Created: 2026-06-07*
*Version: 1.0*
*Difficulty Level: Intermediate*
*Estimated Setup Time: 15 minutes*
*Estimated Project Runtime: 30+ minutes*
