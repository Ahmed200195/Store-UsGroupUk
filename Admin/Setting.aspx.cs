using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class Setting : Page
    {
        ClsBasic clsBasic;
        DataRow dataRow;
        protected void Page_Load(object sender, EventArgs e)
        {
            clsBasic = new ClsBasic();
            if (!IsPostBack)
            {
                dataRow = clsBasic.SelectData("*", "Login WHERE Id = 1").Rows[0];
                txtName.Value = dataRow["Name"].ToString();
                txtEmail.Value = dataRow["Email"].ToString();
                txtPswd.Value = dataRow["Password"].ToString();
            }
        }

        protected void btnEdit_ServerClick(object sender, EventArgs e)
        {
            if (txtName.Value == "" || txtEmail.Value == "" || txtPswd.Value == "")
            {
                return;
            }
            else
            {
                clsBasic.EditUser(txtName.Value, txtEmail.Value, txtPswd.Value);
                Response.Redirect("Setting.aspx");
            }
        }
    }
}