using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup
{
    public partial class leaveList : System.Web.UI.Page
    {
        BLLLeave bll = new BLLLeave();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvLeave.DataSource = bll.GetAllLeave();
            gvLeave.DataBind();

            if (gvLeave.Rows.Count > 0)
            {
                gvLeave.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvLeave.UseAccessibleHeader = true;
            }
        }

        protected string GetOthersText(object monthlyEarning, object exhaustAllLeaves)
        {
            bool isMonthlyEarning = Convert.ToBoolean(monthlyEarning);
            bool isExhaustAllLeaves = Convert.ToBoolean(exhaustAllLeaves);

            string strMonthlyEarning = "";
            string strExhaustAllLeaves = "";
            if (isMonthlyEarning) 
                strMonthlyEarning = "Monthly Earning ";
            if (isExhaustAllLeaves) 
                strExhaustAllLeaves = "Exhaust All Leaves";

            string strOthers = strMonthlyEarning + ((isMonthlyEarning && isExhaustAllLeaves) ? " , " : "") + strExhaustAllLeaves;
            return strOthers;
        }

        protected string GetCashableText(object Cashable)
        {
            bool isCashable = Convert.ToBoolean(Cashable);            
            string strCashable = "No";
            if (isCashable)
                strCashable = "Yes";
            return strCashable;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/addLeave");
        }

        protected void gvLeave_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editLeave")
            {
                int LeaveID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("~/editLeave?LeaveID=" + LeaveID, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}