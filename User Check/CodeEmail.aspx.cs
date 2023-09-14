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
            clsBasic = new ClsBasic();
            dataTable = clsBasic.SelectData("*", "Login");
            if (!dataTable.AsEnumerable().Any(a => a.Field<string>("Email").Equals(Session["Email"]) && a.Field<string>("Password").Equals(Session["Password"])))
            {
                Session["Email"] = null;
                Session["Password"] = null;
                Response.Redirect("~/User Check/Login.aspx");
            }
        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            if (txtCode.Value == Session["Code"].ToString())
            {
                Response.Redirect("~/Admin/Home.aspx");
            }
            else
            {
                dvAlert.Visible = true;
            }
        }
    }
}