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
    public partial class editDepartment : System.Web.UI.Page
    {
        BLLDepartment bld = new BLLDepartment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindValues();
            }
        }

        protected void BindValues()
        {
            int Departmentid = Convert.ToInt32(Request.QueryString["Departmentid"]);
            DataTable dt = bld.GetDepartmentById(Departmentid);

            txtDepartmentID.Text = dt.Rows[0]["DepartmentCode"].ToString();
            txtDepartmentName.Text = dt.Rows[0]["DepartmentName"].ToString();

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
            int Departmentid = Convert.ToInt32(Request.QueryString["Departmentid"]);
            string Departmentcode = txtDepartmentID.Text;
            string Departmentname = txtDepartmentName.Text;

            string status = "Active";
            if (rbInactive.Checked) status = "Inactive";

            int result = bld.UpdateDepartment(Departmentid, Departmentcode, Departmentname, status);
            if (result > 0)
            {
                Response.Redirect("departmentList.aspx");
            }
            else
            {
                ShowAlert("Department Update Failed!", "error");
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("departmentList.aspx");
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