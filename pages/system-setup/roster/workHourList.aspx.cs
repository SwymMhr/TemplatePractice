using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup.roster
{
    public partial class workHourList : System.Web.UI.Page
    {
        BLLWorkHour blw = new BLLWorkHour();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gvWorkHour.DataSource = blw.GetAllWorkHour();
            gvWorkHour.DataBind();

            if (gvWorkHour.Rows.Count > 0)
            {
                gvWorkHour.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvWorkHour.UseAccessibleHeader = true;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("addWorkHour.aspx");
        }

        protected void gvWorkHour_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            DataRowView rowView = e.Row.DataItem as DataRowView;
            if (rowView == null) return;

            string status = rowView["Status"].ToString();

            LinkButton lnkStatus = (LinkButton)e.Row.FindControl("lnkStatus");
            if (lnkStatus != null)
            {
                if (status == "Active")
                {
                    lnkStatus.Text = "<i class='mdi mdi-check'></i> Active";
                    lnkStatus.CssClass = "btn btn-success w-xs waves-effect waves-light btn-xs";
                }
                else
                {
                    lnkStatus.Text = "<i class='mdi mdi-close'></i> Inactive";
                    lnkStatus.CssClass = "btn btn-secondary w-xs waves-effect waves-light btn-xs";
                }
            }
        }

        protected void gvWorkHour_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null) return;

            int workHourId;
            if (!int.TryParse(e.CommandArgument.ToString(), out workHourId)) return;

            if (e.CommandName == "changeStatus")
            {
                DataRow row = blw.GetWorkHourById(workHourId);
                if (row != null)
                {
                    string currentStatus = row["Status"].ToString();
                    string newStatus = currentStatus == "Active" ? "Inactive" : "Active";
                    blw.UpdateStatus(workHourId, newStatus);
                }
                BindGrid();
            }
            else if (e.CommandName == "deleteShift")
            {
                blw.DeleteWorkHour(workHourId);
                BindGrid();
            }
        }
    }
}