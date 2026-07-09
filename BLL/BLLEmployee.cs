using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using TemplatingPractice.DAL;

namespace TemplatingPractice.BLL
{
    public class BLLEmployee
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public DataTable GetAllEmployee()
        {
            return DAO.GetTableQuery("SELECT * FROM tblEmployee ORDER BY EmployeeID ASC", null);
        }

        public DataTable GetEmployeeById(int employeeId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId)
            };
            return DAO.GetTableQuery("SELECT * FROM tblEmployee WHERE EmployeeID = @EmployeeID", param);
        }

        public DataTable GetEmployeeLookupById(int employeeId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId)
            };

            string query = @"SELECT e.EmployeeID,
                           e.EmployeeName,
                           d.DesignationName,
                           dep.DepartmentName,
                           b.BranchName
                    FROM tblEmployee e
                    LEFT JOIN tblDesignation d ON e.DesignationID = d.DesignationID
                    LEFT JOIN tblDepartment dep ON e.DepartmentID = dep.DepartmentID
                    LEFT JOIN tblBranch b ON e.BranchID = b.BranchID
                    WHERE e.EmployeeID = @EmployeeID";

            return DAO.GetTableQuery(query, param);
        }

        public DataTable GetEmployeeListView()
        {
            string query = @"SELECT e.EmployeeID,
                                   e.EmployeeCode,
                                   e.EmployeeName,
                                   d.DesignationName,
                                   g.GradeName,
                                   dep.DepartmentName,
                                   b.BranchName,
                                   e.UserType,
                                   e.Status
                            FROM tblEmployee e
                            LEFT JOIN tblDesignation d ON e.DesignationID = d.DesignationID
                            LEFT JOIN tblGrade g ON e.GradeID = g.GradeID
                            LEFT JOIN tblDepartment dep ON e.DepartmentID = dep.DepartmentID
                            LEFT JOIN tblBranch b ON e.BranchID = b.BranchID
                            ORDER BY e.EmployeeID ASC";

            return DAO.GetTableQuery(query, null);
        }

        // Like GetEmployeeByBranchAndDepartment, but departmentId is optional —
        // pass null to include every department within the branch.
        public DataTable GetEmployeesForAttendanceReport(int branchId, int? departmentId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@BranchID", branchId),
                new SqlParameter("@DepartmentID", (object)departmentId ?? DBNull.Value)
            };

            string query = @"SELECT e.EmployeeID, e.EmployeeName, dep.DepartmentName
                      FROM tblEmployee e
                      LEFT JOIN tblDepartment dep ON e.DepartmentID = dep.DepartmentID
                      WHERE e.BranchID = @BranchID
                        AND (@DepartmentID IS NULL OR e.DepartmentID = @DepartmentID)
                      ORDER BY e.EmployeeName ASC";

            return DAO.GetTableQuery(query, param);
        }

        public DataTable GetEmployeeReport(int branchId, int? departmentId, string status, string employeeType, string sortBy)
        {
            string orderBy = sortBy == "EmpID" ? "e.EmployeeID ASC" : "e.EmployeeName ASC";

            string query = $@"SELECT e.EmployeeID,
                                   e.EmployeeName,
                                   e.Gender,
                                   e.Relationship,
                                   e.DOBEnglish,
                                   e.JoinDateEnglish,
                                   d.DesignationName,
                                   g.GradeName,
                                   dep.DepartmentName,
                                   b.BranchName,
                                   e.EmployeeType,
                                   e.Status,
                                   sup.EmployeeName AS HODName,
                                   e.UserType
                            FROM tblEmployee e
                            LEFT JOIN tblDesignation d ON e.DesignationID = d.DesignationID
                            LEFT JOIN tblGrade g ON e.GradeID = g.GradeID
                            LEFT JOIN tblDepartment dep ON e.DepartmentID = dep.DepartmentID
                            LEFT JOIN tblBranch b ON e.BranchID = b.BranchID
                            LEFT JOIN tblEmployee sup ON e.SupervisorID = sup.EmployeeID
                            WHERE e.BranchID = @BranchID
                              AND (@DepartmentID IS NULL OR e.DepartmentID = @DepartmentID)
                              AND e.Status = @Status
                              AND e.EmployeeType = @EmployeeType
                            ORDER BY {orderBy}";

            SqlParameter[] param =
            {
                new SqlParameter("@BranchID", branchId),
                new SqlParameter("@DepartmentID", (object)departmentId ?? DBNull.Value),
                new SqlParameter("@Status", status),
                new SqlParameter("@EmployeeType", employeeType)
            };

            return DAO.GetTableQuery(query, param);
        }

        public DataTable GetEmployeeByStatus(string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@Status", status)
            };
            return DAO.GetTableQuery("SELECT * FROM tblEmployee WHERE Status = @Status", param);
        }

        public DataTable GetEmployeeByBranchAndDepartment(int branchId, int departmentId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@BranchID", branchId),
                new SqlParameter("@DepartmentID", departmentId)
            };
            return DAO.GetTableQuery(@" SELECT EmployeeID, EmployeeName 
                                        FROM tblEmployee 
                                        WHERE BranchID = @BranchID AND DepartmentID = @DepartmentID
                                        ORDER BY EmployeeName ASC", param);
        }

        public bool IsEmployeeCodeExists(string employeeCode)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeCode", employeeCode)
            };
            DataTable dt = DAO.GetTableQuery("SELECT EmployeeID FROM tblEmployee WHERE EmployeeCode = @EmployeeCode", param);
            return dt.Rows.Count > 0;
        }

        public bool IsLoginIdExists(string loginId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@LoginID", loginId)
            };
            DataTable dt = DAO.GetTableQuery("SELECT EmployeeID FROM tblEmployee WHERE LoginID = @LoginID", param);
            return dt.Rows.Count > 0;
        }

        public int CreateEmployee(string employeeCode, string employeeName, string gender, string relationship,
            DateTime? dobEnglish, string dobNepali, DateTime joinDateEnglish, string joinDateNepali,
            int designationId, int gradeId, int departmentId, int branchId, string email,
            string employeeType, string userType, string status, int? supervisorId,
            string loginId, string password, byte[] imageData)
        {
            string hashedPassword = HashPassword(password);

            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeCode", employeeCode),
                new SqlParameter("@EmployeeName", employeeName),
                new SqlParameter("@Gender", gender),
                new SqlParameter("@Relationship", relationship),
                new SqlParameter("@DOBEnglish", (object)dobEnglish ?? DBNull.Value),
                new SqlParameter("@DOBNepali", (object)dobNepali ?? DBNull.Value),
                new SqlParameter("@JoinDateEnglish", joinDateEnglish),
                new SqlParameter("@JoinDateNepali", (object)joinDateNepali ?? DBNull.Value),
                new SqlParameter("@DesignationID", designationId),
                new SqlParameter("@GradeID", gradeId),
                new SqlParameter("@DepartmentID", departmentId),
                new SqlParameter("@BranchID", branchId),
                new SqlParameter("@Email", (object)email ?? DBNull.Value),
                new SqlParameter("@EmployeeType", employeeType),
                new SqlParameter("@UserType", userType),
                new SqlParameter("@Status", status),
                new SqlParameter("@SupervisorID", (object)supervisorId ?? DBNull.Value),
                new SqlParameter("@LoginID", loginId),
                new SqlParameter("@Password", hashedPassword),
                new SqlParameter("@ImageData", (object)imageData ?? DBNull.Value)
            };

            string query = @"INSERT INTO tblEmployee
                (EmployeeCode, EmployeeName, Gender, Relationship, DOBEnglish, DOBNepali, JoinDateEnglish, JoinDateNepali, DesignationID, GradeID, DepartmentID, BranchID, Email, EmployeeType, UserType, Status, SupervisorID, LoginID, Password, ImageData)
                VALUES
                (@EmployeeCode, @EmployeeName, @Gender, @Relationship, @DOBEnglish, @DOBNepali, @JoinDateEnglish, @JoinDateNepali, @DesignationID, @GradeID, @DepartmentID, @BranchID, @Email, @EmployeeType, @UserType, @Status, @SupervisorID, @LoginID, @Password, @ImageData)";

            return DAO.ExecuteQuery(query, param);
        }

        // Pass imageData = null to leave the existing image untouched.
        public int UpdateEmployee(int employeeId, string employeeName, string gender, string relationship,
            DateTime? dobEnglish, string dobNepali, DateTime joinDateEnglish, string joinDateNepali,
            int designationId, int gradeId, int departmentId, int branchId, string email,
            string employeeType, string userType, string status, int? supervisorId, byte[] imageData)
        {
            string setClause = @"EmployeeName = @EmployeeName,
                        Gender = @Gender,
                        Relationship = @Relationship,
                        DOBEnglish = @DOBEnglish,
                        DOBNepali = @DOBNepali,
                        JoinDateEnglish = @JoinDateEnglish,
                        JoinDateNepali = @JoinDateNepali,
                        DesignationID = @DesignationID,
                        GradeID = @GradeID,
                        DepartmentID = @DepartmentID,
                        BranchID = @BranchID,
                        Email = @Email,
                        EmployeeType = @EmployeeType,
                        UserType = @UserType,
                        Status = @Status,
                        SupervisorID = @SupervisorID";

            if (imageData != null)
                setClause += ", ImageData = @ImageData";

            string query = "UPDATE tblEmployee SET " + setClause + " WHERE EmployeeID = @EmployeeID";

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", employeeId));
            cmd.Parameters.Add(new SqlParameter("@EmployeeName", employeeName));
            cmd.Parameters.Add(new SqlParameter("@Gender", gender));
            cmd.Parameters.Add(new SqlParameter("@Relationship", relationship));
            cmd.Parameters.Add(new SqlParameter("@DOBEnglish", (object)dobEnglish ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@DOBNepali", (object)dobNepali ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@JoinDateEnglish", joinDateEnglish));
            cmd.Parameters.Add(new SqlParameter("@JoinDateNepali", (object)joinDateNepali ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@DesignationID", designationId));
            cmd.Parameters.Add(new SqlParameter("@GradeID", gradeId));
            cmd.Parameters.Add(new SqlParameter("@DepartmentID", departmentId));
            cmd.Parameters.Add(new SqlParameter("@BranchID", branchId));
            cmd.Parameters.Add(new SqlParameter("@Email", (object)email ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@EmployeeType", employeeType));
            cmd.Parameters.Add(new SqlParameter("@UserType", userType));
            cmd.Parameters.Add(new SqlParameter("@Status", status));
            cmd.Parameters.Add(new SqlParameter("@SupervisorID", (object)supervisorId ?? DBNull.Value));

            if (imageData != null)
                cmd.Parameters.Add(new SqlParameter("@ImageData", imageData));

            SqlParameter[] param = new SqlParameter[cmd.Parameters.Count];
            cmd.Parameters.CopyTo(param, 0);

            return DAO.ExecuteQuery(query, param);
        }

        public int UpdateStatus(int employeeId, string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@Status", status)
            };
            return DAO.ExecuteQuery("UPDATE tblEmployee SET Status = @Status WHERE EmployeeID = @EmployeeID", param);
        }

        public int DeleteEmployee(int employeeId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId)
            };
            return DAO.ExecuteQuery("DELETE FROM tblEmployee WHERE EmployeeID = @EmployeeID", param);
        }
    }
}