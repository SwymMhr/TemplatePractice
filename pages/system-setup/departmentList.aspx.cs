using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup
{
    public partial class departmentList : System.Web.UI.Page
    {
        BLLDepartment bld = new BLLDepartment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvDepartment.DataSource = bld.GetAllDepartment();
            gvDepartment.DataBind();

            if (gvDepartment.Rows.Count > 0)
            {
                gvDepartment.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvDepartment.UseAccessibleHeader = true;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("addDepartment.aspx");
        }

        protected void gvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editDepartment")
            {
                int DepartmentID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("editDepartment.aspx?DepartmentID=" + DepartmentID, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}