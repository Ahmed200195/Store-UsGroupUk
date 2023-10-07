using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class LinkProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "UsGroupUk | LinkProduct";
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
            sqlInfoLink.InsertParameters["Name"].DefaultValue = txtName.Value;
            sqlInfoLink.Insert();
        }

        protected void sqlInfoLink_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            DataSourceSelectArguments args = new DataSourceSelectArguments();
            DataView view = (DataView)sqlLinkProduct.Select(args);
            DataTable dataTable = view.ToTable();
            foreach (DataRow row in dataTable.Rows)
            {
                sqlLinkProduct.InsertParameters["Id"].DefaultValue = e.Command.Parameters["@Identity"].Value.ToString();
                sqlLinkProduct.InsertParameters["PId"].DefaultValue = row["Id"].ToString();
                sqlLinkProduct.Insert();
            }
            Response.Redirect("MgLinkProduct.aspx");
        }
    }
}