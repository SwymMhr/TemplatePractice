using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup
{
    public partial class addLeave : System.Web.UI.Page
    {
        BLLLeave bll = new BLLLeave();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Leavename = txtLeaveName.Text;
            string leavetype = "Expire Yearly";
            if (rbAccumulative.Checked)
                leavetype = "Accumulative";
            else if (rbServicePeriod.Checked)
                leavetype = "Service Period";

            bool cashable = false;
            if (rbCashable.Checked) cashable = true;

            bool monthlyEarning = false;
            if (chkMonthlyEarning.Checked) monthlyEarning = true;

            bool exhaustALlLeaves = false;
            if (chkExhaustAllLeaves.Checked) exhaustALlLeaves = true;

            string status = "Active";
            if (rbInactive.Checked) status = "Inactive";

            int result = bll.CreateLeave(Leavename, leavetype, cashable, monthlyEarning, exhaustALlLeaves, status);
            if (result > 0)
            {
                Response.Redirect("~/leaveList");
            }
            else
            {
                ShowAlert("Leave Creation Failed!", "error");
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/leaveList");
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