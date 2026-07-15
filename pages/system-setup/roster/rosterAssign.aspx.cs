using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup.roster
{
    public partial class rosterAssign : System.Web.UI.Page
    {
        BLLBranch blb = new BLLBranch();
        BLLDepartment bld = new BLLDepartment();
        BLLEmployee ble = new BLLEmployee();
        BLLWorkHour blw = new BLLWorkHour();
        BLLEmployeeShift bles = new BLLEmployeeShift();

        private static readonly string[] WeekDays =
        {
            "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBranch();
                BindDepartment();
                BindShiftGroup(CmbDefaultSG);
            }
        }

        private void BindBranch()
        {
            CmbBranch.DataSource = blb.GetAllBranch();
            CmbBranch.DataTextField = "BranchName";
            CmbBranch.DataValueField = "BranchID";
            CmbBranch.DataBind();
            CmbBranch.Items.Insert(0, new ListItem("Select Branch", ""));
        }

        private void BindDepartment()
        {
            CmbDepartment.DataSource = bld.GetAllDepartment();
            CmbDepartment.DataTextField = "DepartmentName";
            CmbDepartment.DataValueField = "DepartmentID";
            CmbDepartment.DataBind();
            CmbDepartment.Items.Insert(0, new ListItem("Select Department", ""));
        }

        private void BindEmployees(int branchId, int departmentId)
        {
            gvEmployee.DataSource = ble.GetEmployeeByBranchAndDepartment(branchId, departmentId);
            gvEmployee.DataBind();
        }

        private void BindShiftGroup(DropDownList ddl)
        {
            ddl.DataSource = blw.GetActiveWorkHour();
            ddl.DataTextField = "ShiftName";
            ddl.DataValueField = "WorkHourID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select", ""));
        }

        private void TryBindEmployees()
        {
            if (int.TryParse(CmbBranch.SelectedValue, out int branchId) &&
                int.TryParse(CmbDepartment.SelectedValue, out int departmentId))
            {
                BindEmployees(branchId, departmentId);
            }
            else
            {
                gvEmployee.DataSource = null;
                gvEmployee.DataBind();
            }
        }

        protected void CmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            TryBindEmployees();
        }

        protected void CmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            TryBindEmployees();
        }

        protected void CmbDefaultSG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CmbDefaultSG.SelectedValue))
            {
                pnlWeekDay.Visible = false;
                return;
            }

            var days = WeekDays.Select(d => new { DayName = d }).ToList();
            gvWeekDay.DataSource = days;
            gvWeekDay.DataBind();

            pnlWeekDay.Visible = true;
        }

        protected void gvWeekDay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            DropDownList ddlDayShift = (DropDownList)e.Row.FindControl("ddlDayShift");
            if (ddlDayShift != null)
            {
                BindShiftGroup(ddlDayShift);
                ddlDayShift.SelectedValue = CmbDefaultSG.SelectedValue;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> employeeIds = new List<int>();
                foreach (GridViewRow row in gvEmployee.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkEmployee");
                    if (chk != null && chk.Checked)
                    {
                        int employeeId = Convert.ToInt32(gvEmployee.DataKeys[row.RowIndex].Value);
                        employeeIds.Add(employeeId);
                    }
                }

                if (employeeIds.Count == 0)
                {
                    ShowAlert("Please select at least one employee.", "error");
                    return;
                }

                if (!pnlWeekDay.Visible || gvWeekDay.Rows.Count == 0)
                {
                    ShowAlert("Please select a Shift Group.", "error");
                    return;
                }

                Dictionary<string, int> weekDayShift = new Dictionary<string, int>();
                foreach (GridViewRow row in gvWeekDay.Rows)
                {
                    HiddenField hdnDayName = (HiddenField)row.FindControl("hdnDayName");
                    DropDownList ddlDayShift = (DropDownList)row.FindControl("ddlDayShift");

                    if (string.IsNullOrEmpty(ddlDayShift.SelectedValue))
                    {
                        ShowAlert("Please assign a shift group for every weekday.", "error");
                        return;
                    }

                    weekDayShift[hdnDayName.Value] = int.Parse(ddlDayShift.SelectedValue);
                }

                int totalUpserted = 0;
                foreach (int employeeId in employeeIds)
                {
                    foreach (var kvp in weekDayShift)
                    {
                        totalUpserted += bles.UpsertEmployeeShift(employeeId, kvp.Key, kvp.Value);
                    }
                }

                if (totalUpserted > 0)
                {
                    ShowAlert("Assignment successful!", "success");
                }
                else
                {
                    ShowAlert("Assignment failed!", "error");
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Error: " + ex.Message, "error");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/rosterAssignList");
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