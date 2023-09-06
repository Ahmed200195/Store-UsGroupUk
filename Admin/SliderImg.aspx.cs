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
    public partial class SliderImg : System.Web.UI.Page
    {
        private HttpPostedFile postedFile;

        protected void Page_Load(object sender, EventArgs e)
        {
            gvImages.DataBind();
            dvNoData.Visible = gvImages.Rows.Count == 0;
            gvImages.Visible = gvImages.Rows.Count != 0;
        }

        protected void sqlImgProduct_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            int len = postedFile.ContentLength;
            byte[] pic = new byte[len + 1];
            postedFile.InputStream.Read(pic, 0, len);
            ((SqlParameter)e.Command.Parameters["@Photo"]).SqlDbType = SqlDbType.Image;
            e.Command.Parameters["@Photo"].Value = pic;
        }

        protected void gvImages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DelPhoto")
            {
                sqlImgProduct.DeleteParameters["Id"].DefaultValue = e.CommandArgument.ToString();
                sqlImgProduct.Delete();
                gvImages.DataBind();
            }
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            if (FileUpload1.HasFiles)
            {
                foreach (HttpPostedFile file in FileUpload1.PostedFiles)
                {
                    postedFile = file;
                    sqlImgProduct.InsertParameters["Name"].DefaultValue = postedFile.FileName;
                    sqlImgProduct.InsertParameters["ContentType"].DefaultValue = postedFile.ContentType;
                    sqlImgProduct.Insert();
                }
            }
            Response.Redirect("SliderImg.aspx");
        }
    }
}