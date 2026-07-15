using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup
{
    public partial class addBranch : System.Web.UI.Page
    {
        BLLBranch blb = new BLLBranch();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string branchcode = txtBranchID.Text;
            string branchname = txtBranchName.Text;
            bool isoutbranch = false;
            if (chkIsOutBranch.Checked) isoutbranch = true; 
            string status = "Active";
            if (rbInactive.Checked) status = "Inactive";

            int result = blb.CreateBranch(branchcode, branchname, isoutbranch, status);
            if (result > 0)
            {
                Response.Redirect("~/branchList");
            }
            else
            {
                ShowAlert("Branch Creation Failed!", "error");
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/branchList");
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