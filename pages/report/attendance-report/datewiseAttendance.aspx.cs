using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;
using TemplatingPractice.Utils;

namespace TemplatingPractice.pages.report.attendance_report
{
    public partial class datewiseAttendance : System.Web.UI.Page
    {
        BLLBranch blBranch = new BLLBranch();
        BLLDepartment blDepartment = new BLLDepartment();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBranches();

                DateTime today = DateTime.Today;
                txtStartDateEnglish.Text = today.ToString("yyyy-MM-dd");
                txtEndDateEnglish.Text = today.ToString("yyyy-MM-dd");
                txtStartDateNepali.Text = NepaliDateConverter.ADToBSString(today);
                txtEndDateNepali.Text = NepaliDateConverter.ADToBSString(today);
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

            if (string.IsNullOrWhiteSpace(ddlBranch.SelectedValue))
            {
                ddlDepartment.Enabled = false;
                return;
            }

            ddlDepartment.DataSource = blDepartment.GetAllDepartment();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("All Departments", ""));
            ddlDepartment.Enabled = true;
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlBranch.SelectedValue))
            {
                ShowAlert("Please select a Branch.", "error");
                return;
            }

            if (!DateTime.TryParse(txtStartDateEnglish.Text.Trim(), out DateTime startDate) ||
                !DateTime.TryParse(txtEndDateEnglish.Text.Trim(), out DateTime endDate))
            {
                ShowAlert("Please provide valid Start and End dates.", "error");
                return;
            }

            if (endDate < startDate)
            {
                ShowAlert("End Date must be on or after Start Date.", "error");
                return;
            }

            string url = "datewiseAttendanceList.aspx"
                + "?BranchID=" + ddlBranch.SelectedValue
                + "&DepartmentID=" + ddlDepartment.SelectedValue
                + "&StartDate=" + startDate.ToString("yyyy-MM-dd")
                + "&EndDate=" + endDate.ToString("yyyy-MM-dd");

            Response.Redirect(url);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("datewiseAttendance.aspx");
        }

        private void ShowAlert(string message, string type)
        {
            string script = $@"swal('{message}', '{type}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
        }
    }
}