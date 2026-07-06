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
    public partial class editLeave : System.Web.UI.Page
    {
        BLLLeave bll = new BLLLeave();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindValues();
            }
        }

        protected void BindValues()
        {
            int Leaveid = Convert.ToInt32(Request.QueryString["Leaveid"]);
            DataTable dt = bll.GetLeaveById(Leaveid);

            txtLeaveName.Text = dt.Rows[0]["LeaveName"].ToString();

            string leaveType = Convert.ToString(dt.Rows[0]["LeaveType"]);
            if (leaveType == "Accumulative")
            {
                rbAccumulative.Checked = true;
            }
            else if(leaveType == "Service Period")
            {
                rbServicePeriod.Checked = true;
            }

            bool cashable = Convert.ToBoolean(dt.Rows[0]["Cashable"]);
            if (!cashable)
            {
                rbNotCashable.Checked = true;
            }

            bool monthlyEarning = Convert.ToBoolean(dt.Rows[0]["MonthlyEarning"]);
            if (monthlyEarning)
            {
                chkMonthlyEarning.Checked = true;
            }

            bool exhaustAllLeaves = Convert.ToBoolean(dt.Rows[0]["ExhaustAllLeaves"]);
            if (exhaustAllLeaves)
            {
                chkExhaustAllLeaves.Checked = true;
            }

            string status = dt.Rows[0]["Status"].ToString();
            if (status == "Inactive")
            {
                rbInactive.Checked = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int Leaveid = Convert.ToInt32(Request.QueryString["Leaveid"]);
            string Leavename = txtLeaveName.Text;
            string leavetype = "Expire Yearly";
            if (rbAccumulative.Checked) leavetype = "Accumulative";
            if (rbServicePeriod.Checked) leavetype = "Service Period";

            bool cashable = false;
            if (rbCashable.Checked) cashable = true;

            bool monthlyEarning = false;
            if (chkMonthlyEarning.Checked) monthlyEarning = true;

            bool exhaustALlLeaves = false;
            if (chkExhaustAllLeaves.Checked) exhaustALlLeaves = true;

            string status = "Active";
            if (rbInactive.Checked) status = "Inactive";


            int result = bll.UpdateLeave(Leaveid, Leavename, leavetype, cashable, monthlyEarning, exhaustALlLeaves, status);
            if (result > 0)
            {
                Response.Redirect("leaveList.aspx");
            }
            else
            {
                ShowAlert("Leave Update Failed!", "error");
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("leaveList.aspx");
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