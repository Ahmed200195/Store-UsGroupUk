using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.DynamicData;

namespace Store.Admin
{
    public partial class MgProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "UsGroupUk | MgProduct";

            if (!IsPostBack)
            {
                ddlSize.DataBind();
                ddlSize.Items.Insert(0, new ListItem() { Text = "No Thing", Value = "", Selected = true });

                ddlColor.DataBind();
                ddlColor.Items.Insert(0, new ListItem() { Text = "No Thing", Value = "", Selected = true });
            }

            if (Request.QueryString["id"] != "0")
            {
                if (!IsPostBack)
                {
                    DataView view = (DataView)sqlProduct.Select(new DataSourceSelectArguments());
                    DataTable dataTable = view.ToTable();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        txtNameAr.Value = row["NameAr"].ToString();
                        txtNameEn.Value = row["NameEn"].ToString();
                        txtPrice.Value = row["Price"].ToString();
                        txtDisc.Value = row["Discount"].ToString();
                        txtInfoAr.Value = row["infoAr"].ToString();
                        txtInfoEn.Value = row["infoEn"].ToString();
                        txtCnt.Value = row["Cnt"].ToString();
                        ddlSize.SelectedValue = row["SId"].ToString();
                        ViewState["oldUpload"] = "";
                        if (!Convert.IsDBNull(row["Photo"]))
                        {
                            ViewState["oldUpload"] = row["Photo"];
                            preview2.InnerHtml = $@"<img height=""100"" width=""100"" class=""lazy-load"" data-src=""{"../Uploads/Product/" + row["Photo"]}"">";
                        }
                        ddlDept.SelectedValue = row["DId"].ToString();
                        ddlBrand.SelectedValue = row["BId"].ToString();
                        ddlColor.SelectedValue = row["CId"].ToString();
                    }
                    gvImages.DataBind();

                }
                btnAdd.InnerText = "تعديل البيانات";
            }
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            
            if (txtNameAr.Value != "" && txtNameEn.Value != "" && txtPrice.Value != "" && txtDisc.Value != "" &&
                ddlDept.SelectedItem.Text != "" && ddlBrand.SelectedItem.Text != "" && txtInfoAr.Value != "" && txtInfoEn.Value != "")
            {
                ClsBasic clsBasic = new ClsBasic();
                string id = clsBasic.GetIndexId("Product"), fileName2 = "BackProduct" + id;
                if (Request.QueryString["id"] == "0")
                {
                    if (!FileUpload1.HasFiles || !FileUpload2.HasFile)
                    {
                        return;
                    }
                    if (FileUpload2.HasFile)
                    {
                        string type = FileUpload2.PostedFile.ContentType;
                        fileName2 = fileName2 + "." + type.Substring(type.IndexOf('/') + 1);
                        string path = Path.Combine(Server.MapPath("~/Uploads/Product"), fileName2);
                        FileUpload2.SaveAs(path);
                    }
                    sqlProduct.InsertParameters["NameAr"].DefaultValue = txtNameAr.Value;
                    sqlProduct.InsertParameters["NameEn"].DefaultValue = txtNameEn.Value;
                    sqlProduct.InsertParameters["Price"].DefaultValue = txtPrice.Value;
                    sqlProduct.InsertParameters["Discount"].DefaultValue = txtDisc.Value;
                    sqlProduct.InsertParameters["infoAr"].DefaultValue = txtInfoAr.Value;
                    sqlProduct.InsertParameters["infoEn"].DefaultValue = txtInfoEn.Value;
                    sqlProduct.InsertParameters["Photo"].DefaultValue = fileName2;
                    sqlProduct.InsertParameters["Cnt"].DefaultValue = txtCnt.Value;
                    sqlProduct.InsertParameters["DId"].DefaultValue = ddlDept.SelectedValue;
                    sqlProduct.InsertParameters["BId"].DefaultValue = ddlBrand.SelectedValue;
                    sqlProduct.InsertParameters["CId"].DefaultValue = ddlColor.SelectedValue;
                    sqlProduct.InsertParameters["SId"].DefaultValue = ddlSize.SelectedValue;
                    sqlProduct.Insert();
                }
                else
                {
                    string fileName = string.Empty;
                    if (FileUpload2.HasFile)
                    {
                        fileName = "BackProduct" + FileUpload2.FileName;
                        string path = Path.Combine(Server.MapPath("~/Uploads/Product"), fileName);
                        if (ViewState["oldUpload"].ToString() != string.Empty)
                        {
                            File.Delete(Path.Combine(Server.MapPath("~/Uploads/Product"), ViewState["oldUpload"].ToString()));
                        }
                        FileUpload2.SaveAs(path);
                    }
                    
                    sqlProduct.UpdateParameters["NameAr"].DefaultValue = txtNameAr.Value;
                    sqlProduct.UpdateParameters["NameEn"].DefaultValue = txtNameEn.Value;
                    sqlProduct.UpdateParameters["Price"].DefaultValue = txtPrice.Value;
                    sqlProduct.UpdateParameters["Discount"].DefaultValue = txtDisc.Value;
                    sqlProduct.UpdateParameters["infoAr"].DefaultValue = txtInfoAr.Value;
                    sqlProduct.UpdateParameters["infoEn"].DefaultValue = txtInfoEn.Value;
                    sqlProduct.UpdateParameters["Photo"].DefaultValue = fileName;
                    sqlProduct.UpdateParameters["Cnt"].DefaultValue = txtCnt.Value;
                    sqlProduct.UpdateParameters["DId"].DefaultValue = ddlDept.SelectedValue;
                    sqlProduct.UpdateParameters["BId"].DefaultValue = ddlBrand.SelectedValue;
                    sqlProduct.UpdateParameters["CId"].DefaultValue = ddlColor.SelectedValue;
                    sqlProduct.UpdateParameters["SId"].DefaultValue = ddlSize.SelectedValue;
                    sqlProduct.UpdateParameters["Id"].DefaultValue = Request.QueryString["id"];
                    sqlProduct.Update();
                }
            }
        }
        protected void sqlProduct_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            ClsBasic clsBasic = new ClsBasic();
            string path, fileName, type;
            int count = int.Parse(clsBasic.GetIndexId("ProductPhotos"));
            foreach (HttpPostedFile file in FileUpload1.PostedFiles)
            {
                type = file.ContentType;
                fileName = count + "ItemProduct" + e.Command.Parameters["@Identity"].Value + "." + type.Substring(type.IndexOf('/') + 1);
                path = Path.Combine(Server.MapPath("~/Uploads/Product"), fileName);
                file.SaveAs(path);
                sqlImgProduct.InsertParameters["Name"].DefaultValue = fileName;
                sqlImgProduct.InsertParameters["PId"].DefaultValue = e.Command.Parameters["@Identity"].Value.ToString();
                sqlImgProduct.Insert();
                count++;
            }
            Response.Redirect("ViewProduct.aspx");
        }
        protected void sqlProduct_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            ClsBasic clsBasic = new ClsBasic();
            string path, fileName, type;
            int count = int.Parse(clsBasic.GetIndexId("ProductPhotos"));
            if (FileUpload1.HasFiles)
            {
                foreach (HttpPostedFile file in FileUpload1.PostedFiles)
                {
                    type = file.ContentType;
                    fileName = count + "ItemProduct" + Request.QueryString["id"] + "." + type.Substring(type.IndexOf('/') + 1);
                    path = Path.Combine(Server.MapPath("~/Uploads/Product"), fileName);
                    file.SaveAs(path);

                    sqlImgProduct.InsertParameters["Name"].DefaultValue = fileName;
                    sqlImgProduct.InsertParameters["PId"].DefaultValue = Request.QueryString["id"];
                    sqlImgProduct.Insert();
                    count++;
                }
            }
            Response.Redirect("ViewProduct.aspx");
        }


        protected void gvImages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DelPhoto")
            {
                string key, id, name;
                key = e.CommandArgument.ToString();
                id = key.Substring(0, key.IndexOf('|'));
                name = key.Substring(key.IndexOf('|') + 1);
                File.Delete(Path.Combine(Server.MapPath("~/Uploads/Product"), name));
                sqlImgProduct.DeleteParameters["Id"].DefaultValue = id;
                sqlImgProduct.Delete();
                gvImages.DataBind();
            }
        }
    }
}