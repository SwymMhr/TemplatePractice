using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.customer_support
{
    public partial class inactiveList : System.Web.UI.Page
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
            gvInactiveEmployees.DataSource = ble.GetEmployeeByStatus("Inactive");
            gvInactiveEmployees.DataBind();
        }

        protected void gvInactiveEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int employeeId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("~/viewEmployee?EmployeeID=" + employeeId, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}