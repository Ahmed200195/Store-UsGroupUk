using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class MgLinkProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvLinkProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DelLing")
            {
                sqlLinkProduct.DeleteParameters["Id"].DefaultValue = e.CommandArgument.ToString();
                sqlLinkProduct.Delete();
            }
        }
    }
}