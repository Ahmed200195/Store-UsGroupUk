using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;
using System.IO;

namespace Store.Admin
{
    public partial class MgItem : Page
    {
        ClsBasic clsBasic;
        DataRow dataRow;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "UsGroupUk | MgItem";
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
                    imgView.Attributes["data-src"] = "../Uploads/Dept/" + dataRow["Photo"].ToString();
                    ViewState["oldUpload"] = dataRow["Photo"].ToString();
                }
            }
        }

        protected void btnAddDept_ServerClick(object sender, EventArgs e)
        {
            ClsBasic clsBasic = new ClsBasic();
            string id = clsBasic.GetIndexId("Dept"), fileName = "BackDept" + id;
            string type = inputFile.PostedFile.ContentType;
            fileName = fileName + "." + type.Substring(type.IndexOf('/') + 1);

            if (txtNameAr.Value != "" && txtNameEn.Value != "")
            {
                if (btnAddDept.InnerText.Contains("تعديل"))
                {
                    if (!inputFile.HasFile)
                    {
                        sqlDept.UpdateCommand = sqlDept.UpdateCommand.Replace(", [Photo] = @Photo", "");
                    }
                    else
                    {
                        string path = Path.Combine(Server.MapPath("~/Uploads/Dept"), "Dept" + inputFile.FileName);
                        if (ViewState["oldUpload"].ToString() != "")
                        {
                            File.Delete(Path.Combine(Server.MapPath("~/Uploads/Dept"), ViewState["oldUpload"].ToString()));
                        }
                        inputFile.SaveAs(path);
                        sqlDept.UpdateParameters["Photo"].DefaultValue = "Dept" + inputFile.FileName;
                    }
                    sqlDept.Update();
                    Response.Redirect("~/Admin/MgItem.aspx?id=0");
                }
                else
                {
                    if (inputFile.HasFile)
                    {
                        string path = Path.Combine(Server.MapPath("~/Uploads/Dept"), fileName);
                        inputFile.SaveAs(path);
                        sqlDept.InsertParameters["Photo"].DefaultValue = fileName;
                        sqlDept.Insert();
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "",
                        //@"hideAlertDiv('alertError');", true);
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
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "",
                //@"hideAlertDiv('alertError');", true);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "",
                //@"hideAlertDiv('alertError');", true);
            }
        }

        protected void sqlDept_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Parameters["@NameAr"].Value = txtNameAr.Value;
            e.Command.Parameters["@NameEn"].Value = txtNameEn.Value;
        }

        protected void sqlDept_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Parameters["@NameAr"].Value = txtNameAr.Value;
            e.Command.Parameters["@NameEn"].Value = txtNameEn.Value;
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