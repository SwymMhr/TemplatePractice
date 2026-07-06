using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TemplatingPractice
{
    public partial class page_confirm_mail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["email"] != null)
            {
                string email = Convert.ToString(Request.QueryString["email"]);
                lblEmail.Text = Server.HtmlEncode(Convert.ToString(Request.QueryString["email"]));
            }
        }
    }
}