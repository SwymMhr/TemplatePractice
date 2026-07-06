using System;
using System.Data;
using System.Web.UI;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.dashboard
{
    public partial class dashboard : System.Web.UI.Page
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
            DataTable allEmployees = ble.GetAllEmployee();

            rptAbsentEmployees.DataSource = allEmployees;
            rptAbsentEmployees.DataBind();
            litAbsentCount.Text = allEmployees.Rows.Count.ToString();

            rptPresentEmployees.DataSource = null;
            rptPresentEmployees.DataBind();
            litPresentCount.Text = "0";

            rptLeaveEmployees.DataSource = null;
            rptLeaveEmployees.DataBind();
            litLeaveCount.Text = "0";

            rptLeaveList.DataSource = null;
            rptLeaveList.DataBind();
        }

        public string GetEmployeeImageUrl(object imageData)
        {
            if (imageData != null && imageData != DBNull.Value)
            {
                byte[] bytes = (byte[])imageData;
                return "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
            }

            return ResolveUrl("~/assets/images/users/avatar-1.jpg");
        }
    }
}