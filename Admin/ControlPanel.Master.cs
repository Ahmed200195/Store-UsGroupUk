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
    public partial class ControlPanel : System.Web.UI.MasterPage
    {
        DataTable dataTable;
        ClsBasic clsBasic;
        protected void Page_Load(object sender, EventArgs e)
        {
            clsBasic = new ClsBasic();
            //dataTable = clsBasic.SelectData("*", $"Login WHERE Email = '{Session["Email"]}' AND Password = '{Session["Password"]}'");
            //if (dataTable.Rows.Count == 0)
            //{
            //    Session["Id"] = null;
            //    Session["Email"] = null;
            //    Session["Password"] = null;
            //    Response.Redirect("~/User Check/Login.aspx");
            //}
            if (!IsPostBack)
            {
                ddlDataByType.DataSource = clsBasic.SelectData("*", "Brand");
                ddlDataByType.DataValueField = "Id";
                ddlDataByType.DataTextField = "Name";
                ddlDataByType.DataBind();
            }
        }

        protected void btnLogOut_ServerClick(object sender, EventArgs e)
        {
            Session["Email"] = null;
            Session["Password"] = null;
            Response.Redirect("~/User Check/Login.aspx");
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDataByType.DataSource = clsBasic.SelectData("*", ddlType.SelectedItem.Text);
            ddlDataByType.DataValueField = "Id";
            ddlDataByType.DataTextField = "Name";
            ddlDataByType.DataBind();
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            if (txtDataByType.Value != "")
            {
                SqlDataByType.InsertCommand = $"INSERT INTO {ddlType.SelectedItem.Text}(Name) VALUES('{txtDataByType.Value}')";
                SqlDataByType.Insert();
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "",
                @"hideAlertDiv('alertMsgSuccess');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "",
                @"hideAlertDiv('alertMsgEmty');", true);
            }
            txtDataByType.Value = "";
            ddlType_SelectedIndexChanged(null, null);
        }

        protected void btnRemove_ServerClick(object sender, EventArgs e)
        {
            SqlDataByType.DeleteCommand = $"DELETE FROM {ddlType.SelectedItem.Text} WHERE Id = {ddlDataByType.SelectedValue}";
            try
            {
                SqlDataByType.Delete();
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "",
                @"hideAlertDiv('alertMsgSuccess');", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "",
                @"hideAlertDiv('alertMsgError');", true);
            }
            txtDataByType.Value = "";
            ddlType_SelectedIndexChanged(null, null);
        }
    }
}