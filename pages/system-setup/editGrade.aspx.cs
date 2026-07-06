using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup
{
    public partial class editGrade : System.Web.UI.Page
    {
        BLLGrade blg = new BLLGrade();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindValues();
            }
        }

        protected void BindValues()
        {
            int Gradeid = Convert.ToInt32(Request.QueryString["Gradeid"]);
            DataTable dt = blg.GetGradeById(Gradeid);

            txtGradeType.Text = dt.Rows[0]["GradeType"].ToString();
            txtGradeName.Text = dt.Rows[0]["GradeName"].ToString();

            string status = dt.Rows[0]["Status"].ToString();
            if (status == "Inactive")
            {
                rbInactive.Checked = true;
            }
            else
            {
                rbActive.Checked = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int Gradeid = Convert.ToInt32(Request.QueryString["Gradeid"]);
            string Gradetype = txtGradeType.Text;
            string Gradename = txtGradeName.Text;

            string status = "Active";
            if (rbInactive.Checked) status = "Inactive";

            int result = blg.UpdateGrade(Gradeid, Gradename, Gradetype, status);
            if (result > 0)
            {
                Response.Redirect("gradeList.aspx");
            }
            else
            {
                ShowAlert("Grade Update Failed!", "error");
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("gradeList.aspx");
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