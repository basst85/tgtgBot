using System;

namespace tgtgBot.Models
{
    public class User
    {
        public string email { get; set; }
        public string password { get; set; }
        public string device_type { get; set; }
        public string user_id { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}