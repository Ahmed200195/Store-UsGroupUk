using System;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web.UI;

namespace Store
{
    public partial class Login : Page
    {
        ClsBasic clsBasic;
        DataTable dataTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "UsGroupUk | Login";
            clsBasic = new ClsBasic();
        }

        protected void btnSend_ServerClick(object sender, EventArgs e)
        {
            dvAlert.Visible = false;
            dataTable = clsBasic.SelectData("*", $"Login WHERE Email = '{txtEmail.Value}' AND Password = '{txtPassword.Value}'");
            if (dataTable.Rows.Count > 0)
            {
                if (int.Parse(dataTable.Rows[0]["CntSession"].ToString()) >= 5)
                {
                    Random random = new Random();
                    if (SendAlerts(random.Next(0, 1000000).ToString("D6"), "us.group.kw100@gmail.com", "pbuhsobbvsfydtxr", txtEmail.Value))
                    {
                        Session["Id"] = dataTable.Rows[0]["Id"].ToString();
                        Session["Email"] = txtEmail.Value;
                        Session["Password"] = txtPassword.Value;
                        Response.Redirect("~/User Check/CodeEmail.aspx");
                    }
                    else
                    {
                        dvAlert.Visible = true;
                    }
                }
                else
                {
                    Session["Id"] = dataTable.Rows[0]["Id"].ToString();
                    Session["Email"] = txtEmail.Value;
                    Session["Password"] = txtPassword.Value;
                    sqlLogin.UpdateCommand = $"UPDATE Login SET CntSession = CntSession + 1 WHERE Email = '{txtEmail.Value}' AND Password = '{txtPassword.Value}'";
                    sqlLogin.Update();
                    Response.Redirect("~/Admin/Home.aspx");
                }
            }
            else
            {
                dvAlert.Visible = true;
            }
        }
        public bool SendAlerts(string code, string fmail, string fpass, string tomail)
        {
            try
            {
                var message = new MailMessage();
                var smtp = new SmtpClient();
                message.From = new MailAddress(fmail);
                message.To.Add(new MailAddress(tomail));
                message.Subject = "US Group UK";
                message.IsBodyHtml = true;
                message.Body = $@"
                                <h3>A message has been sent from a store to verify your identity. Please copy the code </h3><br>
                                <div style=""padding: 10px""><h1>{code}</h1></div>
                                ";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fmail, fpass);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                Session["Code"] = code;
                Session["Email"] = txtEmail.Value;
                Session["Password"] = txtPassword.Value;
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}