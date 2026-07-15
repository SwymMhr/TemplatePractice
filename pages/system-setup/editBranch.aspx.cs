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
    public partial class editBranch : System.Web.UI.Page
    {
        BLLBranch blb = new BLLBranch();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindValues();
            }
        }

        protected void BindValues()
        {
            int branchid = Convert.ToInt32(Request.QueryString["branchid"]);
            DataTable dt = blb.GetBranchById(branchid);

            txtBranchID.Text = dt.Rows[0]["BranchCode"].ToString();
            txtBranchName.Text = dt.Rows[0]["BranchName"].ToString();
            bool isoutbranch = Convert.ToBoolean(dt.Rows[0]["IsOutBranch"]);
            if (isoutbranch)
            {
                chkIsOutBranch.Checked = true;
            }
            string status = dt.Rows[0]["Status"].ToString();
            if(status == "Inactive")
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
            int branchid = Convert.ToInt32(Request.QueryString["branchid"]);
            string branchcode = txtBranchID.Text;
            string branchname = txtBranchName.Text;
            bool isoutbranch = false;
            if (chkIsOutBranch.Checked) isoutbranch = true;
            string status = "Active";
            if (rbInactive.Checked) status = "Inactive";

            int result = blb.UpdateBranch(branchid, branchcode, branchname, isoutbranch, status);
            if (result > 0)
            {
                Response.Redirect("~/branchList");
            }
            else
            {
                ShowAlert("Branch Update Failed!", "error");
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