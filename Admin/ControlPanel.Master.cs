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
            dataTable = clsBasic.SelectData("*", "Login");
            if (!dataTable.AsEnumerable().Any(a => a.Field<string>("Email").Equals(Session["Email"]) && a.Field<string>("Password").Equals(Session["Password"])))
            {
                Session["Email"] = null;
                Session["Password"] = null;
                Response.Redirect("~/User Check/Login.aspx");
            }
        }
    }
}