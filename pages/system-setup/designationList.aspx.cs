using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup
{
    public partial class designationList : System.Web.UI.Page
    {
        BLLDesignation bld = new BLLDesignation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvDesignation.DataSource = bld.GetAllDesignation();
            gvDesignation.DataBind();

            if (gvDesignation.Rows.Count > 0)
            {
                gvDesignation.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvDesignation.UseAccessibleHeader = true;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("addDesignation.aspx");
        }

        protected void gvDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editDesignation")
            {
                int DesignationID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("editDesignation.aspx?DesignationID=" + DesignationID, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}