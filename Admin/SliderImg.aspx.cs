using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Store.Admin
{
    public partial class SliderImg : System.Web.UI.Page
    {
        private HttpPostedFile postedFile;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "UsGroupUk | Slider Images";
            gvImages.DataBind();
            dvNoData.Visible = gvImages.Rows.Count == 0;
            gvImages.Visible = gvImages.Rows.Count != 0;
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

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            if (FileUpload1.HasFiles)
            {
                string path;
                foreach (HttpPostedFile file in FileUpload1.PostedFiles)
                {
                    path = Path.Combine(Server.MapPath("~/Uploads/Product"), file.FileName);
                    file.SaveAs(path);
                    postedFile = file;
                    sqlImgProduct.InsertParameters["Name"].DefaultValue = postedFile.FileName;
                    sqlImgProduct.Insert();
                }
            }
            Response.Redirect("SliderImg.aspx");
        }
    }
}