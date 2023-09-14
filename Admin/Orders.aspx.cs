using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvClient.DataBind();
                cntClient.InnerText = gvClient.Rows.Count.ToString();
            }
        }

        protected void gvClient_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("InfoClint"))
            {
                Response.Redirect("PeOrder.aspx?id=" + e.CommandArgument);
            }
        }
    }
}