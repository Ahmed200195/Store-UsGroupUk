using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class ViewProduct : Page
    {
        ClsBasic clsBasic;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlDept.DataBind();
            }
            gvProduct.DataBind();
            dvNotData.Visible = gvProduct.Rows.Count == 0;
            gvProduct.Visible = gvProduct.Rows.Count != 0;
            cntProduct.InnerText = gvProduct.Rows.Count.ToString();
        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "EditDept")
            {
                Response.Redirect("MgProduct.aspx?id=" + id);
            }
        }
    }
}