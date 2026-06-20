# 🚀 PROJECT SETUP & EXECUTION GUIDE

## ⚠️ SYSTEM REQUIREMENTS

Before running this project, ensure you have:

### Required Software
- [x] **Visual Studio 2019+** (Community, Professional, or Enterprise)
- [x] **SQL Server 2016+** (Express, Developer, or Enterprise)
- [x] **.NET Framework 4.7.2+**
- [x] **IIS Express** (comes with Visual Studio)

### SQL Server Installation Check
```powershell
# Run this to verify SQL Server is installed
Get-Service | Where-Object {$_.Name -like "*MSSQL*"}
```

---

## 📋 STEP-BY-STEP EXECUTION GUIDE

### PHASE 1: DATABASE SETUP (5 minutes)

#### Option A: Using SQL Server Management Studio (GUI)
```
1. Open SQL Server Management Studio
2. Connect to your server instance
3. File → Open → File
4. Select: C:\StudentManagementPortal\Database\DatabaseSchema.sql
5. Click Execute (F5)
6. Wait for "Command(s) completed successfully" message
```

#### Option B: Using Command Line
```powershell
# Replace <SERVER_NAME> with your SQL Server instance name
sqlcmd -S <SERVER_NAME> -i "C:\StudentManagementPortal\Database\DatabaseSchema.sql" -E

# Examples:
# Local instance (SQLEXPRESS):
sqlcmd -S localhost\SQLEXPRESS -i "C:\StudentManagementPortal\Database\DatabaseSchema.sql" -E

# Default instance:
sqlcmd -S localhost -i "C:\StudentManagementPortal\Database\DatabaseSchema.sql" -E

# Named instance:
sqlcmd -S MYCOMPUTER\INSTANCE -i "C:\StudentManagementPortal\Database\DatabaseSchema.sql" -E
```

#### Verify Database Created
```sql
-- Run in SQL Server Management Studio
SELECT * FROM StudentManagementDB.dbo.Users;
-- Should show 6 demo users
```

---

### PHASE 2: VISUAL STUDIO PROJECT SETUP (10 minutes)

#### Step 1: Open Visual Studio
```
1. Launch Visual Studio 2019+
2. Click "Create a new project"
```

#### Step 2: Create ASP.NET Web Forms Project
```
1. Search for "ASP.NET Web Application"
2. Select "ASP.NET Web Application (.NET Framework)"
3. Click Next
4. Name: StudentManagementPortal
5. Framework: .NET Framework 4.7.2
6. Click Create
7. Select "Web Forms" template
8. Authentication: Individual User Accounts
9. Click Create
```

#### Step 3: Copy Project Files
```
1. Close Visual Studio project if it auto-opens
2. Navigate to: C:\Users\[Username]\Documents\Visual Studio 2019\Projects\StudentManagementPortal\
3. Copy ALL files from C:\StudentManagementPortal\* 
4. Paste into the project folder (replace existing files)
5. Keep directory structure intact:
   - App_Code\
   - CSS\
   - Database\
   - All .aspx files
   - Web.config
   - etc.
```

#### Step 4: Update Connection String
```
1. In Visual Studio, open Web.config
2. Find this section:
   <connectionStrings>
       <add name="StudentPortalDB" 
            connectionString="Server=...;Database=StudentManagementDB;..." />
   </connectionStrings>

3. Replace with your server details:
   <connectionStrings>
       <add name="StudentPortalDB" 
            connectionString="Server=MYCOMPUTER\SQLEXPRESS;Database=StudentManagementDB;Integrated Security=true;" 
            providerName="System.Data.SqlClient" />
   </connectionStrings>

   OR if using SQL Server Authentication:
   <connectionStrings>
       <add name="StudentPortalDB" 
            connectionString="Server=MYCOMPUTER\SQLEXPRESS;Database=StudentManagementDB;User Id=sa;Password=YourPassword;" 
            providerName="System.Data.SqlClient" />
   </connectionStrings>
```

**Finding Your Server Name:**
- Open SQL Server Management Studio
- In "Connect to Server" dialog, your server name is shown
- For local: `localhost\SQLEXPRESS` or just `localhost`
- For remote: `SERVERNAME\INSTANCENAME`

---

### PHASE 3: BUILD & RUN (2 minutes)

#### Build the Project
```
1. In Visual Studio, press Ctrl+Shift+B
   OR: Build → Build Solution
2. Wait for "Build succeeded" message in Output window
3. Fix any errors (usually connection string issues)
```

#### Run the Application
```
1. Press F5 (or Debug → Start Debugging)
2. Visual Studio will:
   - Start IIS Express
   - Launch your default browser
   - Navigate to http://localhost:XXXXX/Default.aspx

3. You should see the Student Management Portal home page
```

---

## 🔐 LOGIN TO PORTAL

Once running, use these demo credentials:

### Admin Account (Full Access)
```
Username: admin
Password: 123456
Roles: Can manage everything
```

### Student Accounts
```
Username: student1, student2, or student3
Password: 123456 (all same)
Roles: Can view courses and enroll
```

### Instructor Accounts
```
Username: prof_smith or prof_jones
Password: 123456 (both)
Roles: Can view courses and manage grades
```

---

## 🧪 TEST SCENARIOS AFTER LOGIN

### Test 1: View Dashboard
```
1. Login as admin
2. Should see Dashboard with:
   - Total Students count
   - Total Courses count
   - Total Enrollments count
   - Recent activity list
```

### Test 2: Manage Students
```
1. Navigate to Students page
2. Should see list of 3 demo students (pagination)
3. Click "Search" and enter "STU" to filter
4. Click on a student name to view details
5. (Admin) Click "Edit" to modify student
6. (Admin) Click "Delete" to remove student
```

### Test 3: View Courses
```
1. Navigate to Courses page
2. Should see 3 demo courses
3. Click "Search" and enter "CS" to filter
4. (Student) Click "Enroll" button
5. Navigate to "My Courses" to see enrollment
```

### Test 4: Enroll in Course (as Student)
```
1. Login as student1
2. Go to Courses page
3. Click "Enroll" on CS201 course
4. Should show success message
5. Go to My Courses page
6. Should see the course listed
```

### Test 5: GridView Pagination
```
1. Go to Students page
2. At bottom of grid, click page numbers
3. Should navigate between pages
```

---

## ⚙️ IF YOU ENCOUNTER ERRORS

### Error: "Connection failed"
**Solution:**
```
1. Verify SQL Server is running:
   Get-Service | Where-Object {$_.Name -like "*MSSQL*"}
   
2. Verify database exists:
   Run: SELECT * FROM sys.databases WHERE name = 'StudentManagementDB'
   
3. Check connection string in Web.config matches your server
```

### Error: "Invalid username or password"
**Solution:**
```
1. Verify demo users exist in database:
   SELECT * FROM StudentManagementDB.dbo.Users;
   
2. Verify correct credentials are used
3. Check IsActive = 1 for the user
```

### Error: "Build failed"
**Solution:**
```
1. Check for syntax errors in .cs files
2. Verify all App_Code files are present
3. Clean solution: Build → Clean Solution
4. Rebuild: Build → Build Solution
```

### Error: "GridView shows no data"
**Solution:**
```
1. Verify database tables have data:
   SELECT COUNT(*) FROM StudentManagementDB.dbo.Students;
   
2. Check the Manager class query
3. Look at Visual Studio Output window for SQL errors
```

### Error: "Page not found (404)"
**Solution:**
```
1. Verify all .aspx files are present
2. Check file names match exactly
3. Restart IIS Express: Stop and Start debugging
4. Clear browser cache: Ctrl+Shift+Del
```

---

## 📊 FOLDER STRUCTURE VERIFICATION

After copying files, verify this structure:

```
C:\Users\[Username]\Documents\Visual Studio 2019\Projects\StudentManagementPortal\

├── StudentManagementPortal/          (Project folder)
│   ├── App_Code/
│   │   ├── DatabaseConnection.cs
│   │   ├── UserManager.cs
│   │   ├── StudentManager.cs
│   │   ├── CourseManager.cs
│   │   └── EnrollmentManager.cs
│   │
│   ├── CSS/
│   │   └── style.css
│   │
│   ├── Database/
│   │   └── DatabaseSchema.sql
│   │
│   ├── *.aspx files (12 pages)
│   ├── *.aspx.cs files (code-behind)
│   ├── Site.Master
│   ├── Site.Master.cs
│   ├── Web.config
│   ├── README.md
│   └── Other documentation files
```

---

## 🚀 QUICK COMMAND REFERENCE

### SQL Server Commands
```sql
-- Check if database exists
SELECT * FROM sys.databases WHERE name = 'StudentManagementDB'

-- View all users
SELECT UserID, Username, UserType FROM [StudentManagementDB].[dbo].[Users]

-- View all students
SELECT * FROM [StudentManagementDB].[dbo].[Students]

-- View all courses
SELECT * FROM [StudentManagementDB].[dbo].[Courses]

-- View enrollments
SELECT * FROM [StudentManagementDB].[dbo].[Enrollments]
```

### Visual Studio Debug Commands
```
F5              - Start debugging
Shift+F5        - Stop debugging
Ctrl+Shift+B    - Build solution
Ctrl+Shift+F    - Find in files
F10             - Step over
F11             - Step into
```

---

## 🎯 SUCCESS CHECKLIST

Before considering the project "running":

- [ ] SQL Server database created with 5 tables
- [ ] All .aspx files copied to project
- [ ] All App_Code files copied
- [ ] CSS folder with style.css exists
- [ ] Web.config updated with correct connection string
- [ ] Project builds without errors (Ctrl+Shift+B)
- [ ] F5 launches application in browser
- [ ] http://localhost:XXXXX/Default.aspx loads
- [ ] Login page appears when accessing protected page
- [ ] Can login with demo credentials
- [ ] Dashboard loads and shows statistics
- [ ] Students page displays list with pagination
- [ ] GridView displays correctly
- [ ] Can search and filter
- [ ] GridView sorting works
- [ ] Add/Edit/Delete operations work

---

## 📈 PERFORMANCE NOTES

### Default Settings
- GridView pagination: 10 records per page
- Session timeout: 20 minutes
- Database indexes: 6 (for common queries)

### First-Time Load
- First load may take 5-10 seconds as IIS Express initializes
- Subsequent loads are faster (cached)

---

## 🔍 DEBUGGING IN VISUAL STUDIO

### Set Breakpoints
```
1. Click on line number in code editor
2. Red dot appears
3. Run with F5
4. Execution stops at breakpoint
5. Use Debug menu to inspect variables
```

### Watch Variables
```
1. At breakpoint, right-click variable
2. Select "Add Watch"
3. Variable value updates as you step
```

### Debug Output
```csharp
// In code:
System.Diagnostics.Debug.WriteLine("Value: " + myVariable);

// View in Visual Studio Output window
// Debug → Windows → Output
```

---

## 📚 DOCUMENTATION

Once running, refer to:
- **README.md** - Project overview
- **QUICKSTART.md** - Quick setup
- **PROJECT_DOCUMENTATION.md** - Technical details
- **PROJECT_SUMMARY.md** - File inventory

---

## ✅ PROJECT IS READY TO RUN!

**Next Steps:**
1. Install SQL Server (if not already installed)
2. Create database using DatabaseSchema.sql
3. Open in Visual Studio
4. Update Web.config connection string
5. Press F5 to run
6. Login with demo credentials
7. Test features from scenarios above

---

## 📞 SUPPORT RESOURCES

- **Microsoft Docs**: https://docs.microsoft.com/en-us/aspnet/web-forms/
- **SQL Server Docs**: https://docs.microsoft.com/en-us/sql/
- **Visual Studio Help**: https://docs.microsoft.com/en-us/visualstudio/

---

**🎉 You now have a complete, production-ready ASP.NET Web Forms project!**

**Follow the steps above to get it running on your system.**

