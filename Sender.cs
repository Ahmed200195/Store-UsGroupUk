using Newtonsoft.Json.Linq;


namespace Store
{
    public class Sender
    {
        public Sender(string json)
        {
            JObject jObject = JObject.Parse(json);
            status = jObject["status"].ToString();
            comment = jObject["comment"].ToString();
        }
        public string status { get; set; }
        public string comment { get; set; }
    }
}