using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class LinkProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["Ids"] = "0";
            }
            FillData();
        }

        protected void btnAddToLink_ServerClick(object sender, EventArgs e)
        {
            ViewState["Ids"] += "," + ddlProduct.SelectedValue;
            FillData();
        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveProduct")
            {
                ViewState["Ids"] = ViewState["Ids"].ToString().Replace("," + e.CommandArgument, "");
                FillData();
            }
        }

        void FillData()
        {
            sqlLinkProduct.SelectCommand = $"SELECT [Product].[Id], [NameAr], [Price], [Discount], ISNULL([Photo],'x') as [Photo] FROM [Product] WHERE [Id] IN ({ViewState["Ids"]}) ORDER BY [Id] DESC";
            gvProduct.DataBind();
        }

        protected void btnAddLinkData_ServerClick(object sender, EventArgs e)
        {

        }
    }
}