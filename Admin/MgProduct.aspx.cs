using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class MgProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                        ddlSize.SelectedValue = row["SId"].ToString();
                        if (!Convert.IsDBNull(row["Photo"]))
                        {
                            preview2.InnerHtml = @"<img height=""100"" width=""100"" src=""data:image;base64," + Convert.ToBase64String((byte[])row["Photo"]) + @""">";
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
                
                if (Request.QueryString["id"] == "0")
                {
                    if (!FileUpload1.HasFiles || !FileUpload2.HasFile)
                    {
                        return;
                    }
                    sqlProduct.InsertParameters["NameAr"].DefaultValue = txtNameAr.Value;
                    sqlProduct.InsertParameters["NameEn"].DefaultValue = txtNameEn.Value;
                    sqlProduct.InsertParameters["Price"].DefaultValue = txtPrice.Value;
                    sqlProduct.InsertParameters["Discount"].DefaultValue = txtDisc.Value;
                    sqlProduct.InsertParameters["infoAr"].DefaultValue = txtInfoAr.Value;
                    sqlProduct.InsertParameters["infoEn"].DefaultValue = txtInfoEn.Value;
                    sqlProduct.InsertParameters["DId"].DefaultValue = ddlDept.SelectedValue;
                    sqlProduct.InsertParameters["BId"].DefaultValue = ddlBrand.SelectedValue;
                    sqlProduct.InsertParameters["CId"].DefaultValue = ddlColor.SelectedValue;
                    sqlProduct.InsertParameters["SId"].DefaultValue = ddlSize.SelectedValue;
                    sqlProduct.Insert();
                }
                else
                {
                    if (FileUpload2.HasFile)
                    {
                        sqlBgProduct.Update();
                    }
                    sqlProduct.UpdateParameters["NameAr"].DefaultValue = txtNameAr.Value;
                    sqlProduct.UpdateParameters["NameEn"].DefaultValue = txtNameEn.Value;
                    sqlProduct.UpdateParameters["Price"].DefaultValue = txtPrice.Value;
                    sqlProduct.UpdateParameters["Discount"].DefaultValue = txtDisc.Value;
                    sqlProduct.UpdateParameters["infoAr"].DefaultValue = txtInfoAr.Value;
                    sqlProduct.UpdateParameters["infoEn"].DefaultValue = txtInfoEn.Value;
                    sqlProduct.UpdateParameters["DId"].DefaultValue = ddlDept.SelectedValue;
                    sqlProduct.UpdateParameters["BId"].DefaultValue = ddlBrand.SelectedValue;
                    sqlProduct.UpdateParameters["CId"].DefaultValue = ddlColor.SelectedValue;
                    sqlProduct.UpdateParameters["SId"].DefaultValue = ddlSize.SelectedValue;
                    sqlProduct.UpdateParameters["Id"].DefaultValue = Request.QueryString["id"];
                    sqlProduct.Update();
                }
            }
        }
        protected void sqlProduct_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            int len = FileUpload2.PostedFile.ContentLength;
            byte[] pic = new byte[len + 1];
            FileUpload2.PostedFile.InputStream.Read(pic, 0, len);
            ((SqlParameter)e.Command.Parameters["@Photo"]).SqlDbType = SqlDbType.Image;
            e.Command.Parameters["@Photo"].Value = pic;
        }
        protected void sqlProduct_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            foreach (HttpPostedFile file in FileUpload1.PostedFiles)
            {
                postedFile = file;
                sqlImgProduct.InsertParameters["Name"].DefaultValue = postedFile.FileName;
                sqlImgProduct.InsertParameters["ContentType"].DefaultValue = postedFile.ContentType;
                sqlImgProduct.InsertParameters["PId"].DefaultValue = e.Command.Parameters["@Identity"].Value.ToString();
                sqlImgProduct.Insert();
            }
            Response.Redirect("ViewProduct.aspx");
        }
        protected void sqlProduct_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            if (FileUpload1.HasFiles)
            {
                foreach (HttpPostedFile file in FileUpload1.PostedFiles)
                {
                    postedFile = file;
                    sqlImgProduct.InsertParameters["Name"].DefaultValue = postedFile.FileName;
                    sqlImgProduct.InsertParameters["ContentType"].DefaultValue = postedFile.ContentType;
                    sqlImgProduct.InsertParameters["PId"].DefaultValue = Request.QueryString["id"];
                    sqlImgProduct.Insert();
                }
            }
            Response.Redirect("ViewProduct.aspx");
        }

        protected void sqlImgProduct_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            int len = postedFile.ContentLength;
            byte[] pic = new byte[len + 1];
            postedFile.InputStream.Read(pic, 0, len);
            ((SqlParameter)e.Command.Parameters["@Photo"]).SqlDbType = SqlDbType.Image;
            e.Command.Parameters["@Photo"].Value = pic;
        }
        HttpPostedFile postedFile;


        protected void gvImages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DelPhoto")
            {
                sqlImgProduct.DeleteParameters["Id"].DefaultValue = e.CommandArgument.ToString();
                sqlImgProduct.Delete();
                gvImages.DataBind();
            }
        }

        protected void sqlBgProduct_Updating(object sender, SqlDataSourceCommandEventArgs e)
        {
            e.Command.Parameters["@Id"].Value = Request.QueryString["id"];
            int len = FileUpload2.PostedFile.ContentLength;
            byte[] pic = new byte[len + 1];
            FileUpload2.PostedFile.InputStream.Read(pic, 0, len);
            ((SqlParameter)e.Command.Parameters["@Photo"]).SqlDbType = SqlDbType.Image;
            e.Command.Parameters["@Photo"].Value = pic;
        }
    }
}