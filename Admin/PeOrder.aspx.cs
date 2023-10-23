using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class PeOrder : Page
    {
        DataRow dataRow;
        ClsBasic clsBasic;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "UsGroupUk | PeOrder";
            clsBasic = new ClsBasic();
            dataRow = clsBasic.SelectData("Name, Phone, Address, SUM(total) as Total, Received, Piece, Home, Street", $"Client INNER JOIN Orders ON Client.Id = Orders.CId WHERE Client.Id = {Request.QueryString["id"]} GROUP BY Name, Phone, Address, Received, Piece, Home, Street").Rows[0];
            txtName.Value = dataRow["Name"].ToString();
            txtPhone.Value = dataRow["Phone"].ToString();
            txtAddress.Value = dataRow["Address"].ToString();
            txtPiece.Value = dataRow["Piece"].ToString();
            txtHome.Value = dataRow["Home"].ToString();
            txtStreet.Value = dataRow["Street"].ToString();
            totalOrder.InnerText = dataRow["Total"].ToString();
            if (dataRow["Received"].ToString() == "True")
            {
                btnReceived.InnerText = "تم استلام بنجاح";
                btnReceived.Disabled = true;
            }
            else
            {
                btnReceived.InnerText = "استلام";
            }
            gvProduct.DataBind();
            CntOrder.InnerText = gvProduct.Rows.Count.ToString();
        }

        protected void btnReceived_ServerClick(object sender, EventArgs e)
        {
            sqlProduct.Update();
            Response.Redirect("Orders.aspx");
        }

        protected void btnDeleteOrder_ServerClick(object sender, EventArgs e)
        {
            sqlProduct.Delete();
            Response.Redirect("Orders.aspx");
        }
    }
}