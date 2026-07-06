using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice
{
    public partial class RegisterForm : System.Web.UI.Page
    {
        BLLUser blu = new BLLUser();
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = HashPassword(txtPassword.Text);
            string email = txtEmail.Text;            

            int result = blu.CreateUser(username, password, email);
            if(result > 0)
            {
                ShowAlert("Registration Success!", "success");
            }
            else
            {
                ShowAlert("Registration Failed!","error");
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

        private void ShowAlert(string message, string type)
        {
            message = message.Replace("'", "\\'");

            string script = $@"
                Swal.fire({{
                    title: '{message}',
                    icon: '{type}',
                    confirmButtonText: 'OK'
                }});
            ";

            ClientScript.RegisterStartupScript(this.GetType(), "registrationError", script, true);
        }

    }
}