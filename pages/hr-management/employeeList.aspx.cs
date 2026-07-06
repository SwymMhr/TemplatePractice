using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.hr_management
{
    public partial class employeeList : System.Web.UI.Page
    {
        BLLEmployee ble = new BLLEmployee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvEmployees.DataSource = ble.GetEmployeeListView();
            gvEmployees.DataBind();

            if (gvEmployees.Rows.Count > 0)
            {
                gvEmployees.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvEmployees.UseAccessibleHeader = true;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("addEmployee.aspx");
        }

        protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewEmployee")
            {
                int employeeID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("employeeDetails.aspx?EmployeeID=" + employeeID, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}