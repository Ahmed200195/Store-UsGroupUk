using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class PeOrder : System.Web.UI.Page
    {
        DataRow dataRow;
        ClsBasic clsBasic;
        protected void Page_Load(object sender, EventArgs e)
        {
            clsBasic = new ClsBasic();
            dataRow = clsBasic.SelectData("Name, Phone, Address, SUM(total) as Total, Received", $"Client INNER JOIN Orders ON Client.Id = Orders.CId WHERE Client.Id = {Request.QueryString["id"]} GROUP BY Name, Phone, Address, Received").Rows[0];
            txtName.Value = dataRow["Name"].ToString();
            txtPhone.Value = dataRow["Phone"].ToString();
            txtAddress.Value = dataRow["Address"].ToString();
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
    }
}