using Newtonsoft.Json;
public class AppSettings
{
        public static string rpcuri { get; private set; } = null!;
        public static string rpcuser { get; private set;} = null!;
        public static string rpcpassword { get; private set; } = null!;

        private class Settings
        {
            public string rpcuri { get; set; } = null!;
            public string rpcuser { get; set; } = null!;
            public string rpcpassword { get; set; } = null!;
        }

        //Static constructor
        static AppSettings() 
        {  
            var strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            var filename = Path.Combine(strWorkPath!, "settings.json");        
            var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filename));
            AppSettings.rpcuri = settings!.rpcuri;
            AppSettings.rpcuser = settings!.rpcuser;
            AppSettings.rpcpassword = settings!.rpcpassword;
        }  
}