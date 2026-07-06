using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup
{
    public partial class branchList : System.Web.UI.Page
    {
        BLLBranch blb = new BLLBranch();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvBranch.DataSource = blb.GetAllBranch();
            gvBranch.DataBind();

            if (gvBranch.Rows.Count > 0)
            {
                gvBranch.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvBranch.UseAccessibleHeader = true;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("addBranch.aspx");
        }

        protected void gvBranch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editBranch")
            {
                int branchID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("editBranch.aspx?BranchID=" + branchID, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}