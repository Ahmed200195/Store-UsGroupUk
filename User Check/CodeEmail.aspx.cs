using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.User_Check
{
    public partial class CodeEmail : System.Web.UI.Page
    {
        ClsBasic clsBasic;
        DataTable dataTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "UsGroupUk | Login";
            clsBasic = new ClsBasic();
            dataTable = clsBasic.SelectData("*", $"Login WHERE Email = '{Session["Email"]}' AND Password = '{Session["Password"]}'");
            if (dataTable.Rows.Count == 0)
            {
                Session["Id"] = null;
                Session["Email"] = null;
                Session["Password"] = null;
                Response.Redirect("~/User Check/Login.aspx");
            }
        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            if (txtCode.Value == Session["Code"].ToString())
            {
                sqlLogin.UpdateCommand = $"UPDATE Login SET CntSession = 0 WHERE Email = '{Session["Email"]}' AND Password = '{Session["Password"]}'";
                sqlLogin.Update();
                Response.Redirect("~/Admin/Home.aspx");
            }
            else
            {
                dvAlert.Visible = true;
            }
        }
    }
}