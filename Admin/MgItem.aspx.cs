using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Store.Admin
{
    public partial class MgItem : Page
    {
        ClsBasic clsBasic;
        DataRow dataRow;
        protected void Page_Load(object sender, EventArgs e)
        {
            clsBasic = new ClsBasic();
            dvNoData.Visible = gvDept.Rows.Count == 0;
            gvDept.Visible = gvDept.Rows.Count != 0;
            cntDept.InnerText = gvDept.Rows.Count.ToString();
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != "0")
                {
                    btnAddDept.InnerText = "تعديل";
                    dataRow = clsBasic.SelectData("*", "Dept WHERE Id = " + Request.QueryString["id"]).Rows[0];
                    txtNameAr.Value = dataRow["NameAr"].ToString();
                    txtNameEn.Value = dataRow["NameEn"].ToString();
                    imgView.Attributes["src"] = "data:image;base64," + Convert.ToBase64String((byte[])dataRow["Photo"]);
                }
            }
        }

        protected void btnAddDept_ServerClick(object sender, EventArgs e)
        {
            if (txtNameAr.Value != "" && txtNameEn.Value != "")
            {
                if (btnAddDept.InnerText.Contains("تعديل"))
                {
                    sqlDept.UpdateCommand = !inputFile.HasFile ? sqlDept.UpdateCommand.Replace(", [Photo] = @Photo", "") : sqlDept.UpdateCommand;
                    sqlDept.Update();
                    Response.Redirect("~/Admin/MgItem.aspx?id=0");
                }
                else
                {
                    if (inputFile.HasFile)
                    {
                        sqlDept.Insert();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "",
                        @"hideAlertDiv('alertError');", true);
                        return;
                    }
                }
                sqlDept.EnableCaching = false;
                gvDept.DataBind();
                sqlDept.EnableCaching = true;
                txtNameAr.Value = txtNameEn.Value = "";
                cntDept.InnerText = gvDept.Rows.Count.ToString();
                    dvNoData.Visible = gvDept.Rows.Count == 0;
                    gvDept.Visible = gvDept.Rows.Count != 0;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "",
                @"hideAlertDiv('alertError');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "",
                @"hideAlertDiv('alertError');", true);
            }
        }

        protected void sqlDept_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Parameters["@NameAr"].Value = txtNameAr.Value;
            e.Command.Parameters["@NameEn"].Value = txtNameEn.Value;
            int len = inputFile.PostedFile.ContentLength;
            byte[] pic = new byte[len + 1];
            inputFile.PostedFile.InputStream.Read(pic, 0, len);
            ((SqlParameter)e.Command.Parameters["@Photo"]).SqlDbType = SqlDbType.VarBinary;
            e.Command.Parameters["@Photo"].Value = pic;
        }

        protected void sqlDept_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Parameters["@NameAr"].Value = txtNameAr.Value;
            e.Command.Parameters["@NameEn"].Value = txtNameEn.Value;
            if (inputFile.HasFile)
            {
                int len = inputFile.PostedFile.ContentLength;
                byte[] pic = new byte[len + 1];
                inputFile.PostedFile.InputStream.Read(pic, 0, len);
                ((SqlParameter)e.Command.Parameters["@Photo"]).SqlDbType = SqlDbType.VarBinary;
                e.Command.Parameters["@Photo"].Value = pic;
            }
            e.Command.Parameters["@Id"].Value = int.Parse(Request.QueryString["id"]);
        }

        protected void gvDept_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "EditDept")
            {
                Response.Redirect("~/Admin/MgItem.aspx?id=" + id);
            }
        }
    }
}