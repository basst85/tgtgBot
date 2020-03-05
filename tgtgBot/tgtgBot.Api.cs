using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using tgtgBot.Models;

namespace tgtgBot.Api
{
    public class Auth
    {
        public static async Task<String> GetAccessToken(User tgtgUser)
        {
            string loginUrl = "https://apptoogoodtogo.com/api/auth/v1/loginByEmail";
            string access_token = String.Empty;

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "tgtgBot/0.01");
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", "nl");
                    tgtgUser.device_type = "IOS";

                    string userJson = JsonConvert.SerializeObject(tgtgUser);
                    StringContent postData = new StringContent(userJson, Encoding.UTF8, "application/json");

                    using (HttpResponseMessage result = await httpClient.PostAsync(loginUrl, postData))
                    {
                        using (HttpContent content = result.Content)
                        {
                            string responseData = await content.ReadAsStringAsync();

                            if (responseData != null){
                                // Get oAuth access_token from response JSON
                                access_token = JObject.Parse(responseData)["access_token"].ToString();
                                tgtgUser.access_token = access_token;
                                
                                // Get user_id and oAuth refresh_token 
                                tgtgUser.refresh_token = JObject.Parse(responseData)["refresh_token"].ToString();
                                tgtgUser.user_id = JObject.Parse(responseData)["startup_data"]["user"]["user_id"].ToString();
                            }
                            else
                            {
                                // No data received
                                access_token = string.Empty;
                            }
                        }
                    }
                }
                return access_token;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}