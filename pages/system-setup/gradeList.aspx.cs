using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup
{
    public partial class gradeList : System.Web.UI.Page
    {
        BLLGrade blg = new BLLGrade();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvGrade.DataSource = blg.GetAllGrade();
            gvGrade.DataBind();

            if (gvGrade.Rows.Count > 0)
            {
                gvGrade.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvGrade.UseAccessibleHeader = true;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("addGrade.aspx");
        }

        protected void gvGrade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editGrade")
            {
                int GradeID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("editGrade.aspx?GradeID=" + GradeID, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}