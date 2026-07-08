using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.report.employee_info
{
    public partial class employeeReport : System.Web.UI.Page
    {
        BLLBranch blBranch = new BLLBranch();
        BLLDepartment blDepartment = new BLLDepartment();
        BLLEmployee ble = new BLLEmployee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropdowns();
            }
        }

        private void BindDropdowns()
        {
            // Branch
            ddlBranch.DataSource = blBranch.GetAllBranch();
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("Select Branch", ""));

            // Department - full list; enabled only once a Branch is chosen
            ddlDepartment.DataSource = blDepartment.GetAllDepartment();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool branchSelected = !string.IsNullOrEmpty(ddlBranch.SelectedValue);

            ddlDepartment.Enabled = branchSelected && !chkAllDept.Checked;

            if (!branchSelected)
            {
                ddlDepartment.SelectedIndex = 0;
                chkAllDept.Checked = false;
            }
        }

        protected void chkAllDept_CheckedChanged(object sender, EventArgs e)
        {
            bool branchSelected = !string.IsNullOrEmpty(ddlBranch.SelectedValue);

            if (chkAllDept.Checked)
            {
                ddlDepartment.SelectedIndex = 0;
                ddlDepartment.Enabled = false;
            }
            else
            {
                ddlDepartment.Enabled = branchSelected;
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Picking a specific Department manually overrides "All"
            if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue) && chkAllDept.Checked)
            {
                chkAllDept.Checked = false;
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlBranch.SelectedValue))
            {
                ShowAlert("Please select a Branch.", "error");
                return;
            }

            if (!chkAllDept.Checked && string.IsNullOrEmpty(ddlDepartment.SelectedValue))
            {
                ShowAlert("Please select a Department, or check All.", "error");
                return;
            }

            if (string.IsNullOrEmpty(ddlStatus.SelectedValue))
            {
                ShowAlert("Please select a Status.", "error");
                return;
            }

            if (string.IsNullOrEmpty(ddlType.SelectedValue))
            {
                ShowAlert("Please select a Type.", "error");
                return;
            }

            int branchId = Convert.ToInt32(ddlBranch.SelectedValue);
            int? departmentId = chkAllDept.Checked ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            string status = ddlStatus.SelectedValue;
            string employeeType = ddlType.SelectedValue;
            string sortBy = ddlSort.SelectedValue;

            gvReport.DataSource = ble.GetEmployeeReport(branchId, departmentId, status, employeeType, sortBy);
            gvReport.DataBind();

            if (gvReport.Rows.Count > 0)
            {
                gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvReport.UseAccessibleHeader = true;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("employeeReport.aspx");
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
        }
    }
}