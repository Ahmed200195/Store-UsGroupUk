using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class ViewProduct : Page
    {
        ClsBasic clsBasic;
        DataTable dataTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "UsGroupUk | Show Prodcuts";
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
            
            if (e.CommandName == "EditProduct")
            {
                string id = e.CommandArgument.ToString();
                Response.Redirect("MgProduct.aspx?id=" + id);
            }
            else if(e.CommandName == "DelProduct")
            {
                string key, id, name;
                key= e.CommandArgument.ToString();
                id = key.Substring(0, key.IndexOf('|'));
                name = key.Substring(key.IndexOf('|') + 1);
                File.Delete(Path.Combine(Server.MapPath("~/Uploads/Product"), name));
                clsBasic = new ClsBasic();
                dataTable = clsBasic.SelectData("*", "ProductPhotos WHERE PId = "+ id);
                foreach (DataRow row in dataTable.Rows)
                {
                    File.Delete(Path.Combine(Server.MapPath("~/Uploads/Product"), row["Name"].ToString()));
                }
                sqlProduct.DeleteParameters["Id"].DefaultValue = id;
                sqlProduct.Delete();
                gvProduct.DataBind();
                dvNotData.Visible = gvProduct.Rows.Count == 0;
                gvProduct.Visible = gvProduct.Rows.Count != 0;
                cntProduct.InnerText = gvProduct.Rows.Count.ToString();
            }
        }
    }
}