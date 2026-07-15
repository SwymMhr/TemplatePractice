using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;
using TemplatingPractice.Utils;

namespace TemplatingPractice.pages.report.employee_info
{
    public partial class employeeDetailInfo : System.Web.UI.Page
    {
        BLLBranch blBranch = new BLLBranch();
        BLLDepartment blDepartment = new BLLDepartment();
        BLLEmployee ble = new BLLEmployee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBranches();
                RegisterEmployeeData();
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

        private void BindDepartmentsForBranch()
        {
            ddlDepartment.DataSource = blDepartment.GetAllDepartment();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));
        }

        private void RegisterEmployeeData()
        {
            DataTable dt = ble.GetAllEmployee();

            var list = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new
                {
                    id = Convert.ToInt32(row["EmployeeID"]),
                    name = row["EmployeeName"].ToString(),
                    branchId = Convert.ToInt32(row["BranchID"]),
                    departmentId = Convert.ToInt32(row["DepartmentID"])
                });
            }

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string json = serializer.Serialize(list);

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "employeeData", "var allEmployees = " + json + ";", true);
        }

        private void ClearEmployeeSelection()
        {
            hfEmployeeId.Value = "";
            txtEmployeeSearch.Text = "";
            txtEmpId.Text = "";
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDepartment.Items.Clear();
            ClearEmployeeSelection();

            if (string.IsNullOrWhiteSpace(ddlBranch.SelectedValue))
            {
                return;
            }

            BindDepartmentsForBranch();
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearEmployeeSelection();
        }

        protected void txtEmpId_TextChanged(object sender, EventArgs e)
        {
            int employeeId;
            if (!int.TryParse(txtEmpId.Text.Trim(), out employeeId))
            {
                ClearEmployeeSelection();
                return;
            }

            DataTable empTable = ble.GetEmployeeById(employeeId);
            if (empTable.Rows.Count == 0)
            {
                ClearEmployeeSelection();
                ShowAlert("Employee ID not found.", "error");
                return;
            }

            DataRow empRow = empTable.Rows[0];
            int branchId = Convert.ToInt32(empRow["BranchID"]);
            int departmentId = Convert.ToInt32(empRow["DepartmentID"]);

            BindBranches();
            if (ddlBranch.Items.FindByValue(branchId.ToString()) == null)
            {
                ShowAlert("Employee's branch could not be matched.", "error");
                return;
            }
            ddlBranch.SelectedValue = branchId.ToString();

            BindDepartmentsForBranch();
            if (ddlDepartment.Items.FindByValue(departmentId.ToString()) == null)
            {
                ShowAlert("Employee's department could not be matched.", "error");
                return;
            }
            ddlDepartment.SelectedValue = departmentId.ToString();

            hfEmployeeId.Value = employeeId.ToString();
            txtEmployeeSearch.Text = empRow["EmployeeName"].ToString() + " (" + employeeId + ")";
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlBranch.SelectedValue))
            {
                ShowAlert("Please select a Branch.", "error");
                return;
            }

            string url = "~/viewEmployeeDetailInfo"
                + "?UserID=" + txtEmpId.Text;

            Response.Redirect(url);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/employeeDetailInfo");
        }

        private void ShowAlert(string message, string type)
        {
            string script = $@"swal('{message}', '{type}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
        }
    }
}