using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Store.Client
{
    /// <summary>
    /// Summary description for WebServiceSendDt
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]

    public class WebServiceSendDt : WebService
    {
        //client.DownloadString("http://api.textmebot.com/send.php?recipient=+963936968795&apikey=1y6ewnMQPjpw&text=" + info);
        //client.DownloadString("http://api.textmebot.com/send.php?recipient=+201002409654&apikey=EsnpvQrmfwXG&text=" + info);
        //client.DownloadString("http://api.textmebot.com/send.php?recipient=+96597983633&apikey=EsnpvQrmfwXG&text=" + info);



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SaveCart(string dvInfoProduct, List<string> dataPk)
        {
            //, List<string> dataPk
            HttpContext.Current.Session["dvInfoProduct"] = dvInfoProduct;
            string ids = string.Empty;
            foreach (string item in dataPk)
            {
                ids +=  "'" +item + "',";
            }
            ids = ids.TrimEnd(',');
            HttpContext.Current.Session["dataPk"] = ids;
        }


        [WebMethod]
        public void PaidCheckOut(string dvInfoProduct, List<string> list)
        {
            ClsBasic clsBasic = new ClsBasic();
            string pkClient = clsBasic.newOrder(dvInfoProduct, list);
            // Extract the domain from the URL
            Uri currentUrl = HttpContext.Current.Request.Url;
            string domain = currentUrl.Scheme + "://" + currentUrl.Host;

            Thread.Sleep(5000);
            Action<CancellationToken> send = async (token) =>
            {
                await Task.Run(() => DoWork(dvInfoProduct, list, pkClient, domain));
            };
            HostingEnvironment.QueueBackgroundWorkItem(send);
        }

        public static void DoWork(string dvInfoProduct, List<string> list, string pkClient, string url)
        {
            string info = $"الاسم العميل : {list[0]}\n" +
                $"الهاتف : {list[1]}\n" +
                $"اسم المنطقة : {list[2]}\n" +
                $"رقم القطعة : {list[3]}\n" +
                $"رقم المنزل : {list[4]}\n" +
                $"رقم الشارع : {list[5]}";

            ClsBasic clsBasic = new ClsBasic();
            DataTable dtProduct = clsBasic.SelectData("*", "Product");
            DataRow dataRow;

            int cnt = 1;
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(dvInfoProduct);
            var headers = document.DocumentNode.SelectNodes("//div[@class='elementCart flex mt-2']");
            if (headers != null)
            {
                string dataId, count, total;
                foreach (HtmlNode header in headers)
                {
                    info += $"\n\n\n> المتنج {cnt}\n";
                    dataId = header.Attributes["data-id"].Value;
                    dataRow = dtProduct.Select("Id = " + dataId).First();
                    try
                    {
                        count = header.SelectSingleNode($".//div//div//div[@class='flex flex-row qty h-9']//span[@class='flex justify-center items-center w-11 text-center numberQty']").InnerText;
                    }
                    catch
                    {
                        count = header.SelectSingleNode($".//div//div//div[@class='flex flex-row-reverse qty h-9']//span[@class='flex justify-center items-center w-11 text-center numberQty']").InnerText;
                    }
                    total = header.SelectSingleNode(".//div//div//div[@class='suptotalTotalPrice']//span").InnerText;
                    info += $"الاسم : {dataRow["NameAr"]}\n" +
                            $"السعر : {dataRow["Price"]}\n" +
                            $"السعر بعد الخصم : {dataRow["Discount"]}\n" +
                            $"العدد : {count}\n" +
                            $"المجموع : {total}";
                    cnt++;
                }
            }
            info += $"\n\n\nالرابط : {url}/Admin/PeOrder.aspx?id={pkClient}";
            string reciever = clsBasic.SelectData("Receiver", "Login WHERE Id = 1").Rows[0][0].ToString();
            resend:
            var client = new WebClient();
            Thread.Sleep(GenerateRandomNo());
            string link = $"http://api.textmebot.com/send.php?recipient={reciever}&apikey=EsnpvQrmfwXG&text=" + info + "&json=yes";
            string json = client.DownloadString(link);
            Sender sender = new Sender(json);
            if (sender.status == "error")
            {
                if (sender.comment == "8 sec. Delay needed")
                {
                    goto resend;
                }
            }
            
        }
        public static int GenerateRandomNo()
        {
            int _min = 6000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}
