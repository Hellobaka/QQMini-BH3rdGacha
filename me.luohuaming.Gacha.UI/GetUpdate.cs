using Newtonsoft.Json;
using Native.Tool.Http;

namespace Gacha.UI
{
    public class GetUpdate
    {
        public class Update
        {
            public string GachaVersion { get; set; }
            public string Date { get; set; }
            public string Whatsnew { get; set; }
        }
        public Update GetVersion()
        {
            string str = new HttpWebClient().DownloadString
                ("https://gitee.com/Hellobaka/BH3rdGachaSimulator/raw/master/New.json");
            Update version= JsonConvert.DeserializeObject<Update>(str);
            return version;
        }
    }
}
