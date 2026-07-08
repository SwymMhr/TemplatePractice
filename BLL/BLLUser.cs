using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using TemplatingPractice.DAL;

namespace TemplatingPractice.BLL
{
    public class BLLUser
    {
        public DataTable GetAllUser()
        {
            return DAO.GetTableQuery("SELECT * FROM tblUSer", null);
        }

        public int CreateUser(string username, string password, string email)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@Username",username),
                new SqlParameter("@Password",password),
                new SqlParameter("@Email",email)
            };
            return DAO.ExecuteQuery("INSERT INTO tblUser(Username, Password, Email) VALUES (@Username, @Password, @Email)",param);
        }

        public int UpdateUser(int userid, string username, string password, string email)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@UserID",userid),
                new SqlParameter("@Username",username),
                new SqlParameter("@Password",password),
                new SqlParameter("@Email",email)
            };
            return DAO.ExecuteQuery("UPDATE tblUser SET Username=@Username, Password=@Password, Email=@Email WHERE UserID=@userid", param);
        }

        public int DeleteUser(int userid)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@UserID",userid)                
            };
            return DAO.ExecuteQuery("DELETE FROM tblUSer WHERE UserID = @UserID", param);
        }

        public DataTable LoginUser(string username, string password)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@Username",username),
                new SqlParameter("@Password",password)
            };
            return DAO.GetTableQuery("SELECT * FROM tblUser WHERE Username=@Username AND Password=@Password",param);
        }

        public DataTable GetUserById(int userid)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@UserID",userid)
            };
            return DAO.GetTableQuery("SELECT * FROM tblUSer WHERE UserID = @UserID", param);
        }

        // For password recovery
        public DataTable GetUserByEmail(string email)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@Email", email)
            };
            return DAO.GetTableQuery("SELECT * FROM tblUser WHERE Email=@Email", param);
        }

        public int UpdatePasswordByEmail(string email, string hashedPassword)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", hashedPassword)
            };
            return DAO.ExecuteQuery("UPDATE tblUser SET Password=@Password WHERE Email=@Email", param);
        }

        // For password reset functionality
        public string ResetPasswordForEmail(string email)
        {
            DataTable dt = GetUserByEmail(email);
            if (dt.Rows.Count == 0)
                return null;

            string newPlainPassword = GenerateRandomPassword(10);
            string hashed = RegisterForm.HashPassword(newPlainPassword);

            int rows = UpdatePasswordByEmail(email, hashed);
            return rows > 0 ? newPlainPassword : null;
        }

        private string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789!@#$%";
            byte[] bytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            var sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(chars[b % chars.Length]);
            return sb.ToString();
        }
    }
}