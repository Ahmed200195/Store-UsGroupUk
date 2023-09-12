using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Client
{
    public partial class Site1 : MasterPage
    {
        string OrginalUrl, url;
        protected void Page_Load(object sender, EventArgs e)
        {
            OrginalUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (!IsPostBack)
            {
                ddlDept.DataTextField = Application["lang"].ToString() == "en" ? "NameEn" : "NameAr";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, new ListItem() { Value = "0", Text = Application["lang"].ToString() == "en" ? "All Categories" : "جميع الاقسام" });
                if (OrginalUrl.Contains("ShowProduct"))
                {
                    ddlDept.SelectedValue = Request.QueryString["dept"].ToString();
                }
                btnToggleLang.InnerText = Application["lang"].ToString() == "en" ? "عربي" : "English";
                txtSearch.Attributes["placeholder"] = Application["lang"].ToString() == "en" ? "I'm searching for ..." : "أنا أبحث عن...";
            }
        }

        protected void btnToggleLang_ServerClick(object sender, EventArgs e)
        {
            Application["lang"] = Application["lang"].ToString() == "en" ? "ar" : "en";
            btnToggleLang.InnerText = Application["lang"].ToString() == "en" ? "عربي" : "English";
            Response.Redirect(OrginalUrl);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (OrginalUrl.Contains("ShowProduct"))
            {
                string search = txtSearch.Value == string.Empty ? "0" : txtSearch.Value;
                url = OrginalUrl.Replace("dept=" + Request.QueryString["dept"], "dept=" + ddlDept.SelectedValue);
                url = url.Replace("search=" + Request.QueryString["search"], "search=" + search);
                Session["load"] = "1";
                Response.Redirect(url);

            }
            else
            {
                Response.Redirect($"ShowProduct.aspx?dept={ddlDept.SelectedValue}&size=0&color=0&sortBy=0&search=" + txtSearch.Value);
            }

        }

        

    }
}