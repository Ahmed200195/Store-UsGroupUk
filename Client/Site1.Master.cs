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
            if (Session["lang"] == null)
            {
                Session["lang"] = "ar";
            }
            OrginalUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            try
            {

                dvInfoProduct.InnerHtml = HttpContext.Current.Session["dvInfoProduct"].ToString();

//                ScriptManager.RegisterStartupScript(this, typeof(Page), "", "selectedProducts = ['1']", true);
            }
            catch
            {
            }
            if (!IsPostBack)
            {
                ddlDept.DataTextField = Session["lang"].ToString() == "en" ? "NameEn" : "NameAr";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, new ListItem() { Value = "0", Text = Session["lang"].ToString() == "en" ? "All Categories" : "جميع الاقسام" });
                if (OrginalUrl.Contains("ShowProduct"))
                {
                    ddlDept.SelectedValue = Request.QueryString["dept"].ToString();
                }
                btnToggleLang.InnerText = Session["lang"].ToString() == "en" ? "عربي" : "English";
                txtSearch.Attributes["placeholder"] = Session["lang"].ToString() == "en" ? "I'm searching for ..." : "أنا أبحث عن...";
            }
        }

        protected void btnToggleLang_ServerClick(object sender, EventArgs e)
        {
            Session["lang"] = Session["lang"].ToString() == "en" ? "ar" : "en";
            btnToggleLang.InnerText = Session["lang"].ToString() == "en" ? "عربي" : "English";

            string cart = dvInfoProduct.InnerHtml;

            if (Session["lang"].ToString() == "en")
            {
                cart = cart.Replace("<figure class=\"relative w-1/4 ml-4\">", "<figure class=\"relative w-1/4 mr-4\">");
                cart = cart.Replace("سعر الوحدة", "unit price");
                cart = cart.Replace("د.ك", "k.d");
                cart = cart.Replace("اللون", "color");
                cart = cart.Replace("القياس", "size");
                cart = cart.Replace("<div class=\"flex flex-row-reverse qty h-9\">", "<div class=\"flex flex-row qty h-9\">");
            }
            else
            {
                cart = cart.Replace("<figure class=\"relative w-1/4 mr-4\">", "<figure class=\"relative w-1/4 ml-4\">");
                cart = cart.Replace("unit price", "سعر الوحدة");
                cart = cart.Replace("k.d", "د.ك");
                cart = cart.Replace("color", "اللون");
                cart = cart.Replace("size", "القياس");
                cart = cart.Replace("<div class=\"flex flex-row qty h-9\">", "<div class=\"flex flex-row-reverse qty h-9\">");
            }

            HttpContext.Current.Session["dvInfoProduct"] = cart;
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