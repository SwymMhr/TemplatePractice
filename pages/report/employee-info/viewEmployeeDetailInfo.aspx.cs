using System;
using System.Data;
using System.Web.UI;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.report.employee_info
{
    public partial class viewEmployeeDetailInfo : System.Web.UI.Page
    {
        BLLEmployee ble = new BLLEmployee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDetail();
            }
        }

        private void LoadDetail()
        {
            int employeeId;
            if (!int.TryParse(Request.QueryString["UserID"], out employeeId))
            {
                Response.Redirect("~/employeeDetailInfo");
                return;
            }

            DataTable dt = ble.GetEmployeeFullDetailById(employeeId);
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("~/employeeDetailInfo");
                return;
            }

            DataRow row = dt.Rows[0];

            lblEmployeeId.Text = row["EmployeeID"].ToString();
            lblFullName.Text = row["EmployeeName"].ToString();
            lblGender.Text = row["Gender"] == DBNull.Value ? "" : row["Gender"].ToString();
            lblDOB.Text = row["DOBEnglish"] == DBNull.Value ? "" : Convert.ToDateTime(row["DOBEnglish"]).ToString("yyyy-MM-dd");
            lblJoinDate.Text = row["JoinDateEnglish"] == DBNull.Value ? "" : Convert.ToDateTime(row["JoinDateEnglish"]).ToString("yyyy-MM-dd");
            lblEmail.Text = row["Email"] == DBNull.Value ? "" : row["Email"].ToString();

            lblBranch.Text = row["BranchName"] == DBNull.Value ? "" : row["BranchName"].ToString();
            lblDepartment.Text = row["DepartmentName"] == DBNull.Value ? "" : row["DepartmentName"].ToString();
            lblLoginId.Text = row["LoginID"] == DBNull.Value ? "" : row["LoginID"].ToString();
            lblDesignation.Text = row["DesignationName"] == DBNull.Value ? "" : row["DesignationName"].ToString();
            lblType.Text = row["EmployeeType"] == DBNull.Value ? "" : row["EmployeeType"].ToString();
            lblStatus.Text = row["Status"] == DBNull.Value ? "" : row["Status"].ToString();
            lblUserType.Text = row["UserType"] == DBNull.Value ? "" : row["UserType"].ToString();
            lblGrade.Text = row["GradeName"] == DBNull.Value ? "" : row["GradeName"].ToString();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/employeeDetailInfo");
        }
    }
}