using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.system_setup
{
    public partial class editDesignation : System.Web.UI.Page
    {
        BLLDesignation bld = new BLLDesignation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindValues();
            }
        }

        protected void BindValues()
        {
            int Designationid = Convert.ToInt32(Request.QueryString["Designationid"]);
            DataTable dt = bld.GetDesignationById(Designationid);

            txtDesignationName.Text = dt.Rows[0]["DesignationName"].ToString();

            string status = dt.Rows[0]["Status"].ToString();
            if (status == "Inactive")
            {
                rbInactive.Checked = true;
            }
            else
            {
                rbActive.Checked = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int Designationid = Convert.ToInt32(Request.QueryString["Designationid"]);
            string Designationname = txtDesignationName.Text;

            string status = "Active";
            if (rbInactive.Checked) status = "Inactive";

            int result = bld.UpdateDesignation(Designationid, Designationname, status);
            if (result > 0)
            {
                Response.Redirect("designationList.aspx");
            }
            else
            {
                ShowAlert("Designation Update Failed!", "error");
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("designationList.aspx");
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