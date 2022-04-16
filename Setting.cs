using Newtonsoft.Json;
namespace WalletProxy
{
    public class Setting
    {
        public Settings? Settings {get; private set;}
        public Setting()
        {
            var strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            var filename = Path.Combine(strWorkPath!, "settings.json");        
            Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filename));
        }
    }

         
    public class Settings
    {
        public string rpcuri { get; set; } = null!;
        public string rpcuser { get; set; } = null!;
        public string rpcpassword { get; set; } = null!;
    }
}