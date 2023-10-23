using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class Setting : Page
    {
        ClsBasic clsBasic;
        DataRow dataRow;

        //Sender
        //    /html/body/div[1]/div[1]/b

        //Sub Status
        //    /html/body/div[1]/b[1]

        //Db Status
        //    /html/body/div[1]/b[4]


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "UsGroupUk | Setting";
            clsBasic = new ClsBasic();
            if (!IsPostBack)
            {
                try
                {
                    dataRow = clsBasic.SelectData("*", "Login WHERE Id = " + Session["Id"]).Rows[0];
                }
                catch
                {
                    Session["Id"] = null;
                    Session["Email"] = null;
                    Session["Password"] = null;
                    Response.Redirect("~/User Check/Login.aspx");
                }

                txtName.Value = dataRow["Name"].ToString();
                txtEmail.Value = dataRow["Email"].ToString();
                txtPswd.Value = dataRow["Password"].ToString();
                txtReciever.Value = dataRow["Receiver"].ToString();
            }

            var client = new WebClient();
            string html = client.DownloadString("https://api.textmebot.com/status.php?apikey=EsnpvQrmfwXG");
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            try
            {
                tdSender.InnerText = document.DocumentNode.SelectSingleNode($"/html/body/div[1]/div[1]/b").InnerText;

                tdSubscription.InnerHtml = document.DocumentNode.SelectSingleNode($"/html/body/div[1]/b[1]").InnerText;

                tdDbStatus.InnerHtml = document.DocumentNode.SelectSingleNode($"/html/body/div[1]/b[4]").InnerText;
            }
            catch (Exception)
            {
                tdSender.InnerText = tdSubscription.InnerHtml = tdDbStatus.InnerHtml = "NULL";
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
                clsBasic.EditUser(int.Parse(Session["Id"].ToString()), txtName.Value, txtEmail.Value, txtPswd.Value, txtReciever.Value);
                Response.Redirect("Setting.aspx");
            }
        }
    }
}