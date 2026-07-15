using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice
{
    public partial class page_recoverpw : System.Web.UI.Page
    {
        BLLUser blu = new BLLUser();
        protected void btnRecover_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            string email = txtEmail.Text.Trim();
            string newPassword = blu.ResetPasswordForEmail(email);

            if (!string.IsNullOrEmpty(newPassword))
            {
                try
                {
                    SendPasswordResetEmail(email, newPassword);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Password reset email failed: " + ex.Message);
                }
            }
            Response.Redirect("page-confirm-mail?email=" + HttpUtility.UrlEncode(email));
        }

        private void SendPasswordResetEmail(string email, string newPassword)
        {
            string body = $@"
            <html><body style='font-family:Arial,sans-serif;color:#333'>
              <div style='max-width:600px;margin:auto;border:1px solid #e0e0e0;border-radius:8px;overflow:hidden'>

                <div style='background:#dc3545;padding:20px;text-align:center'>
                  <h2 style='color:white;margin:0'>Password Recovery</h2>
                </div>

                <div style='padding:24px'>
                  <p>Hello,</p>
                  <p>We received a request to reset your password. Your new temporary password is:</p>

                  <p style='font-size:20px;font-weight:bold;background:#f8f9fa;padding:12px;
                            border-radius:4px;text-align:center;letter-spacing:1px'>
                    {newPassword}
                  </p>

                  <p style='margin-top:20px;color:#555'>
                    Please log in and change this password as soon as possible.
                    If you did not request this, please contact support immediately.
                  </p>
                </div>

                <div style='background:#f8f9fa;padding:16px;text-align:center;font-size:12px;color:#888'>
                  This is an automated message — please do not reply.
                </div>
              </div>
            </body></html>";

            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(SmtpConfig.From, "Account Recovery");
                mail.To.Add(email);
                mail.Subject = "Your New Password";
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient(SmtpConfig.Host, int.Parse(SmtpConfig.Port)))
                {
                    smtp.Credentials = new NetworkCredential(SmtpConfig.User, SmtpConfig.Pass);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}