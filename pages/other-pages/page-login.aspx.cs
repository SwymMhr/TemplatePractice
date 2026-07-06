using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice
{
    public partial class LoginPage : System.Web.UI.Page
    {
        BLLUser blu = new BLLUser();
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = HashPassword(txtPassword.Text);

            DataTable result = blu.LoginUser(username, password);
            if(result.Rows.Count > 0)
            {
                Session["UserId"] = result.Rows[0]["UserId"];
                Session["Username"] = result.Rows[0]["Username"];
                Session["Email"] = result.Rows[0]["Email"];
                Response.Redirect("~/pages/dashboard/dashboard.aspx");
            }
            else
            {
                string script = @"
                    Swal.fire({
                        icon: 'error',
                        title: 'Login Failed',
                        text: 'Invalid username or password.',
                        confirmButtonColor: '#f05050'
                    });
                ";

                ClientScript.RegisterStartupScript( this.GetType(), "loginError", script, true );
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(
                    Encoding.UTF8.GetBytes(password));

                return Convert.ToBase64String(bytes);
            }
        }
    }
}