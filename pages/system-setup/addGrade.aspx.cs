using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup
{
    public partial class addGrade : System.Web.UI.Page
    {
        BLLGrade blg = new BLLGrade();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string GradeType = txtGradeType.Text;
            string Gradename = txtGradeName.Text;

            string status = "Active";
            if (rbInactive.Checked) status = "Inactive";

            int result = blg.CreateGrade(Gradename, GradeType, status);
            if (result > 0)
            {
                Response.Redirect("~/GradeList");
            }
            else
            {
                ShowAlert("Grade Creation Failed!", "error");
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GradeList");
        }

        private void ShowAlert(string message, string type)
        {
            string script = $@"
                Swal.fire({{
                    title: '{message}',
                    icon: '{type}',
                    confirmButtonText: 'OK'
                }});
            ";

            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
    }
}