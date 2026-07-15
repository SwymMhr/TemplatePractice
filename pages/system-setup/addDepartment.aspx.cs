using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup
{
    public partial class addDepartment : System.Web.UI.Page
    {
        BLLDepartment bld = new BLLDepartment();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Departmentcode = txtDepartmentID.Text;
            string Departmentname = txtDepartmentName.Text;

            string status = "Active";
            if (rbInactive.Checked) status = "Inactive";

            int result = bld.CreateDepartment(Departmentcode, Departmentname, status);
            if (result > 0)
            {
                Response.Redirect("~/DepartmentList");
            }
            else
            {
                ShowAlert("Department Creation Failed!", "error");
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DepartmentList");
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