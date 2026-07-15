using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup.roster
{
    public partial class addWorkHour : System.Web.UI.Page
    {
        BLLWorkHour blw = new BLLWorkHour();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string shiftName = txtGroupName.Text.Trim();
                string startTime = txtInTime.Text.Trim();
                string lateInBy = txtInTimeLate.Text.Trim();
                string endTime = txtOutTime.Text.Trim();
                string lateOutBy = txtOutTimeLate.Text.Trim();
                int lunchTime = int.Parse(txtLunchTime.Text.Trim());

                // Recompute TotalHour server-side instead of trusting txtHour/txtMinute
                if (!DateTime.TryParse(startTime, out DateTime start) ||
                    !DateTime.TryParse(endTime, out DateTime end))
                {
                    ShowAlert("Invalid time format.", "error");
                    return;
                }

                double diffMinutes = (end - start).TotalMinutes - lunchTime;
                if (diffMinutes < 0) diffMinutes += 24 * 60; // overnight/night shift

                int hours = (int)(diffMinutes / 60);
                int minutes = (int)(diffMinutes % 60);
                string totalHour = hours + ":" + minutes;

                string shift = rbNightShiftYes.Checked ? "Night" : "Day";
                bool defaultForAllWeekend = rbWeekendYes.Checked;
                string status = rbStatusYes.Checked ? "Active" : "Inactive";

                int i = blw.CreateWorkHour(shiftName, startTime, lateInBy, totalHour, lunchTime, endTime, lateOutBy, shift, defaultForAllWeekend, status);

                if (i > 0)
                {
                    ShowAlert("Work Hour added successfully!", "success");
                    Response.Redirect("~/workHourList", false);
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
            Response.Redirect("~/workHourList");
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