using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;
using TemplatingPractice.Utils;

namespace TemplatingPractice.pages.report.attendance_report
{
    public partial class dailyAttendance : System.Web.UI.Page
    {
        BLLBranch blBranch = new BLLBranch();
        BLLDepartment blDepartment = new BLLDepartment();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBranches();

                DateTime today = DateTime.Today;
                txtDateEnglish.Text = today.ToString("yyyy-MM-dd");
                txtDateNepali.Text = NepaliDateConverter.ADToBSString(today);
            }
        }

        private void BindBranches()
        {
            ddlBranch.DataSource = blBranch.GetAllBranch();
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("Select Branch", ""));
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDepartment.Items.Clear();

            ddlDepartment.DataSource = blDepartment.GetAllDepartment();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("All Departments", ""));
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlBranch.SelectedValue))
            {
                ShowAlert("Please select a Branch.", "error");
                return;
            }

            if (!DateTime.TryParse(txtDateEnglish.Text.Trim(), out DateTime startDate))
            {
                ShowAlert("Please provide date.", "error");
                return;
            }

            string url = "~/dailyAttendanceList"
                + "?BranchID=" + ddlBranch.SelectedValue
                + "&DepartmentID=" + ddlDepartment.SelectedValue
                + "&StartDate=" + startDate.ToString("yyyy-MM-dd");

            Response.Redirect(url);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dailyAttendance");
        }

        private void ShowAlert(string message, string type)
        {
            string script = $@"swal('{message}', '{type}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
        }
    }
}