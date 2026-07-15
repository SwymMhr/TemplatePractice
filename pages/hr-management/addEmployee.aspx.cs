using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.hr_management
{
    public partial class addEmployee : System.Web.UI.Page
    {
        BLLEmployee ble = new BLLEmployee();
        BLLBranch blBranch = new BLLBranch();
        BLLDepartment blDepartment = new BLLDepartment();
        BLLGrade blGrade = new BLLGrade();
        BLLDesignation blDesignation = new BLLDesignation();

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

            // Department
            ddlDepartment.DataSource = blDepartment.GetAllDepartment();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));

            // Designation
            ddlDesignation.DataSource = blDesignation.GetAllDesignation();
            ddlDesignation.DataTextField = "DesignationName";
            ddlDesignation.DataValueField = "DesignationID";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("Select Designation", ""));

            // Grade
            ddlGrade.DataSource = blGrade.GetAllGrade();
            ddlGrade.DataTextField = "GradeName";
            ddlGrade.DataValueField = "GradeID";
            ddlGrade.DataBind();
            ddlGrade.Items.Insert(0, new ListItem("Select Grade", ""));

            // Supervisor - pull from existing employees
            ddlSupervisor.DataSource = ble.GetAllEmployee();
            ddlSupervisor.DataTextField = "EmployeeName";
            ddlSupervisor.DataValueField = "EmployeeID";
            ddlSupervisor.DataBind();
            ddlSupervisor.Items.Insert(0, new ListItem("NO HOD", ""));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string employeeCode = txtEmployeeId.Text.Trim();

                if (string.IsNullOrWhiteSpace(employeeCode))
                {
                    ShowAlert("Employee ID is required.", "error");
                    return;
                }

                if (ble.IsEmployeeCodeExists(employeeCode))
                {
                    ShowAlert("This Employee ID is already taken. Please choose a different one.", "error");
                    return;
                }

                string fullName = $"{ddlSalutation.SelectedValue} {txtFirstName.Text.Trim()} {txtMiddleName.Text.Trim()} {txtLastName.Text.Trim()}".Replace("  ", " ").Trim();

                string gender = null;
                if (rbGenderFemale.Checked) gender = "Female";
                else if (rbGenderMale.Checked) gender = "Male";
                else if (rbGenderOther.Checked) gender = "Others";
                if (gender == null)
                {
                    ShowAlert("Please select a gender.", "error");
                    return;
                }

                string relationship = "Single";
                if (rbRelMarried.Checked) relationship = "Married";
                else if (rbRelSeparated.Checked) relationship = "Separated";
                else if (rbRelDivorced.Checked) relationship = "Divorced";

                DateTime? dobEnglish = null;
                DateTime parsedDob;
                if (DateTime.TryParseExact(txtDobEnglish.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDob))
                {
                    dobEnglish = parsedDob;
                }
                string dobNepali = string.IsNullOrWhiteSpace(txtDobNepali.Text) ? null : txtDobNepali.Text.Trim();

                DateTime joinDateEnglish;
                if (!DateTime.TryParseExact(txtJoinDateEnglish.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out joinDateEnglish))
                {
                    ShowAlert("Join date is required and must be valid.", "error");
                    return;
                }
                string joinDateNepali = string.IsNullOrWhiteSpace(txtJoinDateNepali.Text) ? null : txtJoinDateNepali.Text.Trim();

                if (string.IsNullOrWhiteSpace(ddlDesignation.SelectedValue) ||
                    string.IsNullOrWhiteSpace(ddlGrade.SelectedValue) ||
                    string.IsNullOrWhiteSpace(ddlDepartment.SelectedValue) ||
                    string.IsNullOrWhiteSpace(ddlBranch.SelectedValue))
                {
                    ShowAlert("Branch, Department, Designation, and Grade are required.", "error");
                    return;
                }

                int designationId = int.Parse(ddlDesignation.SelectedValue);
                int gradeId = int.Parse(ddlGrade.SelectedValue);
                int departmentId = int.Parse(ddlDepartment.SelectedValue);
                int branchId = int.Parse(ddlBranch.SelectedValue);

                int? supervisorId = string.IsNullOrWhiteSpace(ddlSupervisor.SelectedValue)
                    ? (int?)null
                    : int.Parse(ddlSupervisor.SelectedValue);

                string email = !string.IsNullOrWhiteSpace(txtOfficialEmail.Text) ? txtOfficialEmail.Text.Trim() : txtPersonalEmail.Text.Trim();

                string employeeType = ddlType.SelectedValue;
                string userType = ddlUserType.SelectedValue;
                string status = ddlStatus.SelectedValue;

                string loginId = txtLoginId.Text.Trim();
                if (string.IsNullOrWhiteSpace(loginId))
                {
                    ShowAlert("Login ID is required.", "error");
                    return;
                }

                if (ble.IsLoginIdExists(loginId))
                {
                    ShowAlert("This Login ID is already taken. Please choose a different one.", "error");
                    return;
                }

                string password = txtPassword.Text;

                byte[] imageBytes = null;
                if (fuPhoto.HasFile)
                {
                    imageBytes = fuPhoto.FileBytes;
                }

                int i = ble.CreateEmployee(employeeCode, fullName, gender, relationship, dobEnglish, dobNepali, joinDateEnglish, joinDateNepali, designationId, gradeId, departmentId, branchId, email, employeeType, userType, status, supervisorId, loginId, password, imageBytes);

                if (i > 0)
                {
                    ShowAlert("Employee added successfully!", "success");
                    Response.Redirect("~/employeeList", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    ShowAlert("Addition failed!", "error");
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Error: " + ex.Message, "error");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/employeeList", false);
            Context.ApplicationInstance.CompleteRequest();
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